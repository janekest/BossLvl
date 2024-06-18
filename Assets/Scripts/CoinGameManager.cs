using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtcoinCount;  //die Anzeige der Münzanzahl
    public int coinCounter; // Zähler für die  Münzen
    
 
    void Start()
    {
    }

    void Update()
    {
        txtcoinCount.text = coinCounter.ToString(); // Aktualisiert den Text der aktuellen Münzanzahl
    }
}

