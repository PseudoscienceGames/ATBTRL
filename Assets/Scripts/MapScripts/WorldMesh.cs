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
			verts.AddRange(tile.points);
			foreach(int tri in tile.tris)
			{
				tris.Add(tri + triNum);
			}
			triNum += tile.points.Count;
		}
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		//mesh.RecalculateNormals();
		//mesh.RecalculateTangents();
	}
}
