using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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

    private bool isRightMouseDown = false;
    private Vector2Int? lastPlacedGridPos = null;

    void Start()
    {
        editorControls = new EditorControls();
    }

    public void SetSelectedMapTile(int prefabIndex)
    {
        selectedMapTile = prefabIndex;
    }

    private void OnSelect()
    {
        // Prevent selection if pointer is over UI
        if (IsPointerOverUI())
            return;

        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Debug.Log("Select position: " + targetPosition);
    }

    private void OnPlace()
    {
        isRightMouseDown = true;
        PlaceTileAtMouse();
    }

    private void OnRightMouseUp()
    {
        isRightMouseDown = false;
        lastPlacedGridPos = null;
    }

    void Update()
    {
        // Drag placement logic
        if (isRightMouseDown && Mouse.current != null && Mouse.current.rightButton.isPressed)
        {
            PlaceTileAtMouse();
        }
        // Optionally, reset if mouse released outside of OnRightMouseUp
        if (isRightMouseDown && Mouse.current != null && !Mouse.current.rightButton.isPressed)
        {
            isRightMouseDown = false;
            lastPlacedGridPos = null;
        }
    }

    private void PlaceTileAtMouse()
    {
        if (IsPointerOverUI())
            return;
        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2Int gridPos = new Vector2Int(Mathf.RoundToInt(targetPosition.x), Mathf.RoundToInt(targetPosition.y));

        if (lastPlacedGridPos.HasValue && lastPlacedGridPos.Value == gridPos)
            return; // Don't place again on same cell

        lastPlacedGridPos = gridPos;
        Debug.Log("Place position: " + targetPosition);
        if (tilePrefabs != null && tilePrefabs.Length > 0 && selectedMapTile >= 0 && selectedMapTile < tilePrefabs.Length)
        {
            tileManager.PlaceTile(gridPos, tilePrefabs[selectedMapTile]);
        }
        else
        {
            Debug.LogWarning("Invalid tile selection or prefabs not set.");
        }
    }

    // Raycast to check if mouse pointer is over UI
    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null)
            return false;
        PointerEventData eventData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
}
