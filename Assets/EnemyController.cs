using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Jumping Settings")]
    [SerializeField] private float jumpForce = 10f; // Sprungkraft des Gegners
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundCheckRadius = 0.2f; 
    [SerializeField] private LayerMask groundLayer; // Layer für den Boden
    [SerializeField] private float minJumpInterval = 2f; // Minimales Intervall zwischen Sprüngen
    [SerializeField] private float maxJumpInterval = 5f; 
    [SerializeField] private float jumpDamage = 50f; // Schaden den der Gegner durch Sprung verursacht

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePoint; // Feuerpunkt für das Schießen
    [SerializeField] private float bulletSpeed = 10f; // Geschwindigkeit des Geschosses
    [SerializeField] private float shootingInterval = 2f; // Intervall zwischen Schüssen

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f; // Geschwindigkeit der Bewegung
    [SerializeField] private float minIdleTime = 1f; 
    [SerializeField] private float maxIdleTime = 3f; // Maximale Dauer des Stillstands

    private Rigidbody2D rb;
    private bool isGrounded; 
    private float jumpTimer; // Timer für die Sprungintervalle
    private float shootingTimer; // Timer für die Schussintervalle
    private Transform playerTransform; 
    private bool isFacingRight = true; 
    private float idleTimer; 
    private State currentState; // Aktueller Zustand des Gegners

    private enum State
    {
        Idle, // Zustand: Stillstand
        Moving // Zustand: Bewegung
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 5f;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        jumpTimer = GetRandomJumpInterval(); 
        shootingTimer = shootingInterval; 
        idleTimer = GetRandomIdleTime();
        currentState = State.Idle; // Starten im Zustand "Stillstand"
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Überprüfen ob der Gegner auf dem Boden ist

        // Sprungverhalten 
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0 && isGrounded)
        {
            Jump(); // Ausführen des Sprungs
            jumpTimer = GetRandomJumpInterval(); // Aktualisieren des Sprungtimers
        }

        // Schussverhalten 
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && isGrounded)
        {
            Shoot(); // Ausführen des Schusses
            shootingTimer = shootingInterval; 
        }

        // Bewegungsverhalten 
        switch (currentState)
        {
            case State.Idle:
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    currentState = State.Moving; // Übergang zum Zustand "Bewegung"
                    idleTimer = GetRandomIdleTime(); 
                }
                break;
            case State.Moving:
                MoveTowardsPlayer(); // Bewegung in Richtung des Spielers
                break;
        }

        // Ausrichtung des Gegners zum Spieler
        if (isGrounded)
        {
            RotateTowardsPlayer(); // Ausrichten des Gegners zum Spieler
        }
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            // Zurück zum Stillstandszustand wechseln
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                currentState = State.Idle; // Übergang zum Zustand "Stillstand"
                rb.velocity = new Vector2(0, rb.velocity.y); // Stoppen der Bewegung
                idleTimer = GetRandomIdleTime(); 
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Ausführen des Sprungs
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.velocity = (isFacingRight ? Vector2.right : Vector2.left) * bulletSpeed; // Geschwindigkeit des Geschosses setzen
    }

    private void RotateTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 targetDirection = playerTransform.position - transform.position;

            if (targetDirection.x > 0 && !isFacingRight)
            {
                Flip(); // Drehen des Gegners wenn der Spieler rechts ist und der Gegner nach links schaut
            }
            else if (targetDirection.x < 0 && isFacingRight)
            {
                Flip(); // Drehen des Gegners wenn der Spieler links ist und der Gegner nach rechts schaut
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

       
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Feuerpunkt umdrehen um in die richtige Richtung zu zielen
        Vector3 firePointScale = firePoint.localScale;
        firePointScale.x *= -1;
        firePoint.localScale = firePointScale;
    }

    private float GetRandomJumpInterval()
    {
        return Random.Range(minJumpInterval, maxJumpInterval); // Zufälliges Intervall für den Sprung 
    }

    private float GetRandomIdleTime()
    {
        return Random.Range(minIdleTime, maxIdleTime); // Zufällige Dauer des Stillstands
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>(); // Zugriff auf die Spieler-Gesundheit
            if (playerHealth != null)
            {
                Vector2 contactNormal = collision.GetContact(0).normal; 
                if (contactNormal.y > 0 && rb.velocity.y < 0)
                {
                    playerHealth.TakeDamage(jumpDamage); // Schaden am Spieler verursachen wenn von oben herabgesprungen wird
                }
            }
        }
    }
}



