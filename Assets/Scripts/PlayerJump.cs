using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce;
    private int playerNumber;

    private Rigidbody rb;
    public bool grounded;      // True if the player is standing on the ground
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    public int maxAirJumps;
    public bool jumping = false;
    public int currentJump;
    private Gamepad gamepad;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerNumber = GetComponent<PlayerMovement>().playerNumber;
        jumpForce = PlayerStatManager.playerStats[playerNumber - 1].Jump;
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void Update()
    {
        gamepad = Gamepad.current;

        if (grounded)
            currentJump = 0;

        if (gamepad.aButton.wasPressedThisFrame)
            Jump();
    }

    private void Jump()
    {
        if(currentJump >= maxAirJumps)
        {
            return;
        }

        jumpForce = PlayerStatManager.playerStats[playerNumber].Jump;
        
        rb.velocity = Vector3.up * jumpForce;
        currentJump++;
        StartCoroutine(JumpAnimator(0.07f));
    }

    private IEnumerator JumpAnimator(float time)
    {
        jumping = true;
        float start_time = Time.time;
        float progress = (Time.time - start_time) / time;
        while (progress < 1.0f)
        {
            progress = (Time.time - start_time) / time;
            yield return null;
        }
        jumping = false;
    }

    private IEnumerator LandAnimator(float time)
    {
        float start_time = Time.time;
        float progress = (Time.time - start_time) / time;
        while (progress < 1.0f)
        {
            progress = (Time.time - start_time) / time;
            yield return null;
        }
    }
}
