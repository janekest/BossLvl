using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class Shoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab; 

    void Update()
    {
        // Überprüft ob die linke Maustaste  gedrückt wurde
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Erzeugt ein neues Projektil an der Position des Schusspunkts
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
    }
}

