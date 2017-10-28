using UnityEngine;
using System.Collections.Generic;

namespace Caeludum
{
	public class Math
	{
		public static int FindNearestTile(Vector3 worldLoc, int lastTileIndex)
		{
			bool closestFound = false;
			while(!closestFound)
			{
				Vector3 lastWorldLoc = GridData.Instance.points[lastTileIndex];
				float distanceFromWorldLoc = Vector3.Distance(worldLoc, lastWorldLoc);
				closestFound = true;
				foreach(int adjacentPoint in GridData.Instance.connections[lastTileIndex])
				{
					if(Vector3.Distance(worldLoc, GridData.Instance.points[adjacentPoint]) < distanceFromWorldLoc)
					{
						lastTileIndex = adjacentPoint;
						closestFound = false;
					}
				}
			}
			return lastTileIndex;	
		}

		public static List<int> FindTilesInRange(int tileIndex, int range)
		{
			List<int> inRange = new List<int>();
			inRange.Add(tileIndex);
			for(int i = 1; i <= range; i++)
			{
				List<int> newTiles = new List<int>();
				foreach(int possibleTile in inRange)
				{
					foreach(int connection in GridData.Instance.connections[possibleTile])
					{
						if(!inRange.Contains(connection) && !newTiles.Contains(connection))
						{
							newTiles.Add(connection);
						}
					}
				}
				inRange.AddRange(newTiles);
			}
			inRange.Remove(tileIndex);
			return inRange;
		}
	}
}