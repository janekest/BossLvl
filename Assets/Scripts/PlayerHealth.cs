using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // Spieler-Gesundheit
    [SerializeField] private CanvasGroup loseCanvasGroup; 

    public void TakeDamage(float damage)
    {
        health -= damage; // Reduziert die Gesundheit um den Schaden
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
      
        Debug.Log("Player Died");
        
        loseCanvasGroup.ShowCanvasGroup(); // Zeigt das Verlierer-Canvas an
    }
}