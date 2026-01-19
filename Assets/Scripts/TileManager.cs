using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
	// Stores placed tiles
	private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();

	public void PlaceTile(Vector2Int gridPos, GameObject tilePrefab)
	{
		if (tiles.ContainsKey(gridPos))
		{
			Destroy(tiles[gridPos]);
			tiles.Remove(gridPos);
		}
		GameObject tile = Instantiate(tilePrefab, new Vector3(gridPos.x, gridPos.y, 0), Quaternion.identity);
		tiles[gridPos] = tile;
	}

	public void RemoveTile(Vector2Int gridPos)
	{
		if (tiles.ContainsKey(gridPos))
		{
			Destroy(tiles[gridPos]);
			tiles.Remove(gridPos);
		}
	}
}
