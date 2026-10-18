using System;
using System.IO;
using System.Linq;
using UnityEngine;
namespace QuietDread {
public static class CampaignStorage {
 public static SaveGame Parse(string raw,Content d){
  if(string.IsNullOrWhiteSpace(raw)||!raw.Contains("\"version\"")||!raw.Contains("\"campaign\""))throw new IOException("Incomplete campaign save.");
  var s=JsonUtility.FromJson<SaveGame>(raw);if(s==null)throw new IOException("Empty save.");if(s.version>1)throw new NotSupportedException("Campaign belongs to a newer game version. No files were changed.");if(s.version!=1)throw new IOException("Unsupported campaign schema.");
  var c=s.campaign;if(c==null||!new[]{"trail","conquest"}.Contains(c.mode)||!d.factions.Any(f=>f.id==c.faction)||!new[]{"active","lost","won"}.Contains(c.status)||c.armies==null||c.nodes==null||c.provinces==null)throw new IOException("Invalid campaign state.");
  if(c.armies.Any(a=>a==null||a.units==null||a.units.Any(v=>v==null||!d.profiles.ContainsKey(v.type)||d.P(v.type).faction!=c.faction))||c.armies.Select(a=>a.id).Distinct().Count()!=c.armies.Count)throw new IOException("Invalid army roster.");
  if(c.mode=="conquest"&&(c.provinces.Count!=16||c.provinces.Where((p,i)=>p==null||p.id!=i||p.buildings==null||p.owner!=""&&!d.factions.Any(f=>f.id==p.owner)).Any()||c.armies.Any(a=>a.region<0||a.region>=16)))throw new IOException("Invalid province map.");
  if(c.mode=="trail"&&(c.act<1||c.act>3||c.nodes.Count==0||c.nodes.Any(n=>n==null||n.act<1||n.act>3||n.row<0||n.row>4||n.lane<0||n.lane>2)))throw new IOException("Invalid Trail route.");
  // Unity may materialize a serialized null nested object as an empty Battle.
  if(s.battle!=null&&string.IsNullOrEmpty(s.battle.campaignBattleId)&&(s.battle.units==null||s.battle.units.Count==0))s.battle=null;
  if(s.battle!=null){var b=s.battle;int w=b.width<1?18:b.width,h=b.height<1?12:b.height;if(b.units==null||b.units.Any(u=>u==null||!d.profiles.ContainsKey(u.type)||u.team<0||u.team>1)||b.terrain==null||b.terrain.Length!=w*h||b.score==null||b.score.Length!=2||b.factions==null||b.factions.Length!=2||b.campaignBattleId!=c.pendingId||c.phase!="battle")throw new IOException("Invalid saved battlefield.");}
  c.data=d;return s;
 }
 public static SaveGame Load(string path,Content d,out bool recovered){
  recovered=false;Exception primary=null;
  try{return Parse(File.ReadAllText(path),d);}catch(NotSupportedException){throw;}catch(Exception e){primary=e;}
  try{var result=Parse(File.ReadAllText(path+".bak"),d);recovered=true;return result;}catch(NotSupportedException){throw;}catch{throw new IOException("Campaign and backup could not be loaded. Files preserved. "+primary.Message);}
 }
 public static void Write(string path,SaveGame save,Content d){
  string json=JsonUtility.ToJson(save,true);Parse(json,d);Directory.CreateDirectory(Path.GetDirectoryName(path));bool valid=false;
  if(File.Exists(path))try{Parse(File.ReadAllText(path),d);valid=true;}catch(NotSupportedException){throw;}catch{File.Copy(path,path+".damaged-"+DateTime.UtcNow.Ticks,false);}
  string temp=path+".tmp";File.WriteAllText(temp,json);
  // Never replace a valid backup with the damaged primary we have just recovered from.
  if(File.Exists(path))File.Replace(temp,path,valid?path+".bak":null);else File.Move(temp,path);
 }
}
}
