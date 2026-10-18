using System.Collections.Generic;
namespace QuietDread {
public static class NativeProfiles {
 public static string Summary(Profile p){var rules=new List<string>();
  if(p.commander)rules.Add("Orders: 1 AP, friendly within 4; +15 accuracy and +1 control.");
  if(p.caster)rules.Add("Caster: range 6; spells spend Aether and the turn's attack.");
  if(p.kind=="artillery")rules.Add("Brace before firing; moving removes bracing.");
  if(p.minRange>0)rules.Add("Minimum ranged distance "+p.minRange+".");
  if(p.indirect)rules.Add("Indirect: ignores line of sight.");
  if(p.ignoreCover)rules.Add("Ignores cover.");
  if(p.blast>0)rules.Add("Blast "+p.blast+": half damage to nearby formations, including friends.");
  if(p.stealth)rules.Add("Stealth: −20 incoming accuracy beyond 3 cells.");
  if(p.gas)rules.Add("Gas hits slow the target.");
  if(p.tunnel)rules.Add("Tunnel: 2 AP (Deepdelve: 1), relocate to empty legal ground beside a relay.");
  if(p.flying)rules.Add("Flying: crosses obstacles, water and difficult terrain.");else if(p.amphibious)rules.Add("Amphibious: crosses water; no marsh penalty.");
  switch(p.ability){
   case "repair":rules.Add("Repair: 1 AP, friendly vehicle/titan within 3; heal 7, or 11 using Salvage.");break;
   case "fortify":rules.Add("Fortify: 1 AP; create a trench on own non-water cell.");break;
   case "sabotage":rules.Add("Sabotage: 1 AP, enemy within 4; +2 suppression and Pressure.");break;
   case "charge":rules.Add("Charge: 2 AP, move +1 then adjacent melee with +4 damage.");break;
   case "soulfire":rules.Add("Soulfire: 3 Aether, 7 damage and heal caster 5.");break;
   case "birth":rules.Add("Automatic adjacent Clawrat birth each friendly turn. Signature: another birth. Six living temporary broods maximum.");break;
   case "shield":case "aegis":rules.Add("Signature: allies within 4 gain "+(p.ability=="aegis"?8:6)+" wards and clear suppression.");break;
   case "stomp":rules.Add("Signature: enemies within 2 take 18 damage and 2 suppression.");break;
   case "siege":rules.Add("Siege signature braces the engine; moving removes bracing.");break;
   case "beam":rules.Add("Signature: every enemy in the selected horizontal row takes 16 damage.");break;
   case "veil":rules.Add("Signature: relocate to legal empty terrain within 6; nearby allies gain Veil.");break;
   case "raise":rules.Add("Signature: restore a fallen permanent non-titan at half HP in a legal adjacent cell; no AP on arrival.");break;
   case "stampede":rules.Add("Signature: charge up to 6 cells, then deal 28 damage; surviving enemy retaliates.");break;
   case "riftstorm":rules.Add("Signature: 3 Aether; enemies within 4 take 10 damage; caster takes 5.");break;
   case "roots":rules.Add("Signature: root enemies within 4 and heal nearby allies by 6.");break;
   case "solar":rules.Add("Signature: enemies within 3 take 14 damage; gain 2 Aether.");break;
   case "sanctuary":rules.Add("Signature: allies within 4 heal 12 and clear suppression and Pressure.");break;
   case "siphon":rules.Add("Signature: drain up to 8 HP from each enemy within 4.");break;
   case "contract":rules.Add("Signature: pay 8 HP to deal 20 to an enemy within 8; heal 12 if it dies.");break;
   case "digest":rules.Add("Signature: spend 1 Biomass, heal 16, attempt an adjacent temporary spawn.");break;
   case "overload":rules.Add("Signature: pay 6 HP; all formations within 1 of an enemy target within 10 take 20 damage.");break;
  }
  if(p.titan)rules.Add("Signature normally costs 2 AP and the attack.");
  if(p.id=="tank")rules.Add("First hit each round reduced by 2.");
  if(p.id=="stormvermin")rules.Add("+1 armour within 1 of a relay.");
  if(p.id=="seraphyx")rules.Add("Opening battle: one enemy gains Pressure.");
  if(rules.Count==0)rules.Add("Standard formation actions. Faction-wide rules apply.");
  return string.Join(" ",rules);
 }
}
}
