using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using System;

public class TilemapManager : MonoBehaviour
{
	[SerializeField] private Tilemap _groundMap, _unitMap;
	[SerializeField] private int _levelIndex;

	public void SaveMap()
	{
		var newLevel = ScriptableObject.CreateInstance<ScriptableLevel>();

		newLevel.LevelIndex = _levelIndex;
		newLevel.name = $"Level {_levelIndex}";

		newLevel.GroundTiles = GetTilesFromMap(_groundMap).ToList();
		newLevel.UnitTiles = GetTilesFromMap(_unitMap).ToList();

		ScriptableObjectUtility.SaveLevelFile(newLevel);

		IEnumerable<SavedTile> GetTilesFromMap(Tilemap map)
		{
			foreach (var pos in map.cellBounds.allPositionsWithin)
			{
				if (map.HasTile(pos))
				{
					// TODO: Add Tile maps in the editor for each tile type.
					var levelTile = map.GetTile<LevelTile>(pos); // TODO: This returns null for some reason
					Debug.Log(levelTile);
					yield return new SavedTile()
					{
						Position = pos,
						Tile = levelTile
					};
				}
			}
		}
	}

	public void ClearMap()
	{
		var maps = FindObjectsOfType<Tilemap>();

		foreach (var tilemap in maps)
		{
			tilemap.ClearAllTiles();
		}
	}

	public void LoadMap()
	{
		/// Load map
		var level = Resources.Load<ScriptableLevel>($"Levels/Level {_levelIndex}");
		Debug.Log(level);
		/// Check if it exists
		if (level == null)
		{
			Debug.LogError($"Level {_levelIndex} does not exist.");
			return;
		}

		/// Clear anything left over
		ClearMap();

		// Set tiles from saved tiles by specific tile type so that we have control over specific tile types behaviours
		// Gives granular control for all different types of tiles.
		/// Generate map
		foreach (var savedTile in level.GroundTiles)
		{
			Debug.Log($"Saved tile: {savedTile}, Tile: {savedTile.Tile}, Type: {savedTile.Tile.Type}");
			switch (savedTile.Tile.Type)
			{
				case TileType.GrassBackground:
					_groundMap.SetTile(savedTile.Position, savedTile.Tile);
					break;
				case TileType.Cobble:
					_groundMap.SetTile(savedTile.Position, savedTile.Tile);
					break;
				case TileType.Snow:
					_groundMap.SetTile(savedTile.Position, savedTile.Tile);
					break;
				case TileType.Ice:
					_groundMap.SetTile(savedTile.Position, savedTile.Tile);
					break;
				case TileType.Rock:
					_groundMap.SetTile(savedTile.Position, savedTile.Tile);
					break;

				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		// foreach (var savedTile in level.UnitTiles)
		// {
		// 	switch (savedTile.Tile.Type)
		// 	{
		// 		case TileType.Player:
		// 			_groundMap.SetTile(savedTile.Position, savedTile.Tile);
		// 			break;

		// 		default:
		// 			throw new ArgumentOutOfRangeException();
		// 	}
		// }

		// foreach (var savedTile in level.EnemyTiles)
		// {
		// 	switch (savedTile.Tile.Type)
		// 	{
		// 		case TileType.ExampleEnemy:
		// 			_groundMap.SetTile(savedTile.Position, savedTile.Tile);
		// 			// Add enemy to a list in the GameManager for example
		// 			break;
		// 		case TileType.ExampleGhostEnemy:
		// 			_groundMap.SetTile(savedTile.Position, savedTile.Tile);
		// 			// Add enemy to a list in the GameManager for example
		//			// Spawn a particle system on the ghost
		// 			break;

		// 		default:
		// 			throw new ArgumentOutOfRangeException();
		// 	}
		// }
	}
}

#if UNITY_EDITOR

public static class ScriptableObjectUtility
{
	public static void SaveLevelFile(ScriptableLevel level)
	{
		AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{level.name}.asset");

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}
}

#endif