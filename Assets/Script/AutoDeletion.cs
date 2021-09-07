using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeletion : MonoBehaviour
{
    public float time;
    void Start()
    {
        Destroy(gameObject, time);   
    }

    void Update()
    {
        
    }
}
