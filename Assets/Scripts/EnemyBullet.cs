using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f; // Damage the bullet inflicts

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the bullet upon collision
        }
    }
}