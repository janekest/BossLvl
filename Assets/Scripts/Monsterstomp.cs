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
                player.OnStomp(); 
            }
        }
        else if (collision.gameObject.CompareTag("WeakPoint"))
        {
            //  Zerstören des übergeordneten Gegners
            Transform parentTransform = collision.gameObject.transform.parent; // Zugriff auf Transform des Schwachpunkts
            if (parentTransform != null)
            {
                Destroy(parentTransform.gameObject); // Zerstört das  Spielobjekt
            }
        }
    }
}