using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilemapManager))]
public class TileMapManagerEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		var script = (TilemapManager)target;

		if (GUILayout.Button("Save Map"))
		{
			script.SaveMap();
		}
	}
}
