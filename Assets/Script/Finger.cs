using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    public bool once;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(once == true)
            {
            collision.GetComponent<PlayerMove>().finger += 1;

            Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
            }
            else
            {
                once = true;
            }
        }
    }
}
