using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileData
{
	public int tileIndex;
	public Vector3 worldLoc;
	public float height;
	public int heightMax = 2;
	public float heightMod = .25f;
	public List<Vector3> points = new List<Vector3>();
	public List<int> tris = new List<int>();
	public List<int> connections = new List<int>();

	public TileData(int index)
	{
		tileIndex = index;
	}

	public void GenMeshData()
	{
		height = Random.Range(0, heightMax);
		SortConnections();
		worldLoc = GridData.Instance.points[tileIndex];
		for (int i = 0; i < connections.Count; i++)
		{
			int point1 = connections[i];
			int i2;
			if (i == connections.Count - 1)
				i2 = 0;
			else
				i2 = i + 1;
			int point2 = connections[i2];
			FindPoint(point1, point2);
		}
		for (int i = 1; i < points.Count - 1; i++)
		{
			tris.Add(i);
			tris.Add(0);
			tris.Add(i + 1);
		}
	}

	private void FindPoint(int adjacentTile1, int adjacentTile2)
	{
		Vector3 point1 = worldLoc;
		Vector3 point2 = GridData.Instance.points[adjacentTile1];
		Vector3 point3 = GridData.Instance.points[adjacentTile2];
		points.Add(((point1 + point2 + point3) / 3).normalized * (worldLoc.magnitude + height * heightMod));
	}

	public void SortConnections()
	{
		List<int> sortedConnections = new List<int>();
		List<int> connectionsLeft = new List<int>(connections);

		sortedConnections.Add(connectionsLeft[0]);
		connectionsLeft.RemoveAt(0);

		while(sortedConnections.Count != connections.Count)
		{
			int closestConnection = connectionsLeft[0];
			float distance = Vector3.Distance(GridData.Instance.points[sortedConnections[sortedConnections.Count - 1]], GridData.Instance.points[connectionsLeft[0]]);
			foreach(int connection in connectionsLeft)
			{
				if(Vector3.Distance(GridData.Instance.points[sortedConnections[sortedConnections.Count - 1]], GridData.Instance.points[connection]) < distance)
				{
					closestConnection = connection;
					distance = Vector3.Distance(GridData.Instance.points[sortedConnections[sortedConnections.Count - 1]], GridData.Instance.points[connection]);
				}
			}
			sortedConnections.Add(closestConnection);
			connectionsLeft.Remove(closestConnection);
		}
		connections = sortedConnections;
		if(Vector3.Distance(Vector3.zero, GetNormal(GridData.Instance.points[tileIndex], GridData.Instance.points[connections[0]], GridData.Instance.points[connections[1]]) + GridData.Instance.points[tileIndex]) > Vector3.Distance(Vector3.zero, GridData.Instance.points[tileIndex]))
			connections.Reverse();
	}

	Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c)
	{
		Vector3 side1 = b - a;
		Vector3 side2 = c - a;
		return Vector3.Cross(side1, side2).normalized;
	}
}