using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapEditorController : MonoBehaviour
{
    private Vector2 targetPosition;
    private EditorControls editorControls;

    [SerializeField]
    private int selectedMapTile;

    void Start()
    {
        editorControls = new EditorControls();
    }

    void Update()
    {

    }

    private void OnSelect()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Debug.Log("Select position: " + targetPosition);
    }

    private void OnPlace()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Debug.Log("Place position: " + targetPosition);
    }
}
