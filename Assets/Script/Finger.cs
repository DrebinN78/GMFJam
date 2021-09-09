using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerMove>().finger += 1;

            Destroy(gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
