using System;
using System.Linq;
using UnityEngine;
namespace QuietDread {
public partial class Game {
 Profile inspectedProfile;Vector2 profileScroll;int cataloguePage;string catalogueFaction="all";Vector2 manualScroll;string referenceText;
 Profile[] CatalogueProfiles(){return data.units.Where(p=>catalogueFaction=="all"||p.faction==catalogueFaction).ToArray();}
 void Catalogue(){GUI.enabled=inspectedProfile==null;Banner("MINIATURE CATALOGUE / Actual battlefield models and native rules profiles.");var entries=CatalogueProfiles();int pages=Math.Max(1,(entries.Length+7)/8);cataloguePage=Mathf.Clamp(cataloguePage,0,pages-1);if(Btn("‹",35,120,50,38)){cataloguePage=(cataloguePage+pages-1)%pages;}Text("Page "+(cataloguePage+1)+" / "+pages,100,126,190,30);if(Btn("›",280,120,50,38))cataloguePage=(cataloguePage+1)%pages;
  if(Btn(catalogueFaction=="all"?"All factions":data.F(catalogueFaction).name,380,120,500,38)){int n=Array.FindIndex(data.factions,f=>f.id==catalogueFaction)+1;catalogueFaction=n>=data.factions.Length?"all":data.factions[n].id;cataloguePage=0;}
  Text("Original modular miniatures · Shared faction parts, profile-specific loadouts",910,128,640,28,small);
  for(int i=0;i<8;i++){int n=cataloguePage*8+i;if(n>=entries.Length)break;var p=entries[n];float x=35+i%4*390,y=178+i/4*325;Frame(x,y,365,309);Miniature(p,x+8,y+8,175,208);Text(p.name,x+195,y+12,160,70,new GUIStyle(heading){fontSize=18});Text(p.role+"\n"+p.cost+" points\nHP "+p.hp+"  ARM "+p.armour+"\nMOV "+p.move+"  RNG "+p.range+"\nPOW "+p.power+"  PEN "+p.penetration+"\nMELEE "+p.melee+"  OC "+p.oc,x+195,y+87,160,130,small);if(Btn(p.weapon+"\nInspect full profile",x+12,y+228,340,65))inspectedProfile=p;}
  GUI.enabled=true;if(inspectedProfile!=null){var p=inspectedProfile;Rect(0,0,1600,900,new Color(0,0,0,.8f));Frame(155,115,1290,680);Miniature(p,180,150,470,540);Text(p.name,690,155,710,60,heading);string description=data.F(p.faction).name+" / "+p.role+"\n\n"+p.weapon+"\n\n"+NativeProfiles.Summary(p)+"\n\n"+p.lore;float h=body.CalcHeight(new GUIContent(description),680);profileScroll=GUI.BeginScrollView(new Rect(690,235,715,440),profileScroll,new Rect(0,0,680,h+20));Text(description,0,0,680,h,body);GUI.EndScrollView();if(Btn("Close profile",1000,720,390,45)){inspectedProfile=null;profileScroll=Vector2.zero;}}
 }
 void Rulebook(){Banner("FIELD MANUAL / The Quiet Dread native tabletop rules, edition 0.8.");if(referenceText==null){var asset=Resources.Load<TextAsset>("FieldManual");referenceText=asset?asset.text:"Field manual unavailable.";}Frame(170,125,1260,690);var style=new GUIStyle(body){fontSize=19};float height=style.CalcHeight(new GUIContent(referenceText),1140)+40;manualScroll=GUI.BeginScrollView(new Rect(195,145,1210,645),manualScroll,new Rect(0,0,1170,height));Text(referenceText,10,0,1140,height,style);GUI.EndScrollView();}
}
}

