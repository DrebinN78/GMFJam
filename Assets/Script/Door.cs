using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;

    public GameObject openTrans;
    public GameObject closeTrans;

    public float speed;

    void Start()
    {
        openTrans.transform.parent = null;
        closeTrans.transform.parent = null;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.position = Vector2.MoveTowards(transform.position, openTrans.transform.position, speed);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, closeTrans.transform.position, speed);
        }
    }
}
