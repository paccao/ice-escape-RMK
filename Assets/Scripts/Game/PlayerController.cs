using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Vector2 targetPosition;
    private float targetAngle = 0f;
    private PlayerControls playerControls;
    public float groundMovementSpeed = 2.5f;
    public float iceNormalMovementSpeed = 5.0f;
    public float movementSpeed;
    public float rotationSpeed = 300f;
    public Transform bodyTransform;

    private PlayerState currentState;
    public bool IsOnIce { get; set; }
    public bool IsDead { get; set; }

    void Start()
    {
        playerControls = new PlayerControls();
        targetPosition = transform.position; // prevent from moving to 0.0 onStart
        currentState = new GroundState(this);
        currentState.Enter();
    }

    private void OnMove()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (targetPosition - (Vector2)bodyTransform.position).normalized;
        targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    private void Update()
    {
        currentState.Update();
        float newAngle = Mathf.MoveTowardsAngle(bodyTransform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        bodyTransform.rotation = Quaternion.Euler(0,0, newAngle);
        // transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed);
    }

    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Deadly"))
        {
            Debug.Log("You died!");
            // Stop move and eventual abilities here
            movementSpeed = 0f;
            rotationSpeed = 0f;
            yield return new WaitForSeconds(1.5f);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Restart
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsOnIce = false;
            return;
        }

        if (collision.gameObject.CompareTag("Ice_Normal"))
        {
            IsOnIce = true;
            return;
        }
    }
}
