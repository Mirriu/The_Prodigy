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

    public LineRenderer[] thrusterLines;
    public float thrusterLineLength = 5.0f;


    Gamepad controls;
    Keyboard keyboard;
    bool usingGamepad = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = Gamepad.current;
        usingGamepad = controls != null;
        if(controls == null)
        {
            keyboard = Keyboard.current;
        }
    }

    private void FixedUpdate()
    {
        SetThrusterStrengths();
        transform.rotation *= Quaternion.Euler(0f, 0f, (thrusters[1] - thrusters[0]) * turningSpeed);
        rb.AddForce(transform.up * (thrusters[0] + thrusters[1]) * acceleration);
    }

    private void SetThrusterStrengths()
    {
        if(usingGamepad && false)
        {
            thrusters[inverted ? 1 : 0] = controls.leftTrigger.ReadValue();
            thrusters[inverted ? 0 : 1] = controls.rightTrigger.ReadValue();
        }
        else
        {
            thrusters[inverted ? 1 : 0] = Mathf.Lerp(thrusters[inverted ? 1 : 0], (keyboard.eKey.isPressed ? 1f : 0f), 0.06f);
            thrusters[inverted ? 0 : 1] = Mathf.Lerp(thrusters[inverted ? 0 : 1], (keyboard.iKey.isPressed ? 1f : 0f), 0.06f);
        }



        thrusterLines[0]?.SetPosition(0, thrusterLines[0].transform.position);
        thrusterLines[1]?.SetPosition(0, thrusterLines[1].transform.position);

        thrusterLines[0]?.SetPosition(1, thrusterLines[0].transform.position - transform.up * thrusters[0] * thrusterLineLength);
        thrusterLines[1]?.SetPosition(1, thrusterLines[1].transform.position - transform.up * thrusters[1] * thrusterLineLength);

    }

    void AddWallBoost()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            var hit = Physics2D.Raycast(thrusterLines[i].transform.position, -thrusterLines[i].transform.up, thrusters[i]);
            if(hit)
            {
                rb.AddForce(transform.up * acceleration * thrusters[i] * 1000.0f);
            }
        }
    }
}
