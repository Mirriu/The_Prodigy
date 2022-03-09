using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float[] thrusters = new float[2];
    [SerializeField] private float turningSpeed = 5.0f, acceleration = 10.0f;
    public bool inverted = true;

    Gamepad controls;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = Gamepad.current;
    }

    private void FixedUpdate()
    {
        SetThrusterStrengths();
        transform.rotation *= Quaternion.Euler(0f, 0f, (thrusters[1] - thrusters[0]) * turningSpeed * (inverted ? -1f : 1f));
        rb.AddForce(transform.up * (thrusters[0] + thrusters[1]) * acceleration);
    }

    private void SetThrusterStrengths()
    {
        thrusters[0] = controls.leftTrigger.ReadValue();
        thrusters[1] = controls.rightTrigger.ReadValue();
    }
}
