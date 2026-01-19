using UnityEngine;

using System.Collections.Generic;

public class EditorUIManager : MonoBehaviour
{
    [SerializeField]
    private MapEditorController mapEditorSystem;

    // Map button names to prefab indices
    private Dictionary<string, int> buttonToPrefabIndex = new Dictionary<string, int>
    {
        { "GroundButton", 0 },
        { "IceNormalButton", 1 },
        { "SnowButton", 2 },
        { "WallButton", 3 }
    };

    public void OnTileButtonClicked(string buttonName)
    {
        if (buttonToPrefabIndex.TryGetValue(buttonName, out int prefabIndex))
        {
            mapEditorSystem.SetSelectedMapTile(prefabIndex);
            Debug.Log("Selected tile: " + buttonName + " (index " + prefabIndex + ")");
        }
        else
        {
            Debug.LogWarning("Button name not mapped: " + buttonName);
        }
    }
}