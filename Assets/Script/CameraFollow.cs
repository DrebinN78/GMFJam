using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public int limitNumber;

    public List<Transform> leftL = new List<Transform>();
    public List<Transform> rightL = new List<Transform>();
    public List<Transform> bottomL = new List<Transform>();
    public List<Transform> topL = new List<Transform>();

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }

    }

    void Update()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftL[limitNumber].position.x, rightL[limitNumber].position.x),
            Mathf.Clamp(transform.position.y, bottomL[limitNumber].position.y, topL[limitNumber].position.y),
            transform.position.z
        );

    }
}
