using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f); // Offset der Kamera 
    private float smoothTime = 0.25f; 
    private Vector3 velocity = Vector3.zero; // Geschwindigkeit der Kamera 

    [SerializeField] private Transform target; // Ziel dem die Kamera folgen soll

   
    void LateUpdate()
    {
        if (target != null) // Überprüfen ob ein Ziel vorhanden ist
        {
            Vector3 targetPosition = target.position + offset; // Zielposition  berechnen
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime); // Kamera weiches Nachführen zur Zielposition
        }
    }
}