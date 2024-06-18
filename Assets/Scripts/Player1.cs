using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player1 : MonoBehaviour
{
    private Rigidbody2D rb;
    private float inputDirection;

    [SerializeField] private float jumpForce = 5f; // Sprungkraft
    [SerializeField] private float movementSpeed = 5f; // Bewegungsgeschwindigkeit
    [SerializeField] private float sneakSpeed = 2.5f; // Geschwindigkeit beim Schleichen
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private float groundCheckRadius = 1f; 
    [SerializeField] private LayerMask layerGroundCheck; 
    [SerializeField] private int startingJumpCount; // Anfangssprunganzahl
    private int jumpCount; // Verbleibende Sprünge

    private bool isFacingRight = true; // Blickrichtung
    public bool ClimbingAllowed { get; set; } 

    private UiLevelManager uiLevelManager;
    private bool isSneaking = false; 
    public CoinGameManager cgm; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        uiLevelManager = FindObjectOfType<UiLevelManager>(); // Findet UiLevelManager in der Szene
        Debug.Log("Start!");

        // Abonniert die Sprungaktion
        var playerInput = GetComponent<PlayerInput>();
        playerInput.actions.FindAction("Jump").performed += ctx => OnJump();
        playerInput.actions.FindAction("Sneak").performed += OnSneakStart;
        playerInput.actions.FindAction("Sneak").canceled += OnSneakEnd;
    }

    private void FixedUpdate()
    {
        float currentSpeed = isSneaking ? sneakSpeed : movementSpeed; // Bestimmt die aktuelle Geschwindigkeit
        rb.velocity = new Vector2(inputDirection * currentSpeed, rb.velocity.y); 
    }

    void OnJump()
    {
        if (Physics2D.OverlapCircle(groundCheckPosition.position, groundCheckRadius, layerGroundCheck))
        {
            jumpCount = startingJumpCount; // Setzt die Sprunganzahl zurück
        }

        if (jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Führt den Sprung aus
            jumpCount--; 
        }
    }

    void OnMove(InputValue inputValue)
    {
        inputDirection = inputValue.Get<float>(); 
        Debug.Log("Move! " + inputDirection);

        if (inputDirection > 0 && !isFacingRight)
        {
            Flip(); // Dreht den Spieler nach rechts
        }
        else if (inputDirection < 0 && isFacingRight)
        {
            Flip(); // Dreht den Spieler nach links
        }
    }

    void OnSneakStart(InputAction.CallbackContext context)
    {
        isSneaking = true; // Startet den Schleichmodus
        Debug.Log("Sneaking");
    }

    void OnSneakEnd(InputAction.CallbackContext context)
    {
        isSneaking = false; // Beendet den Schleichmodus
        Debug.Log("Stopped Sneaking");
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale; // Holt die aktuelle Skalierung
        currentScale.x *= -1;
        transform.localScale = currentScale; 

        isFacingRight = !isFacingRight; // Ändert die Blickrichtung
    }

    //  wird aufgerufen, wenn der Spieler stirbt
    public void PlayerDied()
    {
        uiLevelManager.OnGameLose(); // Löst das Spielverloren-Ereignis aus
    }

    //  wird aufgerufen wenn der Spieler gestampft wird (durch MonsterStomp-Skript)
    public void OnStomp()
    {
        Debug.Log("Player stomp action!"); 

        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("coin"))
        {
            Destroy(other.gameObject); // Zerstört das Münzobjekt
            cgm.coinCounter++; // Erhöht den Münzzähler
        }
    }
}


