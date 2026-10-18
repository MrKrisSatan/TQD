using UnityEngine;
namespace QuietDread {
public partial class BattleVisuals {
 Material canopyMaterial;Mesh canopyMesh;
 void Canopy(Transform parent,int x,int y,float seed){if(!canopyMaterial)canopyMaterial=Resources.Load<Material>("Canopy");if(!canopyMesh){canopyMesh=new Mesh();canopyMesh.vertices=new[]{new Vector3(-.68f,0,-.68f),new Vector3(-.68f,0,.68f),new Vector3(.68f,0,.68f),new Vector3(.68f,0,-.68f)};canopyMesh.uv=new[]{new Vector2(0,0),new Vector2(0,1),new Vector2(1,1),new Vector2(1,0)};canopyMesh.triangles=new[]{0,1,2,0,2,3};canopyMesh.RecalculateNormals();}var tree=new GameObject("Woodland canopy");tree.transform.SetParent(parent,false);tree.transform.localPosition=new Vector3(x,.9f+seed*.2f,y);tree.transform.localRotation=Quaternion.Euler(0,seed*360,0);tree.AddComponent<MeshFilter>().sharedMesh=canopyMesh;tree.AddComponent<MeshRenderer>().sharedMaterial=canopyMaterial;}
 Color TerrainColor(int type,int x,int y){float n=Mathf.PerlinNoise(x*.27f,y*.27f);if(type==5)return new Color(.08f,.25f,.31f)+Color.white*n*.035f;if(type==6)return new Color(.22f,.27f,.15f)+Color.white*n*.025f;if(type==4)return new Color(.19f,.25f,.12f)+Color.white*n*.035f;if(type==1||type==9)return new Color(.29f,.28f,.25f);if(type==2)return new Color(.32f,.29f,.21f);if(type==3)return new Color(.24f,.24f,.18f);return battle!=null&&battle.biome=="ruined city"?new Color(.28f,.29f,.28f)+Color.white*n*.045f:new Color(.27f,.31f,.17f)+Color.white*n*.055f;}
 void Scenery(Transform parent,int x,int y,int type){float seed=(x*17+y*31)%19/19f;Color bark=new Color(.23f,.18f,.11f),stone=new Color(.36f,.38f,.32f);if(type==4){Shape(parent,PrimitiveType.Cylinder,new Vector3(x,.45f,y),new Vector3(.13f,.45f,.13f),bark);Canopy(parent,x,y,seed);}

  if(type==8){for(int i=0;i<3;i++)Boulder(parent,new Vector3(x+(i-1)*.23f,.3f+i*.13f,y+(i%2)*.2f),new Vector3(.5f,.5f+i*.13f,.5f),seed*180+i*35);}
  if(type==7){Shape(parent,PrimitiveType.Cube,new Vector3(x,.28f,y),new Vector3(.95f,.55f,.23f),stone);for(int i=-1;i<=1;i++)Shape(parent,PrimitiveType.Cube,new Vector3(x+i*.35f,.62f,y),new Vector3(.19f,.18f,.28f),stone*.85f);}
  if(type==9){Shape(parent,PrimitiveType.Cube,new Vector3(x,.55f,y),new Vector3(.93f,1.1f,.88f),new Color(.4f,.38f,.31f));Shape(parent,PrimitiveType.Cube,new Vector3(x,1.14f,y),new Vector3(1.02f,.16f,.97f),new Color(.2f,.25f,.25f));for(int side=-1;side<=1;side+=2){Shape(parent,PrimitiveType.Cube,new Vector3(x+side*.22f,.62f,y-.45f),new Vector3(.18f,.25f,.02f),new Color(.07f,.1f,.1f));}Shape(parent,PrimitiveType.Cube,new Vector3(x+.3f,1.4f,y+.2f),new Vector3(.17f,.55f,.17f),stone);}
  if(type==6){for(int i=0;i<3;i++){float a=i*2.1f+seed;Shape(parent,PrimitiveType.Cylinder,new Vector3(x+Mathf.Cos(a)*.3f,.16f,y+Mathf.Sin(a)*.3f),new Vector3(.025f,.18f,.025f),new Color(.42f,.4f,.16f));}Shape(parent,PrimitiveType.Cylinder,new Vector3(x,.004f,y),new Vector3(.65f,.008f,.56f),new Color(.12f,.24f,.22f));}
  if(type==5){for(int i=0;i<2;i++)Shape(parent,PrimitiveType.Cube,new Vector3(x+(i-.5f)*.3f,-.018f,y+(seed-.5f)*.7f),new Vector3(.26f,.008f,.025f),new Color(.25f,.43f,.45f));}
  if(type==0&&(x*13+y*7)%9==0){for(int i=0;i<3;i++)Shape(parent,PrimitiveType.Cube,new Vector3(x+(i-1)*.09f,.06f,y),new Vector3(.025f,.12f,.025f),new Color(.32f,.36f,.15f),false,new Vector3(0,0,i*15-15));}
 }
}
}

