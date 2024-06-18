using UnityEngine;

public class MonsterStomp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player1 player = collision.gameObject.GetComponent<Player1>();
            if (player != null)
            {
               // player.OnStomp(); // Methode aufrufen, die den Spieler stört
            }
        }
        else if (collision.gameObject.CompareTag("WeakPoint"))
        {
            // Hier kannst du die Logik einfügen, die den übergeordneten Gegner zerstört
            Transform parentTransform = collision.gameObject.transform.parent;
            if (parentTransform != null)
            {
                Destroy(parentTransform.gameObject);
            }
        }
    }
}