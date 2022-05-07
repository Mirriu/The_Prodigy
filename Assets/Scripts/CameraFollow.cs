using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followObject;
    public Rigidbody2D followRb;
    private Vector3 intendedPosition;

    private void Start()
    {
        followObject = FindObjectOfType<PlayerController>()?.transform;
        followRb = followObject?.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        intendedPosition = followObject.position;
        intendedPosition += (Vector3)followRb?.velocity / 12.0f;


        transform.position = intendedPosition;

        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }


}
