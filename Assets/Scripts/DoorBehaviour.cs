using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool _isDoorOpen = false; //  Tür (offen oder geschlossen)
    Vector3 _doorClosedPos; // Position der geschlossenen Tür
    Vector3 _doorOpenPos; 
    float _doorSpeed = 10f; 

    // Start is called before the first frame update
    void Awake()
    {
        _doorClosedPos = transform.position; 
        _doorOpenPos = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z); 
    }

   
    void Update()
    {
        if (_isDoorOpen)
        {
            OpenDoor(); // Tür öffnen
        }
        else if (!_isDoorOpen)
        {
            CloseDoor(); // Tür schließen
        }
    }

    void OpenDoor()
    {
        if (transform.position != _doorOpenPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _doorOpenPos, _doorSpeed * Time.deltaTime); // Bewegt die Tür zur offenen Position
        }
    }
    
    void CloseDoor()
    {
        if (transform.position != _doorClosedPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _doorClosedPos, _doorSpeed * Time.deltaTime); // Bewegt die Tür zur geschlossenen Position
        }
    }
}