using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputDirection;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float sneakSpeed = 2.5f; // Speed while sneaking
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius = 1f;
    [SerializeField] private LayerMask layerGroundCheck;
    [SerializeField] private int startingJumpCount;
    private int jumpCount;

    private bool isFacingRight = true;
    public bool ClimbingAllowed { get; set; }

    private UiLevelManager uiLevelManager; // Reference to UiLevelManager
    private bool isSneaking = false; // Sneak state
    public CoinGameManager cgm;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        uiLevelManager = FindObjectOfType<UiLevelManager>(); // Find UiLevelManager in scene
        Debug.Log("Start!");

        // Subscribe to the jump action
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions.FindAction("Jump").performed += ctx => OnJump();
        playerInput.actions.FindAction("Sneak").performed += OnSneakStart;
        playerInput.actions.FindAction("Sneak").canceled += OnSneakEnd;
    }

    private void FixedUpdate()
    {
        float currentSpeed = isSneaking ? sneakSpeed : movementSpeed;
        rb.velocity = new Vector2(inputDirection * currentSpeed, rb.velocity.y);
    }

    void OnJump()
    {
        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, layerGroundCheck))
        {
            jumpCount = startingJumpCount;
        }

        if (jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
        }
    }

    void OnMove(InputValue inputValue)
    {
        inputDirection = inputValue.Get<float>();
        Debug.Log("Move! " + inputDirection);

        if (inputDirection > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (inputDirection < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void OnSneakStart(InputAction.CallbackContext context)
    {
        isSneaking = true;
        Debug.Log("Sneaking");
    }

    void OnSneakEnd(InputAction.CallbackContext context)
    {
        isSneaking = false;
        Debug.Log("Stopped Sneaking");
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    // Method called when player dies
    public void PlayerDied()
    {
        // Trigger game over in UiLevelManager
        uiLevelManager.OnGameLose();
    }

    // Method called when player is stomped (triggered by MonsterStomp script)
    public void OnStomp()
    {
        // Action to perform when player is stomped (e.g., decrease health, play animation)
        Debug.Log("Player stomp action!");

        // Example: Destroy the player (this can be replaced with appropriate game logic)
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            cgm.coinCounter++;
        }
    }
}


