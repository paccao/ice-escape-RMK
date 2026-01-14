using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 targetPosition;
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    public float groundMovementSpeed = 2.5f;
    public float iceNormalMovementSpeed = 5.0f;
    private float movementSpeed;

    void Start()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        targetPosition = transform.position; // prevent from moving to 0.0 onStart
        movementSpeed = groundMovementSpeed; // Default
    }

    private void OnMove()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Deadly"))
        {
            Debug.Log("You died!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Enter Ground");
            movementSpeed = groundMovementSpeed;
        }

        if (collision.gameObject.CompareTag("Ice_Normal"))
        {
            Debug.Log("Enter Ice");
            movementSpeed = iceNormalMovementSpeed;
        }

    }
}
