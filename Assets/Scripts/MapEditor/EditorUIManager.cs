using UnityEngine;
using System.Collections.Generic;

public class EditorUIManager : MonoBehaviour
{
    [SerializeField]
    private MapEditorController mapEditorSystem;

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
        else
        {
            Debug.LogWarning("Button name not mapped: " + buttonName);
        }
    }
}