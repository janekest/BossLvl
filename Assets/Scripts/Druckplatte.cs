using UnityEngine;

public class PressurePlateController : MonoBehaviour
{
    public DoorController doorController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stone"))
        {
            doorController.OpenDoor(); // Tür öffnen, wenn der Stein die Druckplatte berührt
        }
    }
}