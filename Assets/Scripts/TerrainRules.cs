using System;
using System.Linq;
using UnityEngine;
namespace QuietDread {
public partial class Battle {
 public static readonly Vector2Int[] Steps={new Vector2Int(1,0),new Vector2Int(-1,0),new Vector2Int(0,1),new Vector2Int(0,-1)};
 public static string TerrainName(int t){return new[]{"Open ground","Ruins","Trench","Crater","Woodland","Deep water","Marsh","Wall","Rock outcrop","Building"}[Mathf.Clamp(t,0,9)];}
 public static bool BlocksSight(int t){return t==1||t==8||t==9;}
 public bool Passable(Profile p,int x,int y){if(!Contains(x,y))return false;int t=Tile(x,y);return !BlocksSight(t)&&t!=7&&(t!=5||p.flying||p.amphibious);}
 public int MoveCost(Profile p,int x,int y){int t=Tile(x,y);if(p.flying)return 1;if(t==6)return p.amphibious?1:2;if(t==4)return p.faction=="verdant"?1:2;if(t==3)return p.faction=="verdant"?1:2;return 1;}
 public int Cover(Soldier attacker,Soldier target){int t=Tile(target.x,target.y);int cover=t==2||t==3||t==4||P(target).faction=="verdant"?20:0;foreach(var step in Steps){int tx=target.x+step.x,ty=target.y+step.y;int near=Tile(tx,ty);if(Contains(tx,ty)&&(near==1||near==7||near==8||near==9||near==4)&&(attacker.x-target.x)*step.x+(attacker.y-target.y)*step.y>=0)cover=Math.Max(cover,near==4?20:25);}return cover;}
 public string Edge(int team){if(team==0)return deploymentSide;return deploymentSide=="west"?"east":deploymentSide=="east"?"west":deploymentSide=="north"?"south":"north";}
 public bool InDeployment(int team,int x,int y){if(!Contains(x,y))return false;string edge=Edge(team);int depth=Mathf.Clamp((edge=="west"||edge=="east"?width:height)/5,3,6);return edge=="west"?x<depth:edge=="east"?x>=width-depth:edge=="south"?y<depth:y>=height-depth;}
 public void ConfigureField(int w,int h,string edge,string environment){if(!deployment)return;width=w;height=h;deploymentSide=edge;biome=environment;fieldConfigured=true;terrain=new int[w*h];
  // Rounded groves, pools and marshes leave broad manoeuvring spaces.
  int patches=w*h/60;for(int i=0;i<patches;i++){int cx=3+(int)(Random()*(w-6)),cy=3+(int)(Random()*(h-6));int type=i%4==0?5:i%4==1?6:4;if(environment=="ruined city"&&type==4)type=3;if(environment=="wetlands"&&i%3==0)type=6;float radius=1.2f+Random()*1.4f;for(int y=cy-3;y<=cy+3;y++)for(int x=cx-3;x<=cx+3;x++)if(Contains(x,y)&&Vector2.Distance(new Vector2(x,y),new Vector2(cx,cy))<=radius)terrain[y*w+x]=type;}
  int structures=w*h/(environment=="ruined city"?38:75);for(int i=0;i<structures;i++){int x=3+(int)(Random()*(w-6)),y=3+(int)(Random()*(h-6));terrain[y*w+x]=new[]{1,8,9}[i%3];if(i%3==2&&Contains(x+1,y))terrain[y*w+x+1]=9;if(i%2==0&&Contains(x,y+1))terrain[(y+1)*w+x]=7;}
  // Clear deployment zones and crossing roads so ordinary infantry can always reach objectives.
  for(int y=0;y<h;y++)for(int x=0;x<w;x++)if(InDeployment(0,x,y)||InDeployment(1,x,y)||x==w/2||y==h/2)terrain[y*w+x]=0;
  foreach(var r in Objectives)foreach(var delta in Steps.Concat(new[]{Vector2Int.zero})){int x=r.x+delta.x,y=r.y+delta.y;if(Contains(x,y))terrain[y*w+x]=0;}
  foreach(int team in new[]{0,1}){var army=Living(team).ToList();string side=Edge(team);bool vertical=side=="north"||side=="south";int span=vertical?w:h;for(int i=0;i<army.Count;i++){int rows=(army.Count+span-3)/(span-2);int slots=(army.Count+rows-1)/rows;int cross=Mathf.Clamp((i%slots+1)*span/(slots+1),1,span-2);int depth=1+i/slots;army[i].x=vertical?cross:side=="west"?depth:w-1-depth;army[i].y=vertical?(side=="south"?depth:h-1-depth):cross;}}
  Note("Deploy on the "+edge+" edge. "+w+" × "+h+" battlefield / "+environment+". Water requires flight or amphibious movement.");
 }
}
}
