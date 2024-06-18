using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraktions : MonoBehaviour
{
    private UiLevelManager uiLevelManager; 

    // Start is called before the first frame update
    void Start()
    {
        uiLevelManager = FindObjectOfType<UiLevelManager>(); // Findet UiLevelManager in der Szene
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("goal"))
        {
            Debug.Log("you win!"); // Loggt den Gewinn
            uiLevelManager.OnGameWin(); // Ruft den GewinnPanel auf
        }
        if (other.CompareTag("Death"))
        {
            Debug.Log("you Death"); // Loggt den Tod
            uiLevelManager.OnGameLose(); // Ruft den VerliererPanelm auf
        }
    }
}

