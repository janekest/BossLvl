using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtcoinCount;
    public int coinCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        txtcoinCount.text = coinCounter.ToString();
    }
}
