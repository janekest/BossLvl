using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    private void Update()
    {
        Flip(); // Ruft  Flip auf
    }

    void Flip()
    {
        // Überprüft ob die 'S'-Taste gedrückt wurde
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            transform.Rotate(0, 180, 0); // Dreht das GameObject um 180 Grad um die Y-Achse
        }
    }
}

