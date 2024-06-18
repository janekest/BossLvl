using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Jumping Settings")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float minJumpInterval = 2f;
    [SerializeField] private float maxJumpInterval = 5f;
    [SerializeField] private float jumpDamage = 50f;

    [Header("Shooting Settings")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float shootingInterval = 2f;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minIdleTime = 1f;
    [SerializeField] private float maxIdleTime = 3f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float jumpTimer;
    private float shootingTimer;
    private Transform playerTransform;
    private bool isFacingRight = true;
    private float idleTimer;
    private State currentState;

    private enum State
    {
        Idle,
        Moving
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 5f;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        jumpTimer = GetRandomJumpInterval();
        shootingTimer = shootingInterval;
        idleTimer = GetRandomIdleTime();
        currentState = State.Idle;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Handle jumping
        jumpTimer -= Time.deltaTime;
        if (jumpTimer <= 0 && isGrounded)
        {
            Jump();
            jumpTimer = GetRandomJumpInterval();
        }

        // Handle shooting
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && isGrounded)
        {
            Shoot();
            shootingTimer = shootingInterval;
        }

        // Handle movement
        switch (currentState)
        {
            case State.Idle:
                idleTimer -= Time.deltaTime;
                if (idleTimer <= 0)
                {
                    currentState = State.Moving;
                    idleTimer = GetRandomIdleTime();
                }
                break;
            case State.Moving:
                MoveTowardsPlayer();
                break;
        }

        // Handle rotation towards player
        if (isGrounded)
        {
            RotateTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

            // Transition back to idle state
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                currentState = State.Idle;
                rb.velocity = new Vector2(0, rb.velocity.y);
                idleTimer = GetRandomIdleTime();
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.velocity = (isFacingRight ? Vector2.right : Vector2.left) * bulletSpeed;
    }

    private void RotateTowardsPlayer()
    {
        if (playerTransform != null)
        {
            Vector3 targetDirection = playerTransform.position - transform.position;

            if (targetDirection.x > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (targetDirection.x < 0 && isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        // Flip the enemy sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // Flip the fire point to face the correct direction
        Vector3 firePointScale = firePoint.localScale;
        firePointScale.x *= -1;
        firePoint.localScale = firePointScale;
    }

    private float GetRandomJumpInterval()
    {
        return Random.Range(minJumpInterval, maxJumpInterval);
    }

    private float GetRandomIdleTime()
    {
        return Random.Range(minIdleTime, maxIdleTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Vector2 contactNormal = collision.GetContact(0).normal;
                if (contactNormal.y > 0 && rb.velocity.y < 0)
                {
                    playerHealth.TakeDamage(jumpDamage);
                }
            }
        }
    }
}


