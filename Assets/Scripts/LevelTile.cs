using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Level Tile", menuName = "2D/Tiles/Level Tile")]
public class LevelTile : Tile
{
	// Tile information, could be anything like for example onCollisionEnter event to set different player states depending on TileType.
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
