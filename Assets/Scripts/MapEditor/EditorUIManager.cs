using UnityEngine;
using System.Collections.Generic;

public class EditorUIManager : MonoBehaviour
{
    [SerializeField]
    private MapEditorController mapEditorSystem;

    [SerializeField]
    private TileManager tileManager;

    [SerializeField]
    private MapNamePromptUI mapNamePromptUI;

    private Dictionary<string, TileType> buttonToTileType = new Dictionary<string, TileType>
    {
        { "GroundButton", TileType.Ground },
        { "IceNormalButton", TileType.IceNormal },
        { "SnowButton", TileType.Snow },
        { "WallButton", TileType.Wall }
    };

    public void OnTileButtonClicked(string buttonName)
    {
        if (buttonToTileType.TryGetValue(buttonName, out TileType tileType))
        {
            mapEditorSystem.SetSelectedMapTile(tileType);
            Debug.Log($"Selected tile: {buttonName} ({tileType})");
        }
        else if (buttonName == "SaveButton")
        {
            mapNamePromptUI.Show(OnSaveMapNameEntered, "Save");
        }
        else if (buttonName == "LoadButton")
        {
            mapNamePromptUI.Show(OnLoadMapNameEntered, "Load");
        }
        else
        {
            Debug.LogWarning("Button name not mapped: " + buttonName);
        }
    }

    private void OnSaveMapNameEntered(string mapName)
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, mapName + ".json");
        tileManager.SaveMapToFile(filePath);
    }

    private void OnLoadMapNameEntered(string mapName)
    {
        string filePath = System.IO.Path.Combine(Application.persistentDataPath, mapName + ".json");
        tileManager.LoadMapFromFile(filePath);
    }
}