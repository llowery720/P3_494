using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicMovement : MonoBehaviour
{
    public float speedMultiplier;

    private PlayerDash dashController;
    private PlayerController controlsController;
    private Rigidbody2D rb;
    private Gamepad gamepad;
    private bool facingRight = true;
    private bool checkFace = true;

    private SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        dashController = GetComponent<PlayerDash>();
        controlsController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        gamepad = Gamepad.current;

        if (!controlsController.enableControls || dashController.inDash)
            return;

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
