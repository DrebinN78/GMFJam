using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool mustStay;
    public Door door;
    public bool position;

    public List<GameObject> itemNearMe = new List<GameObject>();

    void Start()
    {
        
    }

    void Update()
    {
        if(itemNearMe.Count > 0)
        {
            position = true;
        }
        else
        {
            position = false;
        }

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
        if(collision.tag == "Player" || collision.tag == "Finger")
        {
            itemNearMe.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Finger")
        {
            if (mustStay)
            {
                itemNearMe.Remove(collision.gameObject);
            }
        }
    }
}
