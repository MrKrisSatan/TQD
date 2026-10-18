using System;
using System.Linq;
namespace QuietDread {
public partial class Battle {
 public bool TrySupportAction(Soldier actor){if(!CanAct(actor))return false;var p=P(actor);var friends=Living(actor.team).ToList();
  if(p.ability=="repair"){var damaged=friends.Where(q=>D(actor,q)<=3&&(P(q).kind=="vehicle"||P(q).titan)&&MaxHP(q)-q.hp>=7).OrderBy(q=>q.hp/(float)MaxHP(q)).FirstOrDefault();if(damaged!=null&&Act(actor,"repair",damaged.x,damaged.y))return true;}
  if(p.caster&&!actor.attacked&&aether[actor.team]>=3){var urgent=friends.Where(q=>D(actor,q)<=6&&MaxHP(q)-q.hp>=8&&q.hp<=MaxHP(q)*.55f).OrderBy(q=>q.hp/(float)MaxHP(q)-(P(q).commander?.15f:0)).FirstOrDefault();if(urgent!=null&&Act(actor,"heal",urgent.x,urgent.y))return true;}
  if(p.commander&&actor.ap>=2){var recipient=friends.Where(q=>q!=actor&&q.ap>0&&!q.attacked&&q.order==0&&D(actor,q)<=4&&Living(1-actor.team).Any(e=>D(q,e)>1&&ShotError(q,e)==null)).OrderByDescending(q=>P(q).power).FirstOrDefault();if(recipient!=null&&Act(actor,"orders",recipient.x,recipient.y))return true;}
  return false;
 }
 public float TargetValue(Soldier actor,Soldier target){
  bool melee=D(actor,target)<=1;var p=P(actor);float chance=melee?1:Chance(actor,target)/100f;if(!melee&&(p.faction=="lumen"||p.faction=="vesper"))chance=1-(1-chance)*(1-chance);float damage=DamagePreview(actor,target,melee);float value=Math.Min(target.hp,Math.Max(0,damage-target.ward))*chance;
  if(damage>=target.hp+target.ward)value+=8*chance;
  if(P(target).commander)value+=3*chance;
  if(Objectives.Any(r=>Dist(target.x,target.y,r.x,r.y)<=1))value+=3*chance;
  if(!melee&&p.blast>0)foreach(var nearby in units.Where(q=>q.Alive&&q!=target&&Dist(q.x,q.y,target.x,target.y)<=p.blast))value+=Math.Min(nearby.hp,Math.Max(0,damage/2-nearby.ward))*chance*(nearby.team==actor.team?-1.5f:1);
  return value;
 }
 public float PositionValue(Soldier actor,int x,int y,Soldier nearest){
  var p=P(actor);float relay=Objectives.Min(r=>Dist(x,y,r.x,r.y)),enemy=Dist(x,y,nearest.x,nearest.y);float value=relay*2+enemy*.35f;
  if(p.kind=="artillery"||p.ability=="siege")value=enemy<p.minRange?100:Math.Abs(enemy-p.range+2)+relay*.3f;
  // Evaluate a hypothetical position without moving the live formation or touching RNG.
  var probe=new Soldier{type=actor.type,team=actor.team,x=x,y=y,hp=actor.hp,level=actor.level};
  foreach(var foe in Living(1-actor.team)){var fp=P(foe);int distance=Dist(x,y,foe.x,foe.y);if(distance<=fp.range&&(fp.indirect||LOS(foe.x,foe.y,x,y))){float exposed=1-Cover(foe,probe)/35f;value+=exposed*(actor.hp<MaxHP(actor)/2?1.5f:.55f);}if(distance==1&&p.melee<fp.melee)value+=2;}
  // Hold an owned relay instead of wandering toward an equally distant one.
  if(relay<=1)value-=1;
  return value;
 }
}
}
