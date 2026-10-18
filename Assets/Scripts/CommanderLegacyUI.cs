using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
namespace QuietDread {
public partial class Game {
 TrailProgression trailProgress=new TrailProgression(); bool progressLoaded,progressBroken,showLegacy;string legacyCommander;int raiseCommanderIndex;
 string ProgressPath {get{return Path.Combine(Application.persistentDataPath,"trail-progression.json");}}
 string SelectedCommander {get{return roster.FirstOrDefault(id=>data.P(id).commander)??TrailProgression.Primary(data,faction);}}
 void EnsureProgress(){if(progressLoaded||qa)return;progressLoaded=true;try{trailProgress=TrailProgression.Load(ProgressPath);trailProgress.Migrate(data);if(trailProgress.recoveredFromBackup)notice="Commander progress recovered from backup; damaged primary will be preserved.";if(!string.IsNullOrEmpty(trailProgress.migrationNote))notice=trailProgress.migrationNote;}catch(Exception e){progressBroken=true;notice=e.Message;Debug.LogWarning(e.Message);}}
 string MusterError(){EnsureProgress();if(mode!="skirmish"){if(progressBroken)return "Repair trail-progression.json or its backup before starting a campaign.";if(!trailProgress.CanSelect(data,SelectedCommander))return "This commander has not been discovered.";foreach(var id in roster){var reason=trailProgress.LockReason(data,id,SelectedCommander);if(reason!=null)return reason;}if(mode=="trail")return roster.Any(id=>data.P(id).commander)?null:"Choose a commander.";}return Battle.Validate(data,faction,roster);}
 List<string> Recommended(){EnsureProgress();string leader=TrailProgression.Primary(data,faction);return data.F(faction).defaults.Where(id=>mode=="skirmish"||trailProgress.Allows(data,id,leader)).ToList();}
 string ProgressSummary(){EnsureProgress();return "PERSONAL LEGACY · "+data.P(SelectedCommander).name+" · "+trailProgress.CommanderWins(SelectedCommander)+" Trail completions";}
 void SelectCommander(string id){roster.RemoveAll(t=>data.P(t).commander);roster.Insert(0,id);if(mode!="skirmish")roster.RemoveAll(t=>!trailProgress.Allows(data,t,id));}
 void LegacyDialog(){GUI.enabled=true;Rect(0,0,1600,900,new Color(0,0,0,.8f));Frame(220,70,1160,770);string leader=legacyCommander??SelectedCommander;Text(data.P(leader).name+" / PERSONAL LEGACY",250,96,1080,48,heading);Text(trailProgress.CommanderWins(leader)+" successful Trails · Rewards belong to this commander's armies in BOTH campaigns.\nNew commanders begin their own tracks. Skirmish is unrestricted.",250,155,1080,60,body);
  for(int wins=1;wins<=10;wins++){float y=221+(wins-1)*51;bool earned=trailProgress.CommanderWins(leader)>=wins;var r=CommanderRewardCatalog.Load().Track(leader).rewards.FirstOrDefault(x=>x.wins==wins);if(r!=null)earned=trailProgress.Allows(data,r.unit,leader);Rect(248,y,1100,46,earned?new Color(.13f,.25f,.22f):new Color(.09f,.12f,.14f));Text(wins+"  ·  "+(earned?"EARNED":"LOCKED"),262,y+8,165,30,small);Text(TrailProgression.RewardText(data,leader,wins),430,y+5,900,40,new GUIStyle(body){fontSize=17});}
  if(Btn("Close legacy",1050,775,290,42))showLegacy=false;
 }
 void RaiseArmyControls(){var choices=data.units.Where(p=>p.faction==campaign.faction&&p.commander&&(campaign.progression==null||campaign.progression.CanSelect(data,p.id))).ToList();if(choices.Count==0)return;raiseCommanderIndex=Math.Abs(raiseCommanderIndex)%choices.Count;if(Btn("‹",1080,696,40,40))raiseCommanderIndex=(raiseCommanderIndex+choices.Count-1)%choices.Count;if(Btn("›",1495,696,40,40))raiseCommanderIndex=(raiseCommanderIndex+1)%choices.Count;var choice=choices[raiseCommanderIndex];if(Btn("Raise: "+choice.name+" · "+(choice.cost+80),1125,696,365,40)){if(!campaign.RaiseArmy(choice.id))notice="Needs owned barracks, enough gold, and fewer than three armies.";Save();}}
}
}
