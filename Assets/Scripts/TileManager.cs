using System.IO;
using System;
using UnityEngine.Serialization;
using UnityEngine;
using System.Collections.Generic;

public enum TileType
{
	Ground,
	IceNormal,
	Snow,
	Wall
}

[Serializable]
public class TilePrefabEntry
{
	public TileType tileType;
	public GameObject prefab;
}

[Serializable]
public class TileSaveData
{
	public int x;
	public int y;
	public TileType tileType;
}

[Serializable]
public class MapSaveData
{
	public List<TileSaveData> tiles = new List<TileSaveData>();
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

	// Export current map data to a serializable structure
	public MapSaveData GetMapSaveData()
	{
		MapSaveData data = new MapSaveData();
		foreach (var kvp in tiles)
		{
			TileSaveData tileData = new TileSaveData
			{
				x = kvp.Key.x,
				y = kvp.Key.y,
				tileType = GetTileTypeForPrefab(kvp.Value)
			};
			data.tiles.Add(tileData);
		}
		return data;
	}

	// Helper to get TileType from a tile GameObject instance
	private TileType GetTileTypeForPrefab(GameObject tileObj)
	{
		foreach (var entry in tilePrefabs)
		{
			// Compare prefab reference (ignoring instance)
			if (tileObj != null && tileObj.name.StartsWith(entry.Value.name))
				return entry.Key;
		}
		return TileType.Snow; // Default fallback
	}

	public void SaveMapToFile(string path)
	{
		MapSaveData data = GetMapSaveData();
		string json = JsonUtility.ToJson(data, true);
		File.WriteAllText(path, json);
		Debug.Log($"Map saved to {path}");
	}

	public void LoadMapFromFile(string path)
	{
		if (!File.Exists(path))
		{
			Debug.LogWarning($"Map file not found: {path}");
			return;
		}
		string json = File.ReadAllText(path);
		MapSaveData data = JsonUtility.FromJson<MapSaveData>(json);
		ClearMap();
		foreach (var tile in data.tiles)
		{
			PlaceTile(new Vector2Int(tile.x, tile.y), tile.tileType);
		}
		Debug.Log($"Map loaded from {path}");
	}

	public void ClearMap()
	{
		foreach (var tile in tiles.Values)
		{
			if (tile != null)
				Destroy(tile);
		}
		tiles.Clear();
	}
}
