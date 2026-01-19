using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;

public class MapEditorController : MonoBehaviour
{
    private Vector2 targetPosition;
    private EditorControls editorControls;

    [SerializeField]
    private int selectedMapTile;

    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private TileManager tileManager;

    void Start()
    {
        editorControls = new EditorControls();
    }

    // Called by EditorUIManager
    public void SetSelectedMapTile(int prefabIndex)
    {
        selectedMapTile = prefabIndex;
    }

    private void OnSelect()
    {
        // Prevent selection if pointer is over UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Debug.Log("Select position: " + targetPosition);
    }

    private void OnPlace()
    {
        // Prevent placement if pointer is over UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Debug.Log("Place position: " + targetPosition);

        // Convert world position to grid position (round to nearest int)
        Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(targetPosition.x), Mathf.RoundToInt(targetPosition.y));

        // Place the selected tile prefab at the grid position
        if (tilePrefabs != null && tilePrefabs.Length > 0 && selectedMapTile >= 0 && selectedMapTile < tilePrefabs.Length)
        {
            tileManager.PlaceTile(gridPos, tilePrefabs[selectedMapTile]);
        }
        else
        {
            Debug.LogWarning("Invalid tile selection or prefabs not set.");
        }
    }
}
