using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using QuietDread;
public static class CoherenceChecks {
 public static void Run(string root){var d=Content.Load();var lines=new List<string>();Action<bool,string> check=(ok,name)=>{if(!ok)throw new Exception("COHERENCE: "+name);lines.Add("PASS "+name);};
  foreach(var p in d.units)check(p.hp>0&&p.move>0&&p.cost>0&&p.range>=p.minRange&&p.accuracy>=0&&p.accuracy<=100&&p.armour>=0&&p.penetration>=0,p.id+" legal profile");
  var b=Battle.Create(d,"vermin",new List<string>{"marshal"},"compact",new List<string>{"captain"});b.terrain=new int[b.width*b.height];var u=b.units[0];check(!b.Act(u,"rally",u.x,u.y),"no actions during deployment");b.Begin();u.suppression=4;u.pressure=2;u.ap=2;check(b.Act(u,"rally",u.x,u.y)&&u.ap==1&&u.suppression==2&&u.pressure==1,"rally trades AP for suppression and Pressure");check(b.Act(u,"rally",u.x,u.y)&&u.ap==0&&u.suppression==0&&u.pressure==0,"rally can consume remaining AP without refund");check(!b.Act(u,"rally",u.x,u.y),"no AP cannot rally");u.ap=2;check(!b.Act(u,"rally",u.x,u.y)&&u.ap==2,"unnecessary rally does not spend AP");b.turn=1;u.suppression=3;check(!b.Act(u,"rally",u.x,u.y),"cannot rally outside own turn");
  foreach(var p in d.units)check(!string.IsNullOrEmpty(NativeProfiles.Summary(p)),p.id+" native rules summary");
  uint savedSeed=b.seed;var first=new List<int>();for(int i=0;i<1000;i++)first.Add(b.RollD100());check(first.All(n=>n>=1&&n<=100)&&first.Distinct().Count()==100,"d100 bounds and all faces sampled");b.seed=savedSeed;check(first.All(n=>n==b.RollD100()),"saved seed reproduces entire dice sequence");
  b.turn=0;u.type="mortar";u.x=5;u.y=5;var enemy=b.units[1];enemy.x=10;enemy.y=5;uint seed=b.seed;float clean=b.TargetValue(u,enemy);var friend=b.Add("clawrat",0,10,6);check(b.TargetValue(u,enemy)<clean,"AI penalizes blast friendly fire");float position=b.PositionValue(u,5,5,enemy);check(b.seed==seed&&u.x==5&&u.y==5,"AI evaluation preserves position and RNG");check(!float.IsNaN(position)&&!float.IsInfinity(position),"AI position score finite");
  lines.Insert(0,lines.Count+" profile, AI and action-economy checks passed.");File.WriteAllLines(Path.Combine(root,"Documentation/COHERENCE-TEST-RESULTS.txt"),lines);
 }
}
