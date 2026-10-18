using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QuietDread {
[Serializable] public class Faction { public string id,name,color,motto,description,trait,source,lore,history; public string[] defaults; }
[Serializable] public class Profile {
 public string id,name,faction,role,kind,ability,weapon,rule,lore,visual;
 public int cost,hp,move,range,power,armour,penetration,melee,accuracy=75,oc=2,blast,minRange,page;
 public bool commander,unique,caster,indirect,ignoreCover,stealth,gas,tunnel,titan,flying,amphibious;
}
[Serializable] public class Content {
 public Faction[] factions; public Profile[] units; public string setting;
 [NonSerialized] public Dictionary<string,Profile> profiles;
 public void Init(){profiles=units.ToDictionary(p=>p.id);}
 public Profile P(string id){return profiles[id];} public Faction F(string id){return factions.First(f=>f.id==id);}
 public static Content Load(){var d=JsonUtility.FromJson<Content>(Resources.Load<TextAsset>("Content").text);d.Init();return d;}
}
[Serializable] public class Soldier {
 public int id,team,x,y,hp,ap=2,suppression,pressure,ward,level,xp,kills,slow,rooted,veil,order;
 public int markedBy=-1,lastHitRound=-1;
 public string type, persistentId;
 public bool moved,attacked,setup,spawned;
 public bool Alive {get{return hp>0;}}
}
[Serializable] public partial class Battle {
 public const int W=18,H=12;
 public int width=18,height=12; public string deploymentSide="west",biome="woodland"; public bool fieldConfigured;
 public List<Soldier> units=new List<Soldier>(); public int[] terrain=new int[W*H];
 public int[] score=new int[2],aether={6,6},biomass=new int[2],salvage=new int[2],memory=new int[2];
 public string[] factions=new string[2]; public string doctrine="thunder";
 public bool[] broodUsed=new bool[2];
 public bool finalTrailBoss;
 public int turn,round=1,nextId=1,winner=-1; public uint seed=42; public bool deployment=true,finished;
 public string message="Deploy your formations in the blue zone. Select a unit, then select an empty deployment cell.";
 public List<string> log=new List<string>(); public string campaignBattleId;
 [NonSerialized] public Content data;
 public static readonly Vector2Int[] Relays={new Vector2Int(8,2),new Vector2Int(9,6),new Vector2Int(8,9)};
 public Profile P(Soldier u){return data.P(u.type);}
 public int MaxHP(Soldier u){return P(u).hp+3*u.level;}
 public static int Dist(int x,int y,int a,int b){return Math.Abs(x-a)+Math.Abs(y-b);}
 public int D(Soldier a,Soldier b){return Dist(a.x,a.y,b.x,b.y);}
 public bool Contains(int x,int y){return x>=0&&x<width&&y>=0&&y<height;}
 public int Tile(int x,int y){return Contains(x,y)?terrain[y*width+x]:1;}
 public Soldier At(int x,int y){return units.FirstOrDefault(u=>u.Alive&&u.x==x&&u.y==y);}
 public IEnumerable<Soldier> Living(int t){return units.Where(u=>u.Alive&&u.team==t);}
 public float Random(){seed=unchecked(seed*1664525u+1013904223u);return (seed>>8)/16777216f;}
 public int RollD100(){return 1+(int)(Random()*100);}
 public void Note(string text){message=text;log.Insert(0,text);if(log.Count>45)log.RemoveAt(log.Count-1);}
 public static string Validate(Content d,string f,List<string> list){
  if(list.Count<4||list.Count>10)return "Choose 4–10 formations.";
  if(list.Any(t=>!d.profiles.ContainsKey(t)||d.P(t).faction!=f))return "Only formations belonging to your faction are allowed.";
  if(!list.Any(t=>d.P(t).commander))return "An army needs a commander.";
  if(list.Sum(t=>d.P(t).cost)>1600)return "Army exceeds 1,600 points.";
  if(list.Count(t=>d.P(t).titan)>1)return "One titan per army.";
  foreach(var g in list.GroupBy(t=>t))if(g.Count()>(d.P(g.Key).unique?1:g.Key=="clawrat"?4:2))return "Duplicate limit reached for "+d.P(g.Key).name;
  return null;
 }
 public static bool Inside(int x,int y){return x>=0&&x<W&&y>=0&&y<H;}
 public Vector2Int[] Objectives {get{return width==18&&height==12?Relays:(deploymentSide=="north"||deploymentSide=="south")?new[]{new Vector2Int(width/4,height/2),new Vector2Int(width/2,height/2),new Vector2Int(width*3/4,height/2)}:new[]{new Vector2Int(width/2,height/4),new Vector2Int(width/2,height/2),new Vector2Int(width/2,height*3/4)};}}
 public static Battle Create(Content d,string f,List<string> roster,string enemy,List<string> opponents=null,uint seed=42){
  var s=new Battle{data=d,seed=seed,factions=new[]{f,enemy}};
  for(int y=0;y<H;y++)for(int x=4;x<W-4;x++) {float r=s.Random();s.terrain[y*W+x]=r<.1f?1:r<.22f?2:r<.29f?3:0;}
  foreach(var r in Relays)for(int y=r.y-1;y<=r.y+1;y++)for(int x=r.x-1;x<=r.x+1;x++)if(Inside(x,y))s.terrain[y*W+x]=0;
  // Two continuous avenues ensure both armies can reach the centre.
  for(int x=0;x<W;x++){s.terrain[5*W+x]=0;s.terrain[6*W+x]=0;}
  for(int t=0;t<2;t++){var list=t==0?roster:(opponents??d.F(enemy).defaults.ToList());for(int i=0;i<list.Count;i++)s.Add(list[i],t,t==0?1+(i/5)*2:W-2-(i/5)*2,1+(i%5)*2);}
  return s;
 }
 public Soldier Add(string type,int team,int x,int y,bool spawned=false){var p=data.P(type);var u=new Soldier{id=nextId++,type=type,team=team,x=x,y=y,hp=p.hp,spawned=spawned};units.Add(u);return u;}
 public bool Deploy(Soldier u,int x,int y){if(!deployment||u==null||u.team!=0||!InDeployment(0,x,y)||!Contains(x,y)||!Passable(P(u),x,y)||At(x,y)!=null)return false;u.x=x;u.y=y;return true;}
 public void Begin(){deployment=false;foreach(var watcher in units.Where(q=>q.type=="seraphyx")){var victim=Living(1-watcher.team).FirstOrDefault();if(victim!=null)victim.pressure++;}Note("Battle begins. Capture relays or destroy the opposing army.");StartTurn();}
 public bool LOS(int x,int y,int tx,int ty){int n=Math.Max(Math.Abs(tx-x),Math.Abs(ty-y))*3;for(int i=1;i<n;i++){int a=Mathf.RoundToInt(Mathf.Lerp(x,tx,i/(float)n)),b=Mathf.RoundToInt(Mathf.Lerp(y,ty,i/(float)n));if((a!=x||b!=y)&&(a!=tx||b!=ty)&&BlocksSight(Tile(a,b)))return false;}return true;}
 public bool NearCommander(Soldier u,int range=3){return Living(u.team).Any(q=>P(q).commander&&D(u,q)<=range);}
 public int Move(Soldier u){if(u.rooted>0)return 0;return Math.Max(1,P(u).move-(u.slow>0?2:0));}
 public Dictionary<int,int> Reach(Soldier u,int maximum=-1){
  var costs=new Dictionary<int,int>{{u.y*width+u.x,0}};var queue=new Queue<int>();queue.Enqueue(u.y*width+u.x);int cap=maximum<0?Move(u):maximum;var profile=P(u);
  while(queue.Count>0){int v=queue.Dequeue(),x=v%width,y=v/width;foreach(var step in Steps){int a=x+step.x,b=y+step.y,k=b*width+a;if(!Contains(a,b)||(!profile.flying&&(!Passable(profile,a,b)||(At(a,b)!=null&&At(a,b)!=u))))continue;int c=costs[v]+MoveCost(profile,a,b);if(c<=cap&&(!costs.ContainsKey(k)||c<costs[k])){costs[k]=c;queue.Enqueue(k);}}}
  return costs.Where(pair=>(pair.Key==u.y*width+u.x)||(Passable(profile,pair.Key%width,pair.Key/width)&&At(pair.Key%width,pair.Key/width)==null)).ToDictionary(pair=>pair.Key,pair=>pair.Value);
 }
 public int Armour(Soldier u){var p=P(u);int n=p.armour;if(p.faction=="vault"&&!u.moved)n++;if(p.id=="stormvermin"&&Objectives.Any(r=>Dist(r.x,r.y,u.x,u.y)<=1))n++;if((p.faction=="argent"||p.faction=="kiln"||p.faction=="saurian")&&NearCommander(u))n++;return n;}
 public int Chance(Soldier u,Soldier target){var p=P(u);int n=p.accuracy+u.level*2+(u.order>0?15:0)-u.suppression*5;
  if(!p.ignoreCover)n-=Cover(u,target);
  if((P(target).stealth&&D(u,target)>3)||target.veil>0)n-=20;
  if(P(target).titan)n+=10;if(p.faction=="argent"&&!u.moved)n+=10;if(target.markedBy==u.team)n+=10;
  return Mathf.Clamp(n,15,95);
 }
 public string ShotError(Soldier u,Soldier target){if(u==null||target==null||!u.Alive||!target.Alive||u.team==target.team)return "Choose a living enemy.";if(u.attacked||u.ap<1)return "This formation has already attacked or has no AP.";int d=D(u,target);var p=P(u);if(d<=1)return null;if(d>p.range)return "Out of range.";if(d<p.minRange)return "Inside minimum range.";if((p.kind=="artillery"||p.ability=="siege")&&!u.setup)return "Brace this artillery before firing.";if(!p.indirect&&!LOS(u.x,u.y,target.x,target.y))return "Line of sight is blocked by terrain.";return null;}
 public bool CanAct(Soldier u){return !finished&&!deployment&&u!=null&&u.Alive&&u.team==turn&&u.ap>0;}
 public void Hurt(Soldier target,int amount,Soldier source=null,bool bypass=false){if(!target.Alive)return;if(target.type=="tank"&&target.lastHitRound!=round){amount=Math.Max(0,amount-2);target.lastHitRound=round;}int shield=Math.Min(target.ward,amount);target.ward-=shield;amount-=shield;target.hp=Math.Max(0,target.hp-amount);if(target.hp==0){int enemy=1-target.team;biomass[enemy]++;salvage[enemy]++;memory[enemy]=Math.Min(3,memory[enemy]+1);Note(P(target).name+" destroyed.");if(target.type=="clawrat"&&Objectives.Any(r=>Dist(r.x,r.y,target.x,target.y)<=1)){var sensor=Living(enemy).Where(q=>D(q,target)<=3).OrderBy(q=>D(q,target)).FirstOrDefault();if(sensor!=null)sensor.pressure++;}if(source!=null){source.kills++;if(P(source).faction=="oathscar")source.hp=Math.Min(MaxHP(source),source.hp+4);if(P(source).faction=="pelagic")source.hp=Math.Min(MaxHP(source),source.hp+3);if(P(source).faction=="hollow"||P(source).faction=="kiln")aether[source.team]=Math.Min(12,aether[source.team]+1);}}}
 public int DamagePreview(Soldier u,Soldier target,bool melee=false){var p=P(u);int dmg=(melee?p.melee:p.power)+u.level;
  if(melee&&u.moved&&(p.faction=="cinder"||p.faction=="horn"||p.faction=="kiln"))dmg+=3;
  if(p.faction=="rift")dmg+=Math.Min(3,u.kills);if(p.faction=="oathscar")dmg+=2;if(p.faction=="pelagic"&&NearCommander(u))dmg++;if(p.faction=="vesper"&&target.hp<MaxHP(target))dmg+=2;
  return Math.Max(1,dmg-Math.Max(0,Armour(target)-p.penetration));}
 public bool Act(Soldier u,string action,int x,int y){
  string unavailable=ActionUnavailable(u,action);if(unavailable!=null){Note(unavailable);return false;}var p=P(u);var target=At(x,y);
  if(action=="move") {if(u.ap<1||!Reach(u).ContainsKey(y*width+x)||!Contains(x,y)||(x==u.x&&y==u.y))return false;u.x=x;u.y=y;u.ap--;u.moved=true;u.setup=false;return true;}
  if(action=="rally"){if(u.suppression==0&&u.pressure==0)return false;u.suppression=Math.Max(0,u.suppression-2);u.pressure=Math.Max(0,u.pressure-1);u.ap--;Note(p.name+" rallies: removes up to 2 suppression and 1 Pressure. Spent AP is not restored.");return true;}
  if(action=="brace"){if(p.kind!="artillery"&&p.ability!="siege")return false;u.setup=true;u.ap--;Note(p.name+" braced for bombardment.");return true;}
  if(action=="fire"){
   string error=ShotError(u,target);if(error!=null){Note(error);return false;}bool melee=D(u,target)<=1;u.ap--;u.attacked=true;
   int threshold=Chance(u,target),roll=melee?0:RollD100();string dice=roll.ToString();bool hit=melee||roll<=threshold;if(!hit&&(p.faction=="lumen"||p.faction=="vesper")){roll=RollD100();dice+=" → "+roll;hit=roll<=threshold;}
   Note(p.name+(hit?" hits ":" misses ")+P(target).name+(melee?" (melee)": " (d100 "+dice+" ≤ "+threshold+")")+(hit?"; damage "+DamagePreview(u,target,melee)+" before wards.":"."));if(hit){int damage=DamagePreview(u,target,melee);Hurt(target,damage,u);if(p.faction=="compact")target.markedBy=u.team;target.suppression+=p.titan?2:1;if(p.faction=="vermin"){target.pressure++;if(p.indirect&&doctrine=="thunder")target.suppression++;}if(p.gas)target.slow=2;
    if(!melee&&p.blast>0)foreach(var q in units.Where(q=>q.Alive&&q!=target&&Dist(q.x,q.y,x,y)<=p.blast).ToList())Hurt(q,Math.Max(1,damage/2),u);
    if(melee&&target.Alive)Hurt(u,RetaliationDamage(target,u),target);
   }if(p.faction=="oathscar"&&u.Alive)Hurt(u,1);CheckEnd();return true;
  }
  if(action=="charge"){
   if(u.ap<2||u.attacked||target==null||target.team==u.team||!(p.ability=="charge"||p.ability=="stampede"))return false;
   var reach=Reach(u,p.ability=="stampede"?6:Move(u)+1);var cells=reach.Keys.Where(k=>Dist(k%width,k/width,x,y)==1).OrderBy(k=>reach[k]).ToList();if(cells.Count==0)return false;int cell=cells[0];u.x=cell%width;u.y=cell/width;u.moved=true;u.ap=0;u.attacked=true;Hurt(target,p.ability=="stampede"?28:DamagePreview(u,target,true)+4,u);if(target.Alive)Hurt(u,RetaliationDamage(target,u),target);Note(p.name+" charges.");CheckEnd();return true;
  }
  if(action=="orders") {if(!p.commander||target==null||target.team!=u.team||D(u,target)>4||target.order>0)return false;target.order=1;u.ap--;Note("Orders: +15 accuracy and +1 control for "+P(target).name);return true;}
  if(action=="memory"){if(p.faction!="vermin"||(!p.commander&&!p.caster)||target==null||target.team==u.team||D(u,target)>6||memory[u.team]<1)return false;memory[u.team]--;target.markedBy=u.team;target.pressure++;u.ap--;Note("Harvested memory exposes the target: +10 allied accuracy and +1 Pressure.");return true;}
  if(action=="brood"){if(p.faction!="vermin"||!p.commander||doctrine!="brood"||broodUsed[u.team]||biomass[u.team]<2)return false;var fallen=units.FirstOrDefault(q=>q.team==u.team&&!q.Alive&&q.type=="clawrat"&&!q.spawned);if(fallen==null)return false;foreach(var v in new[]{new Vector2Int(1,0),new Vector2Int(0,1),new Vector2Int(-1,0),new Vector2Int(0,-1)}){int bx=u.x+v.x,by=u.y+v.y;if(Contains(bx,by)&&Passable(P(fallen),bx,by)&&At(bx,by)==null){fallen.x=bx;fallen.y=by;fallen.hp=MaxHP(fallen)/2;fallen.ap=0;biomass[u.team]-=2;broodUsed[u.team]=true;u.ap--;Note("Chitterhold restores a fallen Clawrat formation at half strength.");return true;}}return false;}
  if(action=="repair"){if(p.ability!="repair"||target==null||target.team!=u.team||D(u,target)>3||!(P(target).kind=="vehicle"||P(target).titan)||target.hp>=MaxHP(target))return false;int n=salvage[u.team]>0?11:7;if(salvage[u.team]>0)salvage[u.team]--;target.hp=Math.Min(MaxHP(target),target.hp+n);u.ap--;Note("Field repairs restore "+n+" HP.");return true;}
  if(action=="fortify"){if(p.ability!="fortify"||Tile(u.x,u.y)==5)return false;terrain[u.y*width+u.x]=2;u.ap--;return true;}
  if(action=="sabotage"){if(p.ability!="sabotage"||target==null||target.team==u.team||D(u,target)>4)return false;target.pressure+=2;target.suppression+=2;u.ap--;Note("Sabotage disrupts enemy orders.");return true;}
  if(action=="tunnel"){if(!(p.tunnel||(p.faction=="vermin"&&p.kind=="infantry"&&doctrine=="deep"))||u.ap<(doctrine=="deep"?1:2)||!Contains(x,y)||At(x,y)!=null||!Passable(p,x,y)||Tile(x,y)==5||!Objectives.Any(r=>Dist(r.x,r.y,x,y)<=1))return false;u.x=x;u.y=y;u.ap-=doctrine=="deep"?1:2;u.moved=true;return true;}
  if(action=="lance"||action=="terror"||action=="ward"||action=="heal"||action=="soulfire"){
   if(!p.caster||target==null||D(u,target)>6||u.attacked)return false;bool friendly=action=="ward"||action=="heal";if(friendly!=(target.team==u.team))return false;int cost=action=="ward"?(p.faction=="dawn"?1:2):3;if(aether[u.team]<cost){Note("Not enough Aether.");return false;}if(!friendly&&!LOS(u.x,u.y,x,y))return false;
   if(action=="heal"&&target.hp>=MaxHP(target))return false;
   aether[u.team]-=cost;u.ap--;u.attacked=true;if(action=="ward")target.ward+=6;else if(action=="heal")target.hp=Math.Min(MaxHP(target),target.hp+8);else{Hurt(target,action=="terror"?4:action=="soulfire"?7:10,u);if(action=="terror")target.suppression+=2;if(action=="soulfire")u.hp=Math.Min(MaxHP(u),u.hp+5);}Note(p.name+" invokes "+action+".");CheckEnd();return true;
  }
  if(action=="signature")return Titan(u,x,y);
  return false;
 }
 public bool Spawn(Soldier u,string type){if(units.Count(q=>q.Alive&&q.spawned&&q.team==u.team)>=6)return false;foreach(var v in new[]{new Vector2Int(1,0),new Vector2Int(0,1),new Vector2Int(0,-1),new Vector2Int(-1,0)}){int x=u.x+v.x,y=u.y+v.y;if(Contains(x,y)&&At(x,y)==null&&Passable(data.P(type),x,y)){var q=Add(type,u.team,x,y,true);q.ap=0;Note(P(u).name+" births "+data.P(type).name+". The new formation acts next turn.");return true;}}return false;}
 public bool Titan(Soldier u,int x,int y){var p=P(u);if(!p.titan||u.ap<2||u.attacked)return false;var target=At(x,y);var friends=Living(u.team).Where(q=>D(q,u)<=4).ToList();var foes=Living(1-u.team).Where(q=>D(q,u)<=4).ToList();
  switch(p.ability){
   case "birth":if(!Spawn(u,"clawrat"))return false;break;
   case "shield":case "aegis":foreach(var q in friends){q.ward+=p.ability=="aegis"?8:6;q.suppression=0;}break;
   case "stomp":foreach(var q in foes.Where(q=>D(q,u)<=2)){Hurt(q,18,u);q.suppression+=2;}break;
   case "siege":return Act(u,"brace",x,y);
   case "beam":if(!Contains(x,y))return false;foreach(var q in Living(1-u.team).Where(q=>q.y==y).ToList())Hurt(q,16,u);break;
   case "veil":if(!Contains(x,y)||At(x,y)!=null||!Passable(p,x,y)||Dist(u.x,u.y,x,y)>6)return false;u.x=x;u.y=y;foreach(var q in Living(u.team).Where(q=>D(q,u)<=3))q.veil=2;break;
   case "raise":var dead=units.FirstOrDefault(q=>!q.Alive&&q.team==u.team&&!P(q).titan&&!q.spawned);if(dead==null)return false;var cell=Reach(u,1).Keys.FirstOrDefault(k=>At(k%width,k/width)==null&&Passable(P(dead),k%width,k/width));if(At(cell%width,cell/width)!=null||!Contains(cell%width,cell/width)||Dist(u.x,u.y,cell%width,cell/width)>1||!Passable(P(dead),cell%width,cell/width))return false;dead.x=cell%width;dead.y=cell/width;dead.hp=MaxHP(dead)/2;dead.ap=0;break;
   case "stampede":return Act(u,"charge",x,y);
   case "riftstorm":if(aether[u.team]<3)return false;aether[u.team]-=3;foreach(var q in foes)Hurt(q,10,u);Hurt(u,5);break;
   case "roots":foreach(var q in foes)q.rooted=2;foreach(var q in friends)q.hp=Math.Min(MaxHP(q),q.hp+6);break;
   case "solar":foreach(var q in foes.Where(q=>D(q,u)<=3))Hurt(q,14,u);aether[u.team]=Math.Min(12,aether[u.team]+2);break;
   case "sanctuary":foreach(var q in friends){q.hp=Math.Min(MaxHP(q),q.hp+12);q.suppression=0;q.pressure=0;}break;
   case "siphon":foreach(var q in foes){int before=q.hp;Hurt(q,8,u);u.hp=Math.Min(MaxHP(u),u.hp+before-q.hp);}break;
   case "contract":if(target==null||target.team==u.team||D(u,target)>8||u.hp<=8)return false;Hurt(u,8);Hurt(target,20,u);if(!target.Alive)u.hp=Math.Min(MaxHP(u),u.hp+12);break;
   case "digest":if(biomass[u.team]<1)return false;biomass[u.team]--;u.hp=Math.Min(MaxHP(u),u.hp+16);Spawn(u,"pelagic_0");break;
   case "overload":if(target==null||target.team==u.team||D(u,target)>10||u.hp<=6)return false;Hurt(u,6);foreach(var q in units.Where(q=>q.Alive&&Dist(q.x,q.y,x,y)<=1).ToList())Hurt(q,20,u);break;
   default:return false;
  }
  u.ap=0;u.attacked=true;Note(p.name+": "+p.ability+" unleashed.");CheckEnd();return true;
 }
 public int Control(int team,Vector2Int relay){return Living(team).Where(q=>Dist(q.x,q.y,relay.x,relay.y)<=1).Sum(q=>P(q).oc+(q.order>0?1:0)+(P(q).faction=="saurian"&&NearCommander(q)?1:0));}
 public void StartTurn(){aether[turn]=Math.Min(12,aether[turn]+2);foreach(var u in Living(turn).ToList()){var p=P(u);u.markedBy=-1;if(p.faction=="dawn"&&Living(turn).Any(q=>P(q).caster&&D(u,q)<=3))u.suppression=Math.Max(0,u.suppression-2);u.ap=p.faction!="sepulchre"&&u.suppression>=(p.faction=="cinder"?2:3)?1:2;u.suppression=Math.Max(0,u.suppression-1);if(p.faction=="pelagic"&&NearCommander(u))u.ap=2;u.moved=false;u.attacked=false;u.order=0;u.slow=Math.Max(0,u.slow-1);u.rooted=Math.Max(0,u.rooted-1);u.veil=Math.Max(0,u.veil-1);if(u.pressure>=3){u.ap=Math.Max(0,u.ap-1);u.pressure=0;}if(p.faction=="sepulchre"&&Living(turn).Any(q=>P(q).caster&&D(u,q)<=3))u.hp=Math.Min(MaxHP(u),u.hp+2);if(p.ability=="birth")Spawn(u,"clawrat");}}
 public void EndTurn(){if(finished||deployment)return;if(turn==1){foreach(var r in Objectives){int a=Control(0,r),b=Control(1,r);if(a>b)score[0]+=2;else if(b>a)score[1]+=2;}round++;}turn=1-turn;CheckEnd();if(!finished)StartTurn();}
 public void CheckEnd(){if(deployment)return;if(finalTrailBoss&&Living(0).Any()){bool guardians=Living(1).Any(u=>P(u).commander||P(u).titan);if(!guardians){finished=true;winner=0;Note("The final sentinel and its titan fall. The Trail is conquered.");return;}if(round>24){finished=true;winner=1;Note("The final guardians endure. Your expedition must withdraw.");return;}return;}bool a=Living(0).Any(),b=Living(1).Any();if(!a||!b){finished=true;winner=a?0:b?1:2;}else if(score[0]>=30||score[1]>=30||round>16){finished=true;winner=score[0]==score[1]?2:score[0]>score[1]?0:1;}if(finished)Note(winner==2?"The battle ends in a draw.":winner==0?"Victory. The field is yours.":"Defeat. Your expedition must withdraw.");}
 public bool AI(){if(finished||deployment)return false;var actor=Living(turn).FirstOrDefault(u=>u.ap>0);if(actor==null){EndTurn();return false;}var p=P(actor);var enemies=Living(1-turn).ToList();if(enemies.Count==0){CheckEnd();return false;}var nearest=enemies.OrderBy(q=>D(actor,q)).First();
  if(TrySupportAction(actor))return true;
  if(actor.ap>=2&&actor.suppression>=2&&Act(actor,"rally",actor.x,actor.y))return true;
  if(p.faction=="vermin"&&p.commander&&doctrine=="brood"&&Act(actor,"brood",actor.x,actor.y))return true;
  if(p.faction=="vermin"&&(p.commander||p.caster)&&actor.ap>1&&memory[turn]>0&&nearest.markedBy!=turn&&Act(actor,"memory",nearest.x,nearest.y))return true;
  if(p.titan&&actor.ap>=2&&!actor.attacked){bool useful=false;switch(p.ability){case "birth":useful=units.Count(q=>q.Alive&&q.spawned&&q.team==turn)<6;break;case "shield":case "aegis":useful=Living(turn).Any(q=>D(q,actor)<=4&&q.ward<3)&&D(actor,nearest)<9;break;case "sanctuary":case "roots":useful=Living(turn).Any(q=>D(q,actor)<=4&&q.hp<MaxHP(q)-6);break;case "raise":useful=units.Any(q=>!q.Alive&&q.team==turn&&!P(q).titan);break;case "digest":useful=actor.hp<MaxHP(actor)-12&&biomass[turn]>0;break;case "beam":case "contract":case "overload":case "stampede":useful=D(actor,nearest)<=8;break;case "riftstorm":case "siphon":case "solar":case "stomp":useful=D(actor,nearest)<=3;break;}if(useful&&Act(actor,"signature",nearest.x,nearest.y))return true;}
  if(p.caster&&!actor.attacked&&aether[turn]>=3&&D(actor,nearest)<=6&&Act(actor,"lance",nearest.x,nearest.y))return true;
  if((p.kind=="artillery"||p.ability=="siege")&&!actor.setup&&enemies.Any(q=>D(actor,q)<=p.range&&D(actor,q)>=p.minRange)&&Act(actor,"brace",actor.x,actor.y))return true;
  var target=enemies.Where(q=>ShotError(actor,q)==null).OrderByDescending(q=>TargetValue(actor,q)).FirstOrDefault();if(target!=null&&Act(actor,"fire",target.x,target.y))return true;
  if(p.ability=="charge"&&Act(actor,"charge",nearest.x,nearest.y))return true;
  var reach=Reach(actor);int best=actor.y*width+actor.x;float value=float.MaxValue;
  foreach(var cell in reach.Keys){int x=cell%width,y=cell/width;float v=PositionValue(actor,x,y,nearest);if(v<value){value=v;best=cell;}}
  if(best!=actor.y*width+actor.x&&Act(actor,"move",best%width,best/width))return true;
  actor.ap=0;return true;
 }
}
}


