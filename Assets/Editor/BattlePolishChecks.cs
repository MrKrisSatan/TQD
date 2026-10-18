using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using QuietDread;
public static class BattlePolishChecks {
 public static void Run(string root){var d=Content.Load();var report=new List<string>();Action<bool,string> check=(ok,name)=>{if(!ok)throw new Exception("BATTLE POLISH: "+name);report.Add("PASS "+name);};
  Func<Battle> fresh=()=>{var b=Battle.Create(d,"vermin",new List<string>{"marshal"},"compact",new List<string>{"captain"});b.terrain=new int[b.width*b.height];b.units[0].x=3;b.units[0].y=5;b.units[1].x=9;b.units[1].y=5;b.Begin();return b;};
  var test=fresh();var leader=test.units[0];check(test.ActionUnavailable(leader,"rally")!=null,"rally unavailable without counters");leader.rooted=2;check(test.ActionUnavailable(leader,"move")!=null&&!test.Act(leader,"move",4,5),"rooted action rejected centrally");leader.rooted=0;leader.ap=1;check(test.ActionUnavailable(leader,"charge")!=null,"two AP action correctly unavailable");leader.ap=2;leader.attacked=true;check(test.ActionUnavailable(leader,"fire")!=null&&test.ActionUnavailable(leader,"move")==null,"spent attack leaves movement legal");
  test=fresh();leader=test.units[0];var caster=test.Add("confessor",0,4,5);leader.hp=6;int aether=test.aether[0];check(test.TrySupportAction(caster)&&leader.hp==14&&caster.ap==1&&caster.attacked&&test.aether[0]==aether-3,"AI heals critical ally with real AP and resource costs");caster.attacked=false;caster.ap=2;leader.hp=test.MaxHP(leader);aether=test.aether[0];check(!test.Act(caster,"heal",leader.x,leader.y)&&caster.ap==2&&test.aether[0]==aether,"full health healing rejected without resource loss");
  test=fresh();leader=test.units[0];var gunner=test.Add("clawrat",0,6,5);check(test.TrySupportAction(leader)&&gunner.order==1&&leader.ap==1,"AI orders ally with viable ranged attack");check(!test.Act(leader,"orders",gunner.x,gunner.y)&&leader.ap==1,"duplicate orders cannot waste AP");
  test=fresh();var engineer=test.Add(d.units.First(p=>p.ability=="repair").id,0,4,5);var tank=test.Add("tank",0,5,5);tank.hp-=12;int before=tank.hp;check(test.TrySupportAction(engineer)&&tank.hp==before+7&&engineer.ap==1,"AI repairs damaged engine");tank.hp=test.MaxHP(tank);int ap=engineer.ap;check(!test.Act(engineer,"repair",tank.x,tank.y)&&engineer.ap==ap,"full health repair rejected without AP loss");
  test=fresh();var drain=test.Add(d.units.First(p=>p.titan&&p.ability=="siphon").id,0,8,5);drain.hp-=20;before=drain.hp;test.units[1].ward=20;check(test.Act(drain,"signature",9,5)&&drain.hp==before&&test.units[1].ward==12,"siphon cannot heal from ward absorption");
  test=fresh();drain=test.Add(d.units.First(p=>p.titan&&p.ability=="siphon").id,0,8,5);drain.hp-=20;before=drain.hp;test.units[1].ward=3;check(test.Act(drain,"signature",9,5)&&drain.hp==before+5,"siphon heals only actual HP damage");
  test=fresh();leader=test.units[0];var enemy=test.units[1];check(test.RetaliationDamage(enemy,leader)==Math.Max(1,test.P(enemy).melee/2-Math.Max(0,test.Armour(leader)-test.P(enemy).penetration)),"shared retaliation applies penetration");leader.ward=6;leader.pressure=2;leader.setup=true;check(test.StatusSummary(leader).Contains("Ward 6")&&test.StatusSummary(leader).Contains("Pressure 2")&&test.StatusSummary(leader).Contains("Braced"),"status summary exposes defensive and disruption state");
  var mortar=test.Add("mortar",0,4,4);mortar.setup=true;check(test.ActionUnavailable(mortar,"brace")!=null&&!test.Act(mortar,"brace",4,4),"repeated brace cannot waste AP");
  report.Insert(0,report.Count+" support AI, readiness and interaction checks passed.");File.WriteAllLines(Path.Combine(root,"Documentation/BATTLE-POLISH-TEST-RESULTS.txt"),report);
 }
}
