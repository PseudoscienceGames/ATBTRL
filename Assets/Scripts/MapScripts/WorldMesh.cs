using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WorldMesh : MonoBehaviour
{
	public List<Vector3> verts = new List<Vector3>();
	public List<int> tris = new List<int>();

	public void GenMesh(List<TileData> tiles)
	{
		int triNum = 0;
		foreach(TileData tile in tiles)
		{
			List<int> vertIndex = new List<int>();
			//verts.AddRange(tile.points);
			foreach(Vector3 p in tile.points)
			{
				bool dupe = false;
				Vector3 derp = p;
				foreach(Vector3 vert in verts)
				{
					if (Vector3.Distance(vert, p) < 0.01f)
					{
						derp = vert;
						dupe = true;
					}
				}
				if (!dupe)
				{
					verts.Add(p);
				}
				vertIndex.Add(verts.IndexOf(derp));
				
			}
			foreach(int tri in tile.tris)
			{
				tris.Add(vertIndex[tri]);
			}
			triNum += tile.points.Count;
		}
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.RecalculateNormals();
		//mesh.RecalculateTangents();
	}
}
