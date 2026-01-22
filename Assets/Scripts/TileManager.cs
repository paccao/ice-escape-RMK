using UnityEngine;
using System.Collections.Generic;

public enum TileType
{
	Ground,
	IceNormal,
	Snow,
	Wall
}

[System.Serializable]
public class TilePrefabEntry
{
	public TileType tileType;
	public GameObject prefab;
}

public class TileManager : MonoBehaviour
{
	// Stores placed tiles
	private Dictionary<Vector2Int, GameObject> tiles = new Dictionary<Vector2Int, GameObject>();

	public List<TilePrefabEntry> tilePrefabList = new List<TilePrefabEntry>();

	private Dictionary<TileType, GameObject> tilePrefabs;

	[SerializeField]
	private GameObject worldParent;

	void Awake()
	{
		tilePrefabs = new Dictionary<TileType, GameObject>();
		foreach (var entry in tilePrefabList)
		{
			if (!tilePrefabs.ContainsKey(entry.tileType))
			{
				tilePrefabs.Add(entry.tileType, entry.prefab);
			}
		}
	}

	public void PlaceTile(Vector2Int gridPos, TileType tileType)
	{
		if (!tilePrefabs.ContainsKey(tileType) || tilePrefabs[tileType] == null)
		{
			Debug.LogWarning($"Tile prefab for {tileType} not set.");
			return;
		}
		if (tiles.ContainsKey(gridPos))
		{
			Destroy(tiles[gridPos]);
			tiles.Remove(gridPos);
		}
		if (worldParent == null)
		{
			Debug.LogWarning("World GameObject not found in scene. Creating at root.");
		}
		GameObject tile = Instantiate(
			tilePrefabs[tileType],
			new Vector3(gridPos.x, gridPos.y, 0),
			Quaternion.identity,
			worldParent != null ? worldParent.transform : null
		);
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
