using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public PlayerController controller;

    public float dashSpeed;
    public float dashDuration;
    public float minDashGapTime;
    public float dashCooldown;
    public int healthCost;

    public bool debug_gap;
    public bool debug_cooldown;

    private Rigidbody2D rb;
    public int maxDashes;
    public int currentDash;
    public bool inDash;
    private int direction;

    private Gamepad gamepad;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();

        inDash = false;
        direction = 1;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.x < 0) direction = -1;
        else if (rb.velocity.x > 0) direction = 1; // if rb.velocity.x == 0, then the direction is the same as it has been.
    }

    private void Update()
    {
        gamepad = Gamepad.current;

        if (!controller.enableControls)
            return;

        if (gamepad.bButton.wasPressedThisFrame)
            Dash();
    }

    private void Dash()
    {
        if(currentDash >= maxDashes)
        {
            controller.adjustHealth(healthCost);
        }
        inDash = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0.0f;
        currentDash++;
        rb.velocity = new Vector2(dashSpeed, 0) * direction;

        StartCoroutine(Dash_Straight());
    }

    private IEnumerator Dash_Straight()
    {
        float start_time = Time.time;
        float progress = (Time.time - start_time) / dashDuration;
        while (progress < 1.0f)
        {
            progress = (Time.time - start_time) / dashDuration;
            yield return null;
        }
        rb.gravityScale = 1.0f;
        inDash = false;
        rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y * 0.5f);

        StartCoroutine(Check_Consecutive_Dashes());
    }

    private IEnumerator Check_Consecutive_Dashes()
    {
        debug_gap = true;
        float start_time = Time.time;
        float progress = (Time.time - start_time) / minDashGapTime;
        bool hasDashed = false;
        while (progress < 1.0f && !hasDashed)
        {
            progress = (Time.time - start_time) / minDashGapTime;
            if (inDash) hasDashed = true;
            yield return null;
        }
        debug_gap = false;
        if (!hasDashed)
            currentDash = 0;
        else
            StartCoroutine(Dash_Cooldown());
    }

    private IEnumerator Dash_Cooldown()
    {
        debug_cooldown = true;
        float start_time = Time.time;
        float progress = (Time.time - start_time) / dashDuration;
        while(progress < 1.0f && inDash)
        {
            progress = (Time.time - start_time) / dashDuration;
            yield return null;
        }

        start_time = Time.time;
        progress = (Time.time - start_time) / dashCooldown;
        while (progress < 1.0f)
        {
            progress = (Time.time - start_time) / dashCooldown;
            yield return null;
        }

        debug_cooldown = false;
        currentDash = 0;
    }
}
