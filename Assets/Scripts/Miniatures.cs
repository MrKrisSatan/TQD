using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
namespace QuietDread {
// Original modular miniatures. The same construction is used by cards and battle pieces.
public partial class BattleVisuals {
 static int ModelSeed(string s){int n=17;foreach(char c in s)n=unchecked(n*31+c);return n&0x7fffffff;}
 Transform Group(Transform parent,string name,Vector3 position){var t=new GameObject(name).transform;t.SetParent(parent,false);t.localPosition=position;return t;}
 void Link(Transform r,Vector3 a,Vector3 b,float radius,Color c){var g=Shape(r,PrimitiveType.Cylinder,(a+b)*.5f,new Vector3(radius,(b-a).magnitude*.5f,radius),c);g.transform.localRotation=Quaternion.FromToRotation(Vector3.up,b-a);}
 void Plate(Transform r,Vector3 at,Vector3 size,Color c,float angle=0){var g=new GameObject("Armour plate");g.transform.SetParent(r,false);g.transform.localPosition=at;g.transform.localScale=size;g.transform.localEulerAngles=new Vector3(0,0,angle);g.AddComponent<MeshFilter>().sharedMesh=ArmourMesh();g.AddComponent<MeshRenderer>().sharedMaterial=Mat(c);}
 void Orb(Transform r,Vector3 at,Vector3 size,Color c,bool glow=false){Shape(r,PrimitiveType.Sphere,at,size,c,glow);}
 void BuildUnit(Transform root,Profile p,int team){
  Color c=ColorOf(battle.data.F(p.faction).color),dark=Color.Lerp(c,Color.black,.68f),metal=new Color(.36f,.4f,.43f),bone=new Color(.82f,.77f,.61f);
  Shape(root,PrimitiveType.Cylinder,new Vector3(0,.025f,0),new Vector3(.92f,.025f,.92f),team==0?new Color(.16f,.4f,.49f):new Color(.48f,.18f,.16f));
  Shape(root,PrimitiveType.Cylinder,new Vector3(0,.054f,0),new Vector3(.85f,.008f,.85f),new Color(.19f,.18f,.14f));
  var sculpt=Group(root,"Sculpt / "+p.id,Vector3.zero);
  if(p.titan){TitanModel(sculpt,p,c,dark,metal,bone);TitanDetails(sculpt,p,c,metal,bone);}
  else if(p.kind=="vehicle"||p.kind=="artillery")Engine(sculpt,p,c,dark,metal,bone);
  else {bool mounted=p.visual=="cavalry";int count=p.commander?1:mounted||p.visual=="monster"?2:4;
   for(int i=0;i<count;i++){var f=Group(sculpt,"Trooper "+(i+1),new Vector3(count==1?0:(i%2-.5f)*.39f,0,count<=2?0:(i/2-.5f)*.39f));f.localScale=Vector3.one*(p.commander?1.34f:mounted?.85f:p.visual=="monster"?1.2f:.83f);Trooper(f,p,c,dark,metal,bone,i);}
  }
  // Build facing +X; team and selected deployment edge supply the final orientation.
  if(team==1)root.localRotation=Quaternion.Euler(0,180,0);
  BakeSculpt(sculpt);
 }
 void Trooper(Transform r,Profile p,Color c,Color dark,Color metal,Color bone,int index){
  int seed=ModelSeed(p.id);bool rat=p.faction=="vermin",green=p.faction=="cinder"||p.faction=="kiln",alien=p.faction=="pelagic"||p.faction=="saurian",dead=p.faction=="sepulchre",elf=p.faction=="lumen"||p.faction=="vesper",dwarf=p.faction=="vault",demon=p.faction=="rift"||p.faction=="horn"||p.faction=="oathscar",holy=p.faction=="dawn"||p.faction=="hollow";
  c=Color.Lerp(c,p.stealth?Color.black:bone,p.stealth?.55f:p.armour>=3?.18f:0);
  bool mounted=p.visual=="cavalry";Color skin=rat?new Color(.48f,.36f,.29f):green?new Color(.29f,.43f,.19f):dead?bone:alien?Color.Lerp(c,bone,.35f):new Color(.66f,.49f,.39f);
  if(mounted){Mount(r,p,c,dark,metal);r=Group(r,"Rider",new Vector3(0,.24f,0));}
  float h=dwarf?.83f:elf?1.1f:1;var body=Group(r,"Armoured body",Vector3.zero);body.localScale=new Vector3(green?1.2f:elf?.88f:1,h,1);
  for(int s=-1;s<=1;s+=2){float stride=(index%2==0?s:-s)*.025f;var hip=new Vector3(-.015f,.28f,s*.065f);var knee=new Vector3(.03f+stride,.17f,s*.07f);var ankle=new Vector3(-stride,.075f,s*.075f);Link(body,hip,knee,.065f,dark);Link(body,knee,ankle,.06f,metal);Plate(body,knee,new Vector3(.075f,.075f,.09f),c);Plate(body,ankle+new Vector3(.035f,-.018f,0),new Vector3(.13f,.065f,.09f),dark);}
  Orb(body,new Vector3(0,.36f,0),new Vector3(.21f,.26f,.22f),dark);Plate(body,new Vector3(.065f,.38f,0),new Vector3(.075f,.2f,.235f),c,12);Plate(body,new Vector3(.11f,.405f,0),new Vector3(.012f,.035f,.11f),bone);Plate(body,new Vector3(.01f,.265f,0),new Vector3(.19f,.045f,.24f),metal);
  for(int s=-1;s<=1;s+=2){Orb(body,new Vector3(-.005f,.43f,s*.14f),new Vector3(.17f,.14f,.12f),c);Link(body,new Vector3(.01f,.41f,s*.14f),new Vector3(.07f,.31f,s*.16f),.06f,dark);Link(body,new Vector3(.07f,.31f,s*.16f),new Vector3(.18f,.34f,s*.105f),.06f,c);Orb(body,new Vector3(.18f,.34f,s*.105f),Vector3.one*.065f,skin);Plate(body,new Vector3(.04f,.28f,s*.14f),new Vector3(.09f,.1f,.05f),dark);}
  Link(body,new Vector3(0,.46f,0),new Vector3(0,.51f,0),.07f,metal);Orb(body,new Vector3(.015f,.55f,0),new Vector3(.16f,.18f,.17f),rat||green||alien||dead?skin:c);
  Plate(body,new Vector3(.096f,.57f,0),new Vector3(.014f,.025f,.115f),new Color(.46f,.9f,.83f));
  if(rat||green||alien){Orb(body,new Vector3(.11f,.525f,0),new Vector3(.16f,.075f,.11f),skin);Orb(body,new Vector3(.185f,.53f,0),Vector3.one*.035f,dark);for(int s=-1;s<=1;s+=2){Orb(body,new Vector3(-.015f,.63f,s*.08f),new Vector3(.055f,.09f,.055f),rat?skin:dark);Plate(body,new Vector3(.145f,.505f,s*.04f),new Vector3(.025f,.04f,.025f),bone);}}
  if(rat||alien){Vector3 last=new Vector3(-.09f,.29f,0);for(int i=1;i<=5;i++){var next=new Vector3(-.09f-i*.065f,.29f-i*.037f,Mathf.Sin(i*.9f)*.07f);Link(body,last,next,.045f-i*.006f,skin);last=next;}}
  if(demon)for(int s=-1;s<=1;s+=2){Link(body,new Vector3(0,.6f,s*.06f),new Vector3(-.04f,.72f,s*.13f),.04f,bone);Link(body,new Vector3(-.04f,.72f,s*.13f),new Vector3(.02f,.77f,s*.15f),.022f,bone);}
  if(dwarf)for(int i=-1;i<=1;i++)Link(body,new Vector3(.1f,.52f,i*.035f),new Vector3(.12f,.37f,i*.025f),.035f,bone);
  if(dead){for(int s=-1;s<=1;s+=2)Orb(body,new Vector3(.09f,.57f,s*.04f),Vector3.one*.04f,Color.black);for(int i=0;i<4;i++)Plate(body,new Vector3(.1f,.31f+i*.03f,0),new Vector3(.04f,.012f,.15f),bone);}
  // Packs, exhausts and equipment vary with the actual weapon category and profile identifier.
  Plate(body,new Vector3(-.13f,.38f,0),new Vector3(.09f,.19f,.16f),metal);for(int s=-1;s<=1;s+=2)Link(body,new Vector3(-.14f,.31f,s*.09f),new Vector3(-.14f,.48f,s*.09f),.04f,dark);
  if(p.flying)for(int s=-1;s<=1;s+=2){Plate(body,new Vector3(-.15f,.43f,s*.22f),new Vector3(.19f,.05f,.35f),c);Orb(body,new Vector3(-.2f,.31f,s*.11f),new Vector3(.055f,.1f,.055f),Color.cyan,true);}
  string weapon=(p.weapon??"").ToLowerInvariant();
  if(p.caster){Link(body,new Vector3(.2f,.08f,.16f),new Vector3(.2f,.78f,.16f),.025f,metal);Orb(body,new Vector3(.2f,.8f,.16f),Vector3.one*.11f,c,true);for(int s=-1;s<=1;s+=2)Link(body,new Vector3(.2f,.72f,.16f),new Vector3(.2f+s*.08f,.85f,.16f),.018f,bone);}
  else if(p.melee>p.power&&p.range<5){Link(body,new Vector3(.17f,.28f,.14f),new Vector3(.18f,.63f,.14f),.025f,metal);Plate(body,new Vector3(.19f,.63f,.14f),new Vector3(.075f,.22f,.022f),bone,-15);Plate(body,new Vector3(.12f,.36f,-.2f),new Vector3(.055f,.28f,.2f),c);Plate(body,new Vector3(.153f,.36f,-.2f),new Vector3(.012f,.19f,.025f),bone);}
  else {float length=p.range>=9?.4f:.25f;Plate(body,new Vector3(.22f,.35f,.1f),new Vector3(.23f,.075f,.075f),dark);Link(body,new Vector3(.24f,.36f,.1f),new Vector3(.24f+length,.36f,.1f),weapon.Contains("cannon")||p.blast>0?.06f:.026f,metal);Plate(body,new Vector3(.23f,.29f,.1f),new Vector3(.06f,.09f,.045f),metal);if(p.range>=8)Link(body,new Vector3(.19f,.415f,.1f),new Vector3(.32f,.415f,.1f),.03f,metal);if(p.blast>0)Orb(body,new Vector3(.18f,.35f,-.13f),Vector3.one*.13f,c);}
  if(p.commander||holy||elf||p.stealth){for(int i=-2;i<=2;i++)Plate(body,new Vector3(-.12f,.32f,i*.045f),new Vector3(.035f,.37f,.05f),i%2==0?c:dark,-12);}
  if(p.armour>=3)for(int side=-1;side<=1;side+=2){Plate(body,new Vector3(.01f,.46f,side*.14f),new Vector3(.2f,.065f,.16f),bone);Plate(body,new Vector3(.07f,.19f,side*.07f),new Vector3(.035f,.16f,.11f),c);}
  if(p.armour==0){Plate(body,new Vector3(.105f,.36f,0),new Vector3(.015f,.16f,.16f),skin);for(int i=0;i<3;i++)Plate(body,new Vector3(.117f,.3f+i*.045f,0),new Vector3(.015f,.018f,.18f),dark);}
  if(p.gas){for(int side=-1;side<=1;side+=2)Shape(body,PrimitiveType.Capsule,new Vector3(-.17f,.37f,side*.07f),new Vector3(.09f,.18f,.09f),new Color(.55f,.65f,.18f));Link(body,new Vector3(-.17f,.36f,.1f),new Vector3(.2f,.34f,.1f),.023f,metal);Orb(body,new Vector3(.1f,.52f,0),Vector3.one*.09f,metal);}
  if(p.ability=="repair"||p.ability=="fortify"){Link(body,new Vector3(-.16f,.25f,-.12f),new Vector3(-.23f,.67f,-.12f),.023f,metal);Plate(body,new Vector3(-.23f,.67f,-.12f),new Vector3(.12f,.09f,.035f),bone);Plate(body,new Vector3(-.19f,.3f,.13f),new Vector3(.14f,.13f,.08f),new Color(.55f,.4f,.15f));}
  if(p.commander){Link(body,new Vector3(-.16f,.2f,-.1f),new Vector3(-.16f,.92f,-.1f),.018f,metal);Plate(body,new Vector3(-.16f,.81f,.015f),new Vector3(.015f,.16f,.23f),c);Plate(body,new Vector3(-.15f,.81f,.015f),new Vector3(.015f,.1f,.035f),bone);}
  for(int i=0;i<1+seed%3;i++)Plate(body,new Vector3(.11f,.35f+i*.028f,-.07f),new Vector3(.012f,.012f,.045f),bone);
 }
 void Mount(Transform r,Profile p,Color c,Color dark,Color metal){bool organic=new[]{"horn","saurian","pelagic","vermin","verdant"}.Contains(p.faction);if(organic){Orb(r,new Vector3(0,.24f,0),new Vector3(.55f,.25f,.22f),dark);Orb(r,new Vector3(.25f,.34f,0),new Vector3(.22f,.22f,.17f),c);for(int s=-1;s<=1;s+=2)for(int j=-1;j<=1;j+=2){Link(r,new Vector3(j*.17f,.25f,s*.09f),new Vector3(j*.2f,.07f,s*.12f),.055f,metal);Plate(r,new Vector3(j*.21f,.06f,s*.12f),new Vector3(.09f,.06f,.07f),dark);}Link(r,new Vector3(-.2f,.25f,0),new Vector3(-.4f,.14f,0),.035f,c);}else{Plate(r,new Vector3(0,.21f,0),new Vector3(.58f,.17f,.24f),c);for(int j=-1;j<=1;j+=2)Shape(r,PrimitiveType.Cylinder,new Vector3(j*.22f,.14f,0),new Vector3(.25f,.095f,.25f),dark,false,new Vector3(90,0,0));Plate(r,new Vector3(.23f,.34f,0),new Vector3(.1f,.14f,.23f),metal,-20);}}
 void Engine(Transform r,Profile p,Color c,Color dark,Color metal,Color bone){int seed=ModelSeed(p.id);bool gun=p.kind=="artillery";Plate(r,new Vector3(0,.24f,0),new Vector3(.68f,.23f,.54f),dark);for(int s=-1;s<=1;s+=2){Plate(r,new Vector3(0,.17f,s*.32f),new Vector3(.82f,.23f,.16f),metal);for(int i=0;i<7;i++){Plate(r,new Vector3(-.36f+i*.12f,.29f,s*.32f),new Vector3(.07f,.025f,.18f),dark);Shape(r,PrimitiveType.Cylinder,new Vector3(-.3f+i*.1f,.16f,s*.409f),new Vector3(.12f,.018f,.12f),dark,false,new Vector3(90,0,0));}Plate(r,new Vector3(0,.32f,s*.32f),new Vector3(.7f,.08f,.19f),c);}
  Shape(r,PrimitiveType.Cylinder,new Vector3(0,.39f,0),new Vector3(.43f,.07f,.43f),metal);Plate(r,new Vector3(0,.49f,0),new Vector3(.4f,.2f,.4f),c);for(int i=0;i<(p.blast>0?1:1+seed%2);i++){float z=i*.13f-.065f;Vector3 a=new Vector3(.12f,.52f,z),b=new Vector3(gun?.68f:.6f,gun?.73f:.52f,z);Link(r,a,b,gun?.12f:.07f,metal);Orb(r,b,new Vector3(.04f,.075f,.075f),Color.black);}
  Plate(r,new Vector3(-.21f,.38f,0),new Vector3(.12f,.04f,.35f),metal);for(int i=0;i<5;i++)Plate(r,new Vector3(-.21f,.405f,-.13f+i*.065f),new Vector3(.1f,.01f,.025f),dark);Link(r,new Vector3(-.27f,.37f,-.21f),new Vector3(-.27f,.79f,-.21f),.012f,metal);for(int s=-1;s<=1;s+=2){Orb(r,new Vector3(.35f,.32f,s*.2f),new Vector3(.025f,.06f,.06f),bone,true);Plate(r,new Vector3(0,.51f,s*.21f),new Vector3(.17f,.045f,.012f),bone);}if(gun)for(int s=-1;s<=1;s+=2)Link(r,new Vector3(-.2f,.2f,s*.2f),new Vector3(-.48f,.07f,s*.43f),.06f,metal);
 }
 void TitanDetails(Transform r,Profile p,Color c,Color metal,Color bone){bool organic=new[]{"queen","beast","tree","daemon","brute"}.Contains(p.visual);for(int s=-1;s<=1;s+=2)for(int i=0;i<5;i++){if(organic){Orb(r,new Vector3(-.25f+i*.13f,1.1f,s*.65f),new Vector3(.22f,.13f,.12f),i%2==0?c:bone);Link(r,new Vector3(-.3f+i*.16f,1.65f,s*.36f),new Vector3(-.4f+i*.16f,1.88f,s*.46f),.055f,bone);}else{Plate(r,new Vector3(-.45f+i*.2f,1.33f,s*.68f),new Vector3(.12f,.18f,.035f),metal);Orb(r,new Vector3(-.45f+i*.2f,1.4f,s*.705f),Vector3.one*.04f,bone);}}if(!organic)for(int i=0;i<6;i++)Plate(r,new Vector3(-.5f,1.5f,-.35f+i*.14f),new Vector3(.25f,.025f,.06f),Color.black);}
 void BakeSculpt(Transform root){
  // Merge static parts by material; preserve the existing articulated titan legs.
  var filters=root.GetComponentsInChildren<MeshFilter>().Where(f=>f.GetComponent<Stride>()==null).ToArray();
  foreach(var group in filters.GroupBy(f=>f.GetComponent<Renderer>().sharedMaterial)){var mesh=new Mesh{name="Miniature surface",indexFormat=UnityEngine.Rendering.IndexFormat.UInt32};mesh.CombineMeshes(group.Select(f=>new CombineInstance{mesh=f.sharedMesh,transform=root.worldToLocalMatrix*f.transform.localToWorldMatrix}).ToArray());var go=new GameObject("Painted surface");go.transform.SetParent(root,false);go.AddComponent<MeshFilter>().sharedMesh=mesh;go.AddComponent<MeshRenderer>().sharedMaterial=group.Key;go.AddComponent<OwnedMiniatureMesh>();}
  foreach(var f in filters){f.gameObject.SetActive(false);Destroy(f.gameObject);}
 }
}
public class OwnedMiniatureMesh:MonoBehaviour {void OnDestroy(){var f=GetComponent<MeshFilter>();if(f&&f.sharedMesh)Destroy(f.sharedMesh);}}
}
