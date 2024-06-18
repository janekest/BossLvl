using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    [SerializeField] private GameObject redParticles; // Prefab für Partikel-Effekte beim Zerstören
    [SerializeField] private float health = 100f; // Gesundheit des Gegners

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Prüfen, ob der Weak Point getroffen wurde
        if (collision.gameObject.CompareTag("WeakPoint"))
        {
            TakeDamage(health); // Gegner wird zerstört
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Instantiate(redParticles, transform.position, Quaternion.identity); // Partikel-Effekte erzeugen
            Destroy(gameObject); // Zerstört den Gegner
        }
    }
}