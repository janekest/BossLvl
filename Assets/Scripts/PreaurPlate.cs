using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreaurPlate : MonoBehaviour
{

    public Vector3 originalPos;
    bool moveBack = false;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision )
    {
        if (collision.transform.name == "Player")
        {
         
            transform.Translate(-0,01f, 0);
            moveBack = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            collision.transform.parent = transform;
            GetComponent<SpriteRenderer>().color = Color.blue;

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.name == "Player")
        {
            moveBack = true;
            collision.transform.parent = null;
            GetComponent<SpriteRenderer>().color = Color.white;
        }   
    }


    private void Update()
    {
        if (moveBack)
        {
            if (transform.position.y < originalPos.y)
            {
                transform.Translate(0,0.1f,0);
            }   
        }
        else
        {
            moveBack = false;
        }
    }
}