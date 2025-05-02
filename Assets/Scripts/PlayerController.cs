using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveInput;
    private Vector2 lastMoveDirection; // New: to remember the last horizontal direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lastMoveDirection = Vector2.right; // Default facing right
    }

    void Update()
    {
        // Get input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Normalize diagonal movement
        if (moveInput != Vector2.zero)
        {
            moveInput.Normalize();

            // Save the last move direction if moving horizontally
            if (moveInput.x != 0)
            {
                lastMoveDirection = new Vector2(moveInput.x, 0);
            }
        }

        // Set animator parameters
        animator.SetFloat("MoveX", moveInput.x);
        animator.SetFloat("MoveY", moveInput.y);
        animator.SetBool("IsMoving", moveInput.sqrMagnitude > 0);

        // Flip sprite only when moving horizontally
        if (lastMoveDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Flip
        }
        else if (lastMoveDirection.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Normal
        }
    }

    void FixedUpdate()
    {
        // Move character
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
