using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f;
    [SerializeField] private CanvasGroup loseCanvasGroup;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void InstantDeath()
    {
        Die();
    }

    private void Die()
    {
        // Handle player death (e.g., respawn, game over screen)
        Debug.Log("Player Died");
        // Optionally, add more logic to handle player death (e.g., restart level, show game over screen)
        loseCanvasGroup.ShowCanvasGroup();
    }
}