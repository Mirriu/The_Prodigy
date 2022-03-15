using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float[] thrusters = new float[2];
    [SerializeField] private float turningSpeed = 5.0f, acceleration = 10.0f, wallBoostForward = 3.0f, wallBoostOutward = 3.0f;
    public bool inverted = true;

    public bool boostStraight = false;

    public LineRenderer[] thrusterLines;
    public Transform[] boostParticles;
    public float thrusterLineLength = 5.0f;

    public float wallPushForce = 3.0f, wallPushDistance = 7.0f;

    private float defaultDrag, extraDrag;

    private AudioSource audioSource;
    private float vol;

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

        defaultDrag = rb.drag;
        extraDrag = defaultDrag * wallPushForce;
        audioSource = GetComponent<AudioSource>();
        vol = audioSource.volume;
    }

    private void Update()
    {
        
        thrusterLines[0]?.SetPosition(0, thrusterLines[0].transform.position);
        thrusterLines[1]?.SetPosition(0, thrusterLines[1].transform.position);

        thrusterLines[0]?.SetPosition(1, thrusterLines[0].transform.position - thrusterLines[0].transform.up * thrusters[0] * thrusterLineLength);
        thrusterLines[1]?.SetPosition(1, thrusterLines[1].transform.position - thrusterLines[1].transform.up * thrusters[1] * thrusterLineLength);
    }

    private void FixedUpdate()
    {
        SetThrusterStrengths();
        transform.rotation *= Quaternion.Euler(0f, 0f, (thrusters[1] - thrusters[0]) * turningSpeed);
        rb.AddForce(transform.up * (thrusters[0] + thrusters[1]) * acceleration);
        audioSource.volume = 0.0f;
        AddWallBoost();
        WallSlowDown();

        if (transform.position.y > 10000) SceneManager.LoadScene(0);
    }

    private void SetThrusterStrengths()
    {
        if(usingGamepad)
        {
            thrusters[inverted ? 1 : 0] = controls.leftTrigger.ReadValue();
            thrusters[inverted ? 0 : 1] = controls.rightTrigger.ReadValue();
        }
        else
        {
            thrusters[inverted ? 1 : 0] = Mathf.Lerp(thrusters[inverted ? 1 : 0], (Keyboard.current.eKey.isPressed ? 1f : 0f), 0.06f);
            thrusters[inverted ? 0 : 1] = Mathf.Lerp(thrusters[inverted ? 0 : 1], (Keyboard.current.iKey.isPressed ? 1f : 0f), 0.06f);
        }

    }

    float GetThrusterLineLength(int index)
    {
        return (thrusterLines[index].GetPosition(1) - thrusterLines[index].GetPosition(1)).magnitude;
    }

    void AddWallBoost()
    {
        for (int i = 0; i < thrusters.Length; i++)
        {
            var hit = Physics2D.Raycast(thrusterLines[i].transform.position, -thrusterLines[i].transform.up, thrusters[i] * thrusterLineLength);
            if(hit)
            {
                rb.AddForce(thrusterLines[i].transform.up * acceleration * thrusters[i] * wallBoostOutward);
                rb.AddForce(transform.up * acceleration * thrusters[i] * wallBoostForward);
                boostParticles[i].transform.position = hit.point;
                boostParticles[i].transform.right = -transform.up;
                audioSource.volume = vol;
            }
            else
            {
                boostParticles[i].transform.position = new Vector3(0f, 0f, -10000f);
            }

        }
    }



    void WallSlowDown()
    {
        var hit = Physics2D.Raycast(transform.position + transform.right * wallPushDistance, -transform.right, 2f * wallPushDistance);
       
        if(hit)
        {
            var direction = -(hit.point - (Vector2)transform.position).normalized;
            rb.AddForce(direction * wallPushForce);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + transform.right * wallPushDistance, -transform.right * wallPushDistance * 2f);
    }

}
