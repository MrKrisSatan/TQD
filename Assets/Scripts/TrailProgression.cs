using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace QuietDread {
[Serializable] public class FactionProgress { public string faction; public int completions; }
[Serializable] public class CommanderProgress { public string commander; public int completions; public List<string> unlocked=new List<string>(); }
[Serializable] public class CommanderReward { public int wins; public string unit; }
[Serializable] public class CommanderTrack { public string commander,faction; public List<CommanderReward> rewards=new List<CommanderReward>(); }
[Serializable] public class CommanderRewardCatalog {
 public List<CommanderTrack> tracks=new List<CommanderTrack>();
 static CommanderRewardCatalog cached;
 public static CommanderRewardCatalog Load(){if(cached==null)cached=JsonUtility.FromJson<CommanderRewardCatalog>(Resources.Load<TextAsset>("CommanderRewards").text);return cached;}
 public CommanderTrack Track(string commander){return tracks.First(t=>t.commander==commander);}
}
[Serializable] public class TrailProgression {
 public int version=2;
 // Retained for migration and faction totals; never used to authorize commander rewards.
 public List<FactionProgress> factions=new List<FactionProgress>();
 public List<string> completedRuns=new List<string>(),unlockedIds=new List<string>();
 public List<CommanderProgress> commanders=new List<CommanderProgress>();
 public string migrationNote="";
 public int Wins(string faction){return factions.FirstOrDefault(f=>f.faction==faction)?.completions??0;}
 public int CommanderWins(string commander){return commanders.FirstOrDefault(c=>c.commander==commander)?.completions??0;}
 public static string Primary(Content d,string faction){return d.F(faction).defaults.First(id=>d.P(id).commander);}
 public static int Required(Content d,Profile p,string commander=null){
  commander=commander??Primary(d,p.faction);
  if(p.commander)return p.id==commander?0:2;
  var reward=CommanderRewardCatalog.Load().Track(commander).rewards.FirstOrDefault(r=>r.unit==p.id);
  return reward?.wins??0;
 }
 public bool CanSelect(Content d,string commander){var p=d.P(commander);return p.commander&&(commander==Primary(d,p.faction)||commanders.Any(c=>d.P(c.commander).faction==p.faction&&c.completions>=2)||commanders.Any(c=>c.unlocked.Contains(commander)));}
 public bool Allows(Content d,string type,string commander=null){
  var p=d.P(type);commander=commander??Primary(d,p.faction);
  if(!d.profiles.ContainsKey(commander)||!d.P(commander).commander||d.P(commander).faction!=p.faction)return false;
  if(type==commander)return CanSelect(d,commander);
  var owner=commanders.FirstOrDefault(c=>c.commander==commander);
  return (owner!=null&&owner.unlocked.Contains(type))||CommanderWins(commander)>=Required(d,p,commander);
 }
 public string LockReason(Content d,string type,string commander=null){var p=d.P(type);commander=commander??Primary(d,p.faction);return Allows(d,type,commander)?null:"Only "+d.P(commander).name+" may earn this reward: "+Required(d,p,commander)+" Trail wins ("+CommanderWins(commander)+" earned).";}
 public void Migrate(Content d){
  if(version==2)return;if(version!=1)throw new IOException("Unsupported progression version; files preserved.");
  commanders=new List<CommanderProgress>();
  foreach(var f in factions){var owner=new CommanderProgress{commander=Primary(d,f.faction),completions=f.completions};owner.unlocked=unlockedIds.Where(id=>d.profiles.ContainsKey(id)&&d.P(id).faction==f.faction).Distinct().ToList();commanders.Add(owner);}
  foreach(var id in unlockedIds.Where(d.profiles.ContainsKey)){string primary=Primary(d,d.P(id).faction);var owner=commanders.FirstOrDefault(c=>c.commander==primary);if(owner==null){owner=new CommanderProgress{commander=primary};commanders.Add(owner);}if(!owner.unlocked.Contains(id))owner.unlocked.Add(id);}
  // Version 1 recorded run IDs but no per-run commander ownership: do not invent that history.
  migrationNote="Legacy faction rewards were assigned to each faction's original starting commander; old saves did not record their earning commander. Other commanders begin independent tracks.";version=2;
 }
 public bool Record(Campaign c){
  if(c==null||c.mode!="trail"||c.status!="won"||c.act!=3||!c.nodes.Any(n=>n.act==3&&n.kind=="Boss"&&n.visited))return false;
  Migrate(c.data);c.EnsureChronicle();if(completedRuns.Contains(c.chronicleId))return false;
  string leader=c.CommanderFor(c.Army);var owner=commanders.FirstOrDefault(p=>p.commander==leader);if(owner==null){owner=new CommanderProgress{commander=leader};commanders.Add(owner);}owner.completions++;
  var f=factions.FirstOrDefault(p=>p.faction==c.faction);if(f==null){f=new FactionProgress{faction=c.faction};factions.Add(f);}f.completions++;completedRuns.Add(c.chronicleId);
  foreach(var p in c.data.units.Where(p=>p.faction==c.faction&&Required(c.data,p,leader)>0&&Required(c.data,p,leader)<=owner.completions))if(!owner.unlocked.Contains(p.id))owner.unlocked.Add(p.id);
  string reward=RewardText(c.data,leader,owner.completions);
  c.Note(c.data.P(leader).name+" completed the Trail "+owner.completions+" time(s). "+(reward==null?"All current personal rewards earned.":"Personal legacy earned: "+reward+". Rewards belong to this commander in both campaigns."));c.lastChronicleChoice=c.message;return true;
 }
 public static string RewardText(Content d,string commander,int wins){
  if(wins==2)return "Command retinue: recruit alternate commanders; discovered leaders begin independent legacies";
  if(wins==4)return "Supply warrant: +50 starting campaign gold";
  if(wins==6)return "Drill charter: training costs 10 less gold";
  if(wins==8)return "Field surgeons: recovery restores an extra 10% maximum HP";
  if(wins==9)return "Veteran command: surviving formations gain +1 XP on victory";
  var r=CommanderRewardCatalog.Load().Track(commander).rewards.FirstOrDefault(x=>x.wins==wins);return r==null?null:d.P(r.unit).name+" recruitment";
 }
 [NonSerialized] public bool recoveredFromBackup;
 static TrailProgression Decode(string raw){
  var p=JsonUtility.FromJson<TrailProgression>(raw);if(p!=null&&p.version>2)throw new NotSupportedException("Progression was written by a newer game version; files preserved.");
  if(p!=null&&p.version==1&&p.commanders==null)p.commanders=new List<CommanderProgress>();
  if(p==null||!raw.Contains("\"version\"")||!raw.Contains("\"completedRuns\"")||!raw.Contains("\"factions\"")||(p.version!=1&&p.version!=2)||p.factions==null||p.completedRuns==null||p.commanders==null||p.unlockedIds==null||p.factions.Any(f=>f==null||f.completions<0)||p.commanders.Any(c=>c==null||c.completions<0||c.unlocked==null))throw new IOException("Invalid progression data.");return p;
 }
 public static TrailProgression Load(string path){
  if(!File.Exists(path)&&!File.Exists(path+".bak"))return new TrailProgression();
  foreach(var candidate in new[]{path,path+".bak"})try{var p=Decode(File.ReadAllText(candidate));p.recoveredFromBackup=candidate!=path;return p;}catch(NotSupportedException){throw;}catch{}
  throw new IOException("Progression save and backup could not be read. Existing files have been preserved.");
 }
 public void Save(string path){
  Directory.CreateDirectory(Path.GetDirectoryName(path));bool valid=false;
  if(File.Exists(path))try{Decode(File.ReadAllText(path));valid=true;}catch(NotSupportedException){throw;}catch{File.Copy(path,path+".damaged-"+DateTime.UtcNow.Ticks,false);}
  string temp=path+".tmp";File.WriteAllText(temp,JsonUtility.ToJson(this,true));if(File.Exists(path))File.Replace(temp,path,valid?path+".bak":null);else File.Move(temp,path);
 }

}
public partial class Campaign {
 [NonSerialized] public TrailProgression progression;
 public int ArmyCapacity {get{return mode=="trail"?20:10;}}
 public string CommanderFor(Expedition army){if(army==null)return TrailProgression.Primary(data,faction);if(string.IsNullOrEmpty(army.commanderType))army.commanderType=army.units.FirstOrDefault(v=>v.id==commanderId)?.type??army.units.FirstOrDefault(v=>data.P(v.type).commander)?.type??TrailProgression.Primary(data,faction);return army.commanderType;}
 public int PersonalWins {get{return progression?.CommanderWins(CommanderFor(Army))??0;}}
 public int TrainingCost(Veteran u){return 50+35*u.level-(PersonalWins>=6?10:0);}
 public string CampaignLock(string type){return progression?.LockReason(data,type,CommanderFor(Army));}
 public static List<string> StartingWarband(Content d,string faction,List<string> muster,TrailProgression progress){
  string selected=muster.FirstOrDefault(id=>d.profiles.ContainsKey(id)&&d.P(id).commander)??TrailProgression.Primary(d,faction);
  var eligible=muster.Where(id=>d.profiles.ContainsKey(id)&&d.P(id).faction==faction&&(progress==null||progress.Allows(d,id,selected))).ToList();
  string commander=eligible.FirstOrDefault(id=>d.P(id).commander)??TrailProgression.Primary(d,faction);
  var core=d.units.Where(p=>p.faction==faction&&!p.commander&&!p.titan&&p.kind=="infantry"&&(progress==null||progress.Allows(d,p.id,commander))).OrderBy(p=>p.cost).ThenBy(p=>p.id).Take(2).ToList();
  return new List<string>{commander,core[0].id,core[1].id};
 }
 public void GenerateTrail(){
  for(int chapter=1;chapter<=3;chapter++)for(int step=0;step<5;step++){
   int shift=(int)(Random()*3);
   for(int branch=0;branch<(step==4?1:3);branch++){
    string kind=step==4?"Boss":step==0?"Battle":new[]{"Battle","Elite","Camp","Armory","Event"}[(int)(Random()*5)];
    if(step==3&&branch==1)kind="Armory";if(step==1&&branch==shift)kind="Camp";
    nodes.Add(new TrailNode{act=chapter,row=step,lane=branch,kind=kind});
   }
  }
 }
}
}
