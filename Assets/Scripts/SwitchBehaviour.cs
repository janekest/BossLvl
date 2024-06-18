using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] DoorBehaviour _doorBehaviour; 

    [SerializeField] bool _isDoorOpenSwitch; // Schalter zum Öffnen der Tür
    [SerializeField] bool _isDoorCloseSwitch; // Schalter zum Schließen der Tür

    float _switchSizeY; // Höhe des Schalters
    Vector3 _switchUpPos;
    Vector3 _switchDownPos;
    float _switchSpeed = 1f; // Geschwindigkeit des Schalters
    float _switchDelay = 0.2f; // Verzögerung des Schalters
    bool _isPressingSwitch = false;

    void Awake()
    {
        _switchSizeY = transform.localScale.y / 2;
        
        _switchUpPos = transform.position; 
        _switchDownPos = new Vector3(transform.position.x, transform.position.y - _switchSizeY, transform.position.z);
    }

    
    void Update()
    {
        if (_isPressingSwitch)
        {
            MoveSwitchDown(); // Bewegt den Schalter nach unten
        }
        else if (!_isPressingSwitch)
        {
            MoveSwitchUp(); // Bewegt den Schalter nach oben
        }
    }
    
    void MoveSwitchDown()
    {
        if (transform.position != _switchDownPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _switchDownPos, _switchSpeed * Time.deltaTime); // Bewegt den Schalter zur unteren Position
        }
    }
    
    void MoveSwitchUp()
    {
        if (transform.position != _switchUpPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _switchUpPos, _switchSpeed * Time.deltaTime); // Bewegt den Schalter zur oberen Position
        }
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.CompareTag("Player"))
        {
            _isPressingSwitch = !_isPressingSwitch; // Schalterzustand umkehren
            
            if (_isDoorOpenSwitch && !_doorBehaviour._isDoorOpen)
            {
                _doorBehaviour._isDoorOpen = !_doorBehaviour._isDoorOpen; // Tür öffnen
            }
            else if (_isDoorCloseSwitch && _doorBehaviour._isDoorOpen)
            {
                _doorBehaviour._isDoorOpen = !_doorBehaviour._isDoorOpen; // Tür schließen
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SwitchUpDelay(_switchDelay)); // Verzögerung bevor der Schalter zurückgesetzt wird
        }
    }

    IEnumerator SwitchUpDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); 
        _isPressingSwitch = false;
    }
}
