﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float speedMultiplier;
    private Rigidbody2D rb;
    private Gamepad gamepad;
    private SpriteRenderer playerSprite;

    public int playerNumber;

    private bool facingRight = true;
    private bool checkFace = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        gamepad = Gamepad.current;
        Debug.Log(gamepad.name);

        speedMultiplier = PlayerStatManager.playerStats[playerNumber].Speed;

        float horizontal_velocity = gamepad.leftStick.x.ReadValue();
        Vector2 current_velocity = rb.velocity;
        current_velocity.x = horizontal_velocity * speedMultiplier;
        rb.velocity = current_velocity;

        if (horizontal_velocity < 0)
            facingRight = false;
        else if (horizontal_velocity > 0)
            facingRight = true;

        if (checkFace != facingRight)
            Flip();
        checkFace = facingRight;
    }

    private void Flip()
    {
        playerSprite.flipX = !playerSprite.flipX;
    }
}
