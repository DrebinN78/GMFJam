using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool mustStay;
    public Door door;
    public bool position;

    void Start()
    {
        
    }

    void Update()
    {
        if(position == true)
        {
            door.isOpen = true;
        }
        else
        {
            door.isOpen = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
                position = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (mustStay)
            {
                position = false;
            }
        }
    }
}
