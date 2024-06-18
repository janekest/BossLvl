using System;
using UnityEngine;

public class LadderDetector : MonoBehaviour
{
    private float vertical;
    private float speed = 8f; // Klettergeschwindigkeit
    private bool isLadder; 
    private bool isClimbing; // Gibt an ob der Spieler klettert

    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical"); 

        // Überprüft ob eine Leiter erkannt wurde 
        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true; // Der Spieler klettert
        }
        else if (Mathf.Abs(vertical) == 0f)
        {
            isClimbing = false; // Der Spieler hört auf zu klettern
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f; // Deaktiviert die Gravitation während des Kletterns
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f; 
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y); // Behält die  Geschwindigkeit bei
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true; // Leiter wurde betreten 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false; //Leiter wurde verlassen
            isClimbing = false; // Setzt das Klettern zurück
        }
    }
}