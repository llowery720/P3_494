using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator playerAnim;
    private PlayerController controller;
    private Rigidbody rb;

    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    { /*
        if (controller.onGround && (rb.velocity.x >= 0.1 || rb.velocity.x <= -0.1))
        {
            playerAnim.SetBool("isRunning", true);
        }
        else if (controller.jumping)
        {
            playerAnim.SetBool("isJumping", true);
        }
        else if (controller.landing)
        {
            playerAnim.SetBool("isLanding", true);
        }
        else if (rb.velocity.y > 0)
        {
            playerAnim.SetBool("isFalling", false);
            playerAnim.SetBool("isRising", true);
        }
        else if (!controller.onGround)
        {
            playerAnim.SetBool("isFalling", true);
            playerAnim.SetBool("isRising", false);
        }
        else
        {
            playerAnim.SetBool("isFalling", false);
            playerAnim.SetBool("isRising", false);
            playerAnim.SetBool("isRunning", false);
            playerAnim.SetBool("isJumping", false);
            playerAnim.SetBool("isLanding", false);
        }
        */

        //status values:
        // 0: idle
        // 1: running
        // 2: jumping
        // 3: rising
        // 4: falling
        // 5: landing
        // 6: dashing

        if(controller.onGround && rb.velocity.x == 0)
        {
            playerAnim.SetInteger("status", 0);
        }

        if(controller.onGround && rb.velocity.x != 0)
        {
            playerAnim.SetInteger("status", 1);
        }

        if(controller.jumping)
        {
            playerAnim.SetInteger("status", 2);
        }

        if(playerAnim.GetInteger("status") == 2 && !controller.jumping)
        {
            playerAnim.SetInteger("status", 3);
        }

        if(!controller.onGround && rb.velocity.y < 0)
        {
            playerAnim.SetInteger("status", 4);
        }

        if (controller.landing)
        {
            playerAnim.SetInteger("status", 5);
        }

        if (controller.dashing)
        {
            playerAnim.SetInteger("status", 6);
        }
    }
}
