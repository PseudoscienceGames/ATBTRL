using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridData : MonoBehaviour
{
	public Mesh gridBlank;
	public List<Vector3> points = new List<Vector3>();
	public Dictionary<int, List<int>> connections = new Dictionary<int, List<int>>();
	public List<TileData> tiles = new List<TileData>();
	public GameObject test;
	
	public static GridData Instance;
	void Awake(){Instance = this;}
	
	public void GenGrid()
	{
		points = gridBlank.vertices.ToList<Vector3>();
		List<int> tris = gridBlank.triangles.ToList<int>();
		for(int i = 0; i < tris.Count; i += 3)
		{
			int p1 = tris[i];
			int p2 = tris[i + 1];
			int p3 = tris[i + 2];

			if (!connections.ContainsKey(p1))
				connections.Add(p1, new List<int>());
			if (!connections.ContainsKey(p2))
				connections.Add(p2, new List<int>());
			if (!connections.ContainsKey(p3))
				connections.Add(p3, new List<int>());

			if (!connections[p1].Contains(p2))
				connections[p1].Add(p2);
			if (!connections[p1].Contains(p3))
				connections[p1].Add(p3);

			if (!connections[p2].Contains(p1))
				connections[p2].Add(p1);
			if (!connections[p2].Contains(p3))
				connections[p2].Add(p3);

			if (!connections[p3].Contains(p1))
				connections[p3].Add(p1);
			if (!connections[p3].Contains(p2))
				connections[p3].Add(p2);
		}
		for(int i = 0; i < points.Count; i++)
		{
			TileData tile = new TileData(i);
			foreach (int con in connections[i])
				tile.connections.Add(con);
			tiles.Add(tile);
		}
		foreach(TileData t in tiles)
		{
			t.GenMeshData();
		}
	}
}