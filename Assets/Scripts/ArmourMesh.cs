using System.Collections.Generic;
using UnityEngine;
namespace QuietDread {
public partial class BattleVisuals {
 Mesh armourMesh;
 Mesh ArmourMesh(){if(armourMesh)return armourMesh;var vertices=new List<Vector3>();var triangles=new List<int>();
  System.Action<Vector3[]> face=points=>{Vector3 centre=Vector3.zero;foreach(var p in points)centre+=p;centre/=points.Length;if(Vector3.Dot(Vector3.Cross(points[1]-points[0],points[2]-points[0]),centre)<0)System.Array.Reverse(points);int first=vertices.Count;vertices.AddRange(points);for(int i=1;i<points.Length-1;i++){triangles.Add(first);triangles.Add(first+i);triangles.Add(first+i+1);}};
  const float edge=.40f,outer=.5f;
  // Six recessed square faces, twelve bevel strips and eight corner caps.
  for(int axis=0;axis<3;axis++)for(int sign=-1;sign<=1;sign+=2){var points=new Vector3[4];int[] a={-1,1,1,-1},b={-1,-1,1,1};for(int i=0;i<4;i++){Vector3 p=Vector3.zero;p[axis]=sign*outer;p[(axis+1)%3]=a[i]*edge;p[(axis+2)%3]=b[i]*edge;points[i]=p;}face(points);}
  for(int axis=0;axis<3;axis++)for(int s=-1;s<=1;s+=2)for(int t=-1;t<=1;t+=2){int a=(axis+1)%3,b=(axis+2)%3;var points=new Vector3[4];for(int i=0;i<4;i++){Vector3 p=Vector3.zero;p[axis]=(i==0||i==3?-1:1)*edge;p[a]=s*(i<2?outer:edge);p[b]=t*(i<2?edge:outer);points[i]=p;}face(points);}
  for(int x=-1;x<=1;x+=2)for(int y=-1;y<=1;y+=2)for(int z=-1;z<=1;z+=2)face(new[]{new Vector3(x*outer,y*edge,z*edge),new Vector3(x*edge,y*outer,z*edge),new Vector3(x*edge,y*edge,z*outer)});
  armourMesh=new Mesh{name="Bevelled armour plate"};armourMesh.SetVertices(vertices);armourMesh.SetTriangles(triangles,0);armourMesh.RecalculateNormals();armourMesh.RecalculateBounds();return armourMesh;
 }
}
}
