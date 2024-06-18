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
        uiLevelManager = FindObjectOfType<UiLevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("goal"))
        {
            Debug.Log("you win!");
            uiLevelManager.OnGameWin();
        }
        if (other.CompareTag("Death"))
        {
            Debug.Log("you Death");
            uiLevelManager.OnGameLose();
        }
    }
    
}
