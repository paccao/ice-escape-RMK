using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 targetPosition;
    private PlayerControls playerControls;
    private Rigidbody2D rb;

    void Start()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnMove()
    {
        Debug.Log("Move");
        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * 5);
    }
}
