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
    private TileManager tileManager;

    private bool isRightMouseDown = false;
    private Vector2Int? lastPlacedGridPos = null;

    private TileType selectedTileType = TileType.Snow;

    void Start()
    {
        editorControls = new EditorControls();
        // Fill the map with snow tiles by default
        int mapSize = 64;
        for (int x = 0; x < mapSize; x++)
        {
            for (int y = 0; y < mapSize; y++)
            {
                tileManager.PlaceTile(new Vector2Int(x, y), TileType.Snow);
            }
        }
    }

    public void SetSelectedMapTile(TileType tileType)
    {
        selectedTileType = tileType;
    }

    private void OnSelect()
    {
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

    void FixedUpdate()
    {
        // Drag placement logic
        if (isRightMouseDown && Mouse.current != null && Mouse.current.rightButton.isPressed)
        {
            PlaceTileAtMouse();
        }
        // Reset if mouse released outside of OnRightMouseUp
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
        tileManager.PlaceTile(gridPos, selectedTileType);
    }

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
