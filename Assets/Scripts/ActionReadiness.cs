using System;
using System.Collections.Generic;
namespace QuietDread {
public partial class Battle {
 // Target-independent reasons are shared by the action bar and rules entry point.
 public string ActionUnavailable(Soldier u,string action){
  if(finished)return "The battle is over.";if(deployment)return "Finish deployment first.";
  if(u==null||!u.Alive)return "Select a living formation.";if(u.team!=turn)return "Wait for this army's turn.";
  var p=P(u);int ap=action=="charge"||action=="signature"?2:action=="tunnel"&&doctrine!="deep"?2:1;
  if(u.ap<ap)return "Requires "+ap+" AP; this formation has "+u.ap+".";
  bool spell=action=="lance"||action=="terror"||action=="heal"||action=="ward"||action=="soulfire";
  if((spell||action=="fire"||action=="charge"||action=="signature")&&u.attacked)return "This formation has used its attack this turn.";
  if(spell){if(!p.caster)return "Only casters can invoke magic.";int cost=action=="ward"?(p.faction=="dawn"?1:2):3;if(aether[u.team]<cost)return "Requires "+cost+" Aether.";}
  if(action=="move"&&Move(u)==0)return "Rooted: this formation cannot move.";
  if(action=="rally"&&u.suppression==0&&u.pressure==0)return "No suppression or Pressure to remove.";
  if(action=="brace"&&u.setup)return "Already braced. Moving removes bracing.";
  if(action=="memory"&&memory[u.team]<1)return "Requires 1 harvested Memory.";
  if(action=="brood"&&(broodUsed[u.team]||biomass[u.team]<2))return broodUsed[u.team]?"Brood restoration has already been used.":"Requires 2 Biomass.";
  return null;
 }
 public string StatusSummary(Soldier u){var states=new List<string>();if(u.ward>0)states.Add("Ward "+u.ward);if(u.suppression>0)states.Add("Suppressed "+u.suppression);if(u.pressure>0)states.Add("Pressure "+u.pressure);if(u.rooted>0)states.Add("Rooted");if(u.slow>0)states.Add("Slowed");if(u.veil>0)states.Add("Veiled");if(u.order>0)states.Add("Orders");if(u.setup)states.Add("Braced");if(u.attacked)states.Add("Attack spent");return states.Count==0?"Ready":string.Join(" · ",states);}
 public int RetaliationDamage(Soldier defender,Soldier attacker){return Math.Max(1,P(defender).melee/2-Math.Max(0,Armour(attacker)-P(defender).penetration));}
}
}
