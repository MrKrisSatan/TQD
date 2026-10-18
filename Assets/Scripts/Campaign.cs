using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuietDread {
[Serializable] public class Veteran { public string id,type; public int hp,xp,level; }
[Serializable] public class Expedition {public string commanderType;public int id,region;public bool moved;public List<Veteran> units=new List<Veteran>();}
[Serializable] public class Province {public int id,x,y,tier=1,growth,ruined;public string owner="",name;public List<string> buildings=new List<string>();}
[Serializable] public class TrailNode {public int act,row,lane;public string kind;public bool visited;}
[Serializable] public class Invasion {public int province;public string attacker;}
[Serializable] public class RivalLedger {public string faction;public int gold=180;}
[Serializable] public class RecruitmentStock {public string type;public int count;}
[Serializable] public partial class Campaign {
 public string mode,faction,difficulty="normal",phase="map",status="active",message="",commanderId,pendingId;
 public int gold=350,turn=1,victories,act=1,row=-1,lane=-1,selectedArmy=1,nextUnit=1,nextArmy=2,selectedProvince,attackTarget=-1,siegeTarget=-1,siegeTurns,siegeArmy,defense=-1,nextBattle=1;
 public string pendingEnemy,doctrine="thunder";
 public bool battleReward,territoryReward,settled;
 public uint seed=42; public List<TrailNode> nodes=new List<TrailNode>(); public List<Expedition> armies=new List<Expedition>();public List<Province> provinces=new List<Province>();
 public List<Invasion> invasionQueue=new List<Invasion>(); public List<string> log=new List<string>();public List<string> rivals=new List<string>();
 public List<RivalLedger> ledgers=new List<RivalLedger>();public List<RecruitmentStock> stock=new List<RecruitmentStock>();
 public string eventKind; public int pendingLane,pendingRow; public bool pendingBoss;
 [NonSerialized] public Content data;
 public Expedition Army {get{return armies.FirstOrDefault(a=>a.id==selectedArmy);}}
 public void Note(string s){message=s;log.Insert(0,s);if(log.Count>35)log.RemoveAt(log.Count-1);}
 float Random(){seed=unchecked(seed*1664525u+1013904223u);return (seed>>8)/16777216f;}
 public Veteran New(string type){return new Veteran{id="v"+nextUnit++,type=type,hp=data.P(type).hp};}
 public static Campaign Create(Content d,string mode,string faction,List<string> roster,string difficulty="normal",uint seed=42,TrailProgression progression=null){
  var c=new Campaign{data=d,progression=progression,mode=mode,faction=faction,difficulty=difficulty,seed=seed,gold=mode=="trail"?160:350};
  string selectedLeader=roster.First(id=>d.P(id).commander);if(progression!=null){if(!progression.CanSelect(d,selectedLeader))throw new InvalidOperationException("Commander has not been discovered.");roster=roster.Where(id=>progression.Allows(d,id,selectedLeader)).ToList();}var army=new Expedition{id=1,region=0,commanderType=selectedLeader};string commander=roster.First(t=>d.P(t).commander);var ordered=roster.ToList();ordered.Remove(commander);ordered.Insert(0,commander);var types=mode=="trail"?StartingWarband(d,faction,ordered,progression):ordered.Take(6).ToList();var titan=roster.FirstOrDefault(t=>d.P(t).titan);if(mode!="trail"&&titan!=null&&!types.Contains(titan))types.Add(titan);army.units=types.Select(c.New).ToList();c.armies.Add(army);c.commanderId=army.units.First(u=>d.P(u.type).commander).id;if(c.PersonalWins>=4)c.gold+=50;
  if(mode=="trail"){c.GenerateTrail();c.Note("Three formations begin a procedural three-act trail. Recruit up to twenty, train survivors and defeat the final sentinel. Losing your original commander ends the run.");}
  else{var pool=d.factions.Where(f=>f.id!=faction).OrderBy(f=>c.Random()).Take(3).ToList();c.rivals=pool.Select(f=>f.id).ToList();for(int y=0;y<4;y++)for(int x=0;x<4;x++){int id=y*4+x;c.provinces.Add(new Province{id=id,x=x,y=y,name=new[]{"Ash Reach","Silent March","Glass Basin","Crown Verge"}[y]+" "+(x+1)});}var capital=c.provinces[0];capital.owner=faction;capital.tier=2;capital.buildings.Add("barracks");for(int i=0;i<3;i++){var p=c.provinces[new[]{15,3,12}[i]];p.owner=c.rivals[i];p.tier=2;}c.Note("Secure relays, fund armies and take 60% of the frontier. Each army moves once per strategic turn.");}
  if(mode=="conquest"){foreach(var r in c.rivals)c.ledgers.Add(new RivalLedger{faction=r});foreach(var p in d.units.Where(p=>p.faction==faction))c.stock.Add(new RecruitmentStock{type=p.id,count=p.unique?1:2});}
  return c;
 }
 public bool Available(TrailNode n){return mode=="trail"&&phase=="map"&&status=="active"&&n.act==act&&n.row==row+1&&(lane<0||n.kind=="Boss"||Math.Abs(n.lane-lane)<=1);}
 public bool Visit(TrailNode n){if(!Available(n))return false;n.visited=true;pendingLane=n.lane;pendingRow=n.row;pendingBoss=n.kind=="Boss";eventKind=n.kind;
  if(n.kind=="Battle"||n.kind=="Elite"||n.kind=="Boss"){phase="battle";pendingId="trail-"+nextBattle++;settled=false;}
  else phase=n.kind=="Camp"?"camp":n.kind=="Armory"?"shop":"event";Note("Reached "+n.kind+".");return true;
 }
 public void AdvanceTrail(){row=pendingRow;lane=pendingLane;phase="map";if(pendingBoss){if(act>=3){status="won";Note("The third sentinel falls. Your expedition survives the death trail.");}else{act++;row=-1;lane=-1;foreach(var u in Army.units)u.hp=data.P(u.type).hp+u.level*3;Note("Act "+act+" begins. Your surviving formations recover.");}}pendingBoss=false;}
 bool ChoiceCore(string choice){if(status!="active")return false;if(mode!="trail")return false;
  if(phase=="reward"){if(choice=="gold")gold+=60;else Heal(.35f);AdvanceTrail();return true;}
  if(phase=="camp"){if(choice=="gold")gold+=80;else Heal(.5f);AdvanceTrail();return true;}
  if(phase=="event"){if(choice=="safe")gold+=45;else if(Random()<.6f){gold+=110;foreach(var u in Army.units)u.xp++;Note("The silent vault yields supplies and experience.");}else{foreach(var u in Army.units)u.hp=Math.Max(1,(int)(u.hp*.8f));Note("The vault wakes. Your formations escape wounded.");}AdvanceTrail();return true;}
  if(phase=="shop"&&choice=="leave"){AdvanceTrail();return true;}return false;
 }
 public void Heal(float fraction){if(Army==null)return;if(PersonalWins>=8)fraction+=.1f;foreach(var u in Army.units)u.hp=Math.Min(data.P(u.type).hp+u.level*3,u.hp+(int)((data.P(u.type).hp+u.level*3)*fraction));}
 public string RecruitError(string type){if(status!="active"||Army==null||phase!="map"&&phase!="shop")return "Recruitment is unavailable here.";var p=data.P(type);if(p.faction!=faction)return "Foreign formation.";if(CampaignLock(type)!=null)return CampaignLock(type);if(Army.units.Count>=ArmyCapacity)return "Army is full.";if(gold<p.cost)return "Insufficient gold.";if(Army.units.Count(u=>u.type==type)>=(p.unique?1:type=="clawrat"?4:2))return "Duplicate limit.";if(p.titan&&Army.units.Any(u=>data.P(u.type).titan))return "One titan per army.";
  
  if(mode=="conquest"){var r=provinces[Army.region];if(r.owner!=faction||!r.buildings.Contains("barracks"))return "Requires friendly barracks.";if(p.caster&&!r.buildings.Contains("sanctum"))return "Requires a sanctum.";if((p.titan||p.kind=="vehicle"||p.kind=="artillery")&&r.tier<3)return "War engines require settlement tier 3.";if(stock.First(s=>s.type==type).count<1)return "Recruitment stock refills next turn.";}return null;
 }
 public bool Recruit(string type){var error=RecruitError(type);if(error!=null){Note(error);return false;}gold-=data.P(type).cost;if(mode=="conquest")stock.First(s=>s.type==type).count--;Army.units.Add(New(type));Note(data.P(type).name+" recruited.");return true;}
 public bool Train(Veteran u){if(Army==null||!Army.units.Contains(u)||phase!="map"&&phase!="shop"&&phase!="camp"||u.level>=3||u.xp<2+2*u.level||gold<TrainingCost(u))return false;gold-=TrainingCost(u);u.xp-=2+2*u.level;u.level++;u.hp+=3;Note(data.P(u.type).name+" trained to rank "+u.level+".");return true;}
 public IEnumerable<Province> Adjacent(int id){var p=provinces[id];return provinces.Where(q=>Math.Abs(p.x-q.x)+Math.Abs(p.y-q.y)==1);}
 public bool Move(int id,bool siege=false){if(mode!="conquest"||phase!="map"||status!="active"||Army==null||Army.moved||!Adjacent(Army.region).Any(p=>p.id==id)||provinces[id].ruined>0)return false;var target=provinces[id];
  if(target.owner==faction){Army.region=id;Army.moved=true;Note("Army moved to "+target.name+".");return true;}
  attackTarget=id;defense=-1;Army.moved=true;if(siege&&target.tier>=2){siegeTarget=id;siegeArmy=Army.id;siegeTurns=2;phase="siege";Note("Siege begun. Advance two turns to weaken the defenders.");}else{phase="battle";pendingId="conquest-"+nextBattle++;settled=false;}return true;
 }
 public bool Build(string type){if(mode!="conquest"||phase!="map"||status!="active")return false;var r=provinces[selectedProvince];int cost=type=="market"?100:type=="fort"?150:type=="sanctum"?110:120;if(r.owner!=faction||r.buildings.Contains(type)||r.buildings.Count>=r.tier||gold<cost)return false;gold-=cost;r.buildings.Add(type);Note(type+" built in "+r.name+".");return true;}
 public bool RaiseArmy(string commander){if(mode!="conquest"||phase!="map"||status!="active"||armies.Count>=3)return false;if(progression!=null&&!progression.CanSelect(data,commander))return false;var r=provinces[selectedProvince];var p=data.P(commander);if(r.owner!=faction||!r.buildings.Contains("barracks")||p.faction!=faction||!p.commander||gold<p.cost+80)return false;gold-=p.cost+80;var a=new Expedition{id=nextArmy++,region=r.id,commanderType=commander};a.units.Add(New(commander));armies.Add(a);selectedArmy=a.id;Note("New expedition raised.");return true;}
 public bool Rest(){if(mode!="conquest"||phase!="map"||Army==null||Army.moved||gold<40)return false;gold-=40;Army.moved=true;Heal(.5f);return true;}
 public bool EndTurn(){if(mode!="conquest"||status!="active"||(phase!="map"&&phase!="siege"))return false;turn++;
  int income=provinces.Where(p=>p.owner==faction).Sum(p=>25+15*p.tier+(p.buildings.Contains("market")?35:0));int upkeep=armies.Sum(a=>a.units.Sum(u=>(data.P(u.type).cost+19)/20));gold=Math.Max(0,gold+income-upkeep);
  foreach(var p in provinces){p.ruined=Math.Max(0,p.ruined-1);if(p.owner!=""&&++p.growth>=3){p.growth=0;p.tier=Math.Min(5,p.tier+1);}}
  foreach(var a in armies){a.moved=false;foreach(var u in a.units)u.hp=Math.Min(data.P(u.type).hp+u.level*3,u.hp+(provinces[a.region].buildings.Contains("sanctum")?8:2));}
  foreach(var s in stock)s.count=Math.Min(data.P(s.type).unique?1:2,s.count+1);
  Note("Turn "+turn+": income "+income+", upkeep "+upkeep+".");
  // Rivals expand once per turn; defended invasions become tactical battles.
  foreach(string rival in rivals){var origins=provinces.Where(p=>p.owner==rival).ToList();var ledger=ledgers.First(l=>l.faction==rival);ledger.gold+=origins.Sum(p=>20+p.tier*10)-origins.Count*15;if(ledger.gold<60)continue;var frontier=origins.SelectMany(p=>Adjacent(p.id)).Where(p=>p.owner!=rival&&p.ruined==0).Distinct().OrderBy(p=>Random()).ToList();if(frontier.Count==0)continue;ledger.gold-=60;var target=frontier[0];if(target.owner==faction&&armies.Any(a=>a.region==target.id)){if(!invasionQueue.Any(q=>q.province==target.id))invasionQueue.Add(new Invasion{province=target.id,attacker=rival});}else if(target.owner!=faction||Random()<.55f){target.owner=rival;Note(data.F(rival).name+" takes "+target.name+".");}}
  if(siegeTarget>=0){var besiegers=armies.FirstOrDefault(a=>a.id==siegeArmy);if(besiegers==null||!Adjacent(besiegers.region).Any(p=>p.id==siegeTarget)||provinces[siegeTarget].owner==faction){siegeTarget=-1;phase="map";}else if(--siegeTurns<=0){selectedArmy=siegeArmy;attackTarget=siegeTarget;phase="battle";pendingId="siege-"+nextBattle++;settled=false;}}
  if(phase!="battle"&&invasionQueue.Count>0)NextDefense();CheckVictory();return true;
 }
 public void NextDefense(){while(invasionQueue.Count>0){var invasion=invasionQueue[0];int id=invasion.province;invasionQueue.RemoveAt(0);var a=armies.FirstOrDefault(a=>a.region==id);if(a==null||provinces[id].owner!=faction)continue;selectedArmy=a.id;pendingEnemy=invasion.attacker;defense=id;attackTarget=id;phase="battle";pendingId="defense-"+nextBattle++;settled=false;Note("An invasion threatens "+provinces[id].name+".");return;}phase=siegeTarget>=0?"siege":"map";if(siegeTarget>=0)selectedArmy=siegeArmy;}
 public Battle MakeBattle(){if(phase!="battle"||Army==null)return null;string enemy;int count;
  if(mode=="trail"){enemy=data.factions[(int)(Random()*data.factions.Length)].id;if(enemy==faction)enemy=data.factions.First(f=>f.id!=faction).id;count=Math.Min(18,2+3*(act-1)+pendingRow+(eventKind=="Elite"?1:0)+(pendingBoss?2:0));}
  else{enemy=defense>=0?pendingEnemy:provinces[attackTarget].owner;if(enemy==""||enemy==faction)enemy=rivals.FirstOrDefault()??"compact";count=Math.Min(10,3+provinces[attackTarget].tier+(defense<0&&provinces[attackTarget].buildings.Contains("fort")?2:0));if(defense<0&&siegeTarget>=0&&siegeTurns<=0)count=Math.Max(3,count-2);}
  var list=data.F(enemy).defaults.Take(count).ToList();if(mode=="trail")while(list.Count<count)list.Add(data.F(enemy).defaults[1+(list.Count%(data.F(enemy).defaults.Length-1))]);if((mode=="trail"&&pendingBoss&&act==3)||(mode=="conquest"&&provinces[attackTarget].tier>=3)){var titan=enemy+"_titan";if(mode!="trail"&&list.Count>=10)list.RemoveAt(list.Count-1);list.Add(titan);}
  var b=Battle.Create(data,faction,Army.units.Select(u=>u.type).ToList(),enemy,list,seed++);b.campaignBattleId=pendingId;
  b.finalTrailBoss=mode=="trail"&&pendingBoss&&act==3;b.doctrine=doctrine;if(mode=="trail"&&pendingBoss){var boss=b.Living(1).First(u=>b.P(u).commander);boss.level=act;boss.hp=b.MaxHP(boss);b.Note(act==3?"FINAL SENTINEL: an elite commander and titan defend the end of the trail.":"ACT SENTINEL: an experienced commander blocks your path.");}
  var own=b.Living(0).ToList();for(int i=0;i<own.Count;i++){var v=Army.units[i];own[i].persistentId=v.id;own[i].hp=v.hp;own[i].level=v.level;own[i].xp=v.xp;if(mode=="conquest"&&defense>=0&&provinces[defense].buildings.Contains("fort"))own[i].ward=6;}
  float scale=difficulty=="easy"?.85f:difficulty=="hard"?1.15f:1;foreach(var u in b.Living(1))u.hp=Math.Max(1,(int)(u.hp*scale));EnsureChronicle();b.chronicleCampaignId=chronicleId;b.CaptureChronicleStart();return b;
 }
 bool SettleCore(Battle b){if(b==null||!b.finished||settled||b.campaignBattleId!=pendingId||Army==null)return false;settled=true;bool win=b.winner==0;var army=Army;
  army.units=army.units.Where(v=>b.units.Any(u=>u.persistentId==v.id&&u.Alive)).ToList();foreach(var v in army.units){var u=b.units.First(q=>q.persistentId==v.id);v.hp=u.hp;v.xp+=win?2+(PersonalWins>=9?1:0):1;}
  if(mode=="trail"){if(!win||!army.units.Any(u=>u.id==commanderId)){status="lost";phase="map";Note("The expedition is lost. The death trail ends here.");}else{victories++;gold+=pendingBoss?180:eventKind=="Elite"?110:70;phase="reward";Note("Victory. Choose supplies or battlefield recovery.");}return true;}
  if(win){victories++;if(defense>=0){Note("The invasion is repelled.");defense=-1;NextDefense();}else{phase="capture";Note("Victory. Occupy the settlement or raze it for supplies.");}}
  else{if(army.units.Count==0)armies.Remove(army);else{var retreat=provinces.FirstOrDefault(p=>p.owner==faction&&p.id!=attackTarget);if(retreat!=null)army.region=retreat.id;else armies.Remove(army);}if(defense>=0){if(armies.Any(a=>a.region==defense))invasionQueue.Insert(0,new Invasion{province=defense,attacker=pendingEnemy});else provinces[defense].owner=pendingEnemy;}defense=-1;if(army.id==siegeArmy)siegeTarget=-1;selectedArmy=armies.FirstOrDefault()?.id??0;NextDefense();Note("Your force withdraws; losses persist.");}
  CheckVictory();return true;
 }
 bool CaptureCore(bool raze){if(phase!="capture"||Army==null)return false;var p=provinces[attackTarget];if(raze){p.owner="";p.ruined=2;p.tier=1;p.buildings.Clear();gold+=130;}else{p.owner=faction;Army.region=p.id;}siegeTarget=-1;attackTarget=-1;NextDefense();CheckVictory();return true;}
 public void CheckVictory(){if(mode!="conquest")return;int owned=provinces.Count(p=>p.owner==faction);if(owned==0){status="lost";phase="map";Note("Your last settlement has fallen.");return;}if(victories>=2&&!battleReward){battleReward=true;gold+=150;Note("Minor objective completed: two victories (+150 gold).");}if(owned>=3&&!territoryReward){territoryReward=true;gold+=150;Note("Minor objective completed: three settlements (+150 gold).");}if((battleReward&&territoryReward&&owned>=10)||!provinces.Any(p=>rivals.Contains(p.owner))){status="won";phase="map";Note("The frontier is secured. Your dominion has prevailed.");}}
}
[Serializable] public class SaveGame {public int version=1;public Campaign campaign;public Battle battle;}
}

