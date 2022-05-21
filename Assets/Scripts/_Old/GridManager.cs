using UnityEngine;

public class GridManager : MonoBehaviour
{
	[SerializeField] private int _width, _height;

	[SerializeField] private OldTile _tilePrefab;

	[SerializeField] private Transform _cam;

	void Start()
	{
		GenerateGrid();
	}

	void GenerateGrid()
	{
		for (int x = 0; x < _width; x++)
		{
			for (int y = 0; y < _height; y++)
			{
				var spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
				spawnedTile.name = $"Tile {x} {y}";

				// Is X even and is Y uneven OR is X uneven and Y even?
				// Color separate colors on different tiles.
				var isOffset = x % 2 != y % 2;
				spawnedTile.Init(isOffset);
			}
		}

		_cam.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10);
	}
}
