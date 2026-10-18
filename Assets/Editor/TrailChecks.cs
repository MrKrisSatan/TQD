using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using QuietDread;

public static class TrailChecks {
 public static void Run(string root){
  var lines=new List<string>();Action<bool,string> check=(ok,name)=>{if(!ok)throw new Exception("TRAIL CHECK: "+name);lines.Add("PASS "+name);};
  var d=Content.Load();var progress=new TrailProgression();
  Func<string,uint,Campaign> create=(f,seed)=>Campaign.Create(d,"trail",f,d.F(f).defaults.ToList(),"normal",seed,progress);
  foreach(var f in d.factions){var c=create(f.id,42);check(c.Army.units.Count==3&&c.Army.units.Count(v=>d.P(v.type).commander)==1&&!c.Army.units.Any(v=>d.P(v.type).titan),f.id+" modest three-unit start");check(c.Army.units.All(v=>progress.Allows(d,v.type)),f.id+" starter has no locked units");check(c.nodes.Count(n=>n.kind=="Boss")==3&&c.nodes.Any(n=>n.act==3&&n.row==4&&n.kind=="Boss"),f.id+" final sentinel exists");}
  var seeded=create("vermin",100);check(route(seeded)==route(create("vermin",100)),"same seed is reproducible");
  string route(Campaign c){return string.Join(",",c.nodes.Select(n=>n.kind));}
  check(route(seeded)!=route(create("vermin",101)),"different seeds change routes");
  for(uint seed=1;seed<=30;seed++){var c=create("vermin",seed);check(c.nodes.Where(n=>n.row==2).All(n=>c.nodes.Any(shop=>shop.act==n.act&&shop.row==3&&shop.kind=="Armory"&&Math.Abs(shop.lane-n.lane)<=1)),"seed "+seed+" armory reachable before each sentinel");}
  check(!progress.Allows(d,"gore_maiden")&&!progress.Allows(d,"seraphyx")&&!progress.Allows(d,"vermin_titan"),"initial Verminhorde rewards locked");
  var active=create("vermin",1);check(!progress.Record(active),"active run cannot award a completion");active.status="lost";check(!progress.Record(active),"lost run cannot award a completion");
  for(int win=1;win<=10;win++){
   var c=create("vermin",(uint)win);int steps=0;
   while(c.status=="active"&&steps++<90){
    if(c.phase=="map")c.Visit(c.nodes.First(c.Available));
    else if(c.phase=="battle"){
     var b=c.MakeBattle();check(b.Living(0).Count()==c.Army.units.Count,"run "+win+" keeps persistent army");
     if(c.act==1&&c.pendingRow==0)check(b.Living(1).Count()==2,"opening opposition suits a tiny warband");
     if(c.act==3&&c.pendingBoss){check(b.finalTrailBoss&&b.Living(1).Any(u=>b.P(u).titan),"final sentinel has titan support");b.Begin();b.score[0]=99;b.CheckEnd();check(!b.finished,"relay points cannot bypass final boss");foreach(var u in b.Living(1).Where(u=>b.P(u).titan||b.P(u).commander).ToList())u.hp=0;b.CheckEnd();check(b.finished&&b.winner==0,"defeating both final guardians wins");}
     b.finished=true;b.winner=0;c.Settle(b);
    }else c.Choice(c.phase=="shop"?"leave":c.phase=="event"?"safe":"gold");
   }
   check(c.status=="won"&&progress.Record(c),"complete run "+win+" awards once");check(!progress.Record(c)&&progress.Wins("vermin")==win,"run "+win+" duplicate settlement/save does not award twice");
   var restored=JsonUtility.FromJson<Campaign>(JsonUtility.ToJson(c));restored.data=d;check(!progress.Record(restored),"completed run "+win+" reload is idempotent");
   check(progress.Allows(d,"gore_maiden")&&progress.Allows(d,"seraphyx")== (win>=2)&&progress.Allows(d,"vermin_titan")== (win>=10),"milestone gating at "+win+" wins");
  }
  check(progress.Wins("vault")==0&&!progress.Allows(d,"vault_titan"),"progress is faction specific");
  foreach(var mode in new[]{"trail","conquest"}){
   var c=Campaign.Create(d,mode,"vermin",d.F("vermin").defaults.ToList(),"normal",42,progress);c.gold=10000;
   if(mode=="conquest"){c.provinces[0].tier=3;c.provinces[0].buildings.Add("sanctum");}
   foreach(var id in new[]{"gore_maiden","seraphyx","vermin_titan"})check(c.Recruit(id),mode+" can recruit earned "+id);
  }
  var locked=Campaign.Create(d,"conquest","vermin",d.F("vermin").defaults.ToList(),"normal",42,new TrailProgression());locked.gold=10000;check(!locked.Recruit("gore_maiden")&&!locked.RaiseArmy("seraphyx"),"locked recruits and raised commanders cannot bypass progression");
  var growth=create("vermin",9);growth.gold=50000;
  foreach(var p in d.units.Where(p=>p.faction=="vermin"))while(growth.Army.units.Count<20&&growth.Recruit(p.id)){}
  check(growth.Army.units.Count==20&&growth.RecruitError("clawrat")=="Army is full.","campaign can grow to twenty formations with a hard cap");
  growth.Visit(growth.nodes.First(growth.Available));var large=growth.MakeBattle();
  foreach(var size in new[]{new Vector2Int(24,16),new Vector2Int(32,22),new Vector2Int(40,28)})foreach(var edge in new[]{"west","east","north","south"}){large.ConfigureField(size.x,size.y,edge,"woodland");check(large.Living(0).All(u=>large.InDeployment(0,u.x,u.y))&&large.units.Select(u=>u.x+":"+u.y).Distinct().Count()==large.units.Count,"twenty units deploy without overlap "+size+" "+edge);}
  var veteran=growth.Army.units[0];veteran.xp=10;growth.phase="map";check(growth.Train(veteran)&&veteran.level==1,"survivors can train during growth");
  // Isolated persistence fixture; never touch the player's LocalLow progression or campaign.
  string dir=Path.Combine(root,"Temp/TrailProgressionChecks");Directory.CreateDirectory(dir);string path=Path.Combine(dir,"progress.json");
  progress.Save(path);progress.Save(path);var loaded=TrailProgression.Load(path);check(loaded.Wins("vermin")==10&&loaded.Allows(d,"vermin_titan"),"progression survives independent save/load");File.WriteAllText(path,"broken json");check(TrailProgression.Load(path).Wins("vermin")==10,"corrupt primary recovers from backup");
  File.WriteAllText(Path.Combine(root,"Documentation/TRAIL-RESULTS.txt"),lines.Count+" checks passed\n"+string.Join("\n",lines));
 }
}

