using System.Collections.Generic;
using UnityEngine;

public class ScriptableLevel : ScriptableObject
{
	public int LevelIndex;
	public List<SavedTile> GroundTiles;
	public List<SavedTile> UnitTiles;
}

public class SavedTile
{
	public Vector3Int Position;
	public LevelTile Tile;
}
