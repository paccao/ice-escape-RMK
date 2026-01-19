using UnityEngine;

using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
	// Stores placed tiles by grid position
	private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();

	// Place a tile at a given grid position
	public void PlaceTile(Vector2Int gridPos, GameObject tilePrefab)
	{
		if (tiles.ContainsKey(gridPos))
		{
			// Optionally destroy the old tile
			Destroy(tiles[gridPos]);
			tiles.Remove(gridPos);
		}
		GameObject tile = Instantiate(tilePrefab, new Vector3(gridPos.x, gridPos.y, 0), Quaternion.identity);
		tiles[gridPos] = tile;
	}

	// Remove a tile at a given grid position
	public void RemoveTile(Vector2Int gridPos)
	{
		if (tiles.ContainsKey(gridPos))
		{
			Destroy(tiles[gridPos]);
			tiles.Remove(gridPos);
		}
	}

	// Get the tile at a given grid position
	// public GameObject GetTile(Vector2Int gridPos)
	// {
	// 	tiles.TryGetValue(gridPos, out GameObject tile);
	// 	return tile;
	// }
}
