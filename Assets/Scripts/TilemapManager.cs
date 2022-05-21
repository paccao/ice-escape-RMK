using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

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
					var levelTile = map.GetTile<LevelTile>(pos);
					yield return new SavedTile()
					{
						Position = pos,
						Tile = levelTile
					};
				}
			}
		}
	}

	public void ClearMap() {
		var maps = FindObjectsOfType<Tilemap>();

		foreach (var tilemap in maps)
		{
			tilemap.ClearAllTiles();
		}
	}

	public void LoadMap() { }
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