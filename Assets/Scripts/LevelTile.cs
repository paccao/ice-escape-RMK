using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level Tile", menuName = "2D/Tiles/Level Tile")]
public class LevelTile : Tile
{
	public TileType Type;
}

[Serializable]
public enum TileType
{
	// Ground
	GrassBackground = 0,
	GrassPathDot = 1,
	GrassPathMiddle = 2,
	GrassPathTop = 3,
	GrassPathBottom = 4,

	Snow = 5,
	Ice = 6,
	Cobble = 7,
	Rock = 8,

	// Unit
	Player = 500,

	// Props
	SnowPineTop = 1000,
	SnowPineBottom = 1001,
	SnowBush = 1002,
	SnowLog = 1003,
	SnowRockPile = 1004

}
