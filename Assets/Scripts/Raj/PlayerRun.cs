using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    Rigidbody rigid;
    PlayerJump2 pj;
    SpriteRenderer sr;

    public bool paused = false;
    public bool transitioning = false;
    
    public bool SpeedGainAccess = false;
    public bool SpeedGainToggle = false;
    public float acceleration = 2.0f;
    public float MaxSpeed = 12f;

    public float moveSpeed = 5f;

    // is this Player 1, 2, 3?
    public int playerNum;

    private float interpolation;


    private List<Gamepad> gamePads = new List<Gamepad>(Gamepad.all);


    void Awake(){
        rigid = this.GetComponent<Rigidbody>();
        pj = this.GetComponent<PlayerJump2>();
        sr = this.GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.C) && SpeedGainAccess) SpeedGainToggle = ! SpeedGainToggle;
        if(!paused){
            Vector3 newVelocity = rigid.velocity;

            float horiz_move = 0f;
            // float vert_move;

            if (gamePads.Count == 0)
            {
                if(playerNum == 1) {
                    horiz_move = Input.GetAxis("Horizontal1");
                }
                else if(playerNum == 2) {
                    horiz_move = Input.GetAxis("Horizontal2");
                }
                
            }
            else
            {
                horiz_move = gamePads[playerNum - 1].leftStick.x.ReadValue();
            }

            if (!SpeedGainAccess || !SpeedGainToggle){

                if(horiz_move != 0){
                    if (playerNum == 1)
                    {
                        newVelocity.x = Input.GetAxis("Horizontal1") * moveSpeed;
                    }
                    else if (playerNum == 2)
                    {
                        newVelocity.x = Input.GetAxis("Horizontal2") * moveSpeed;
                    }
                    
                }
                else{
                    newVelocity.x = pj.jumpMomentum;
                }

                if (horiz_move > 0)
                {
                    sr.flipX = false;
                }
                else if (horiz_move < 0)
                {
                    sr.flipX = true;
                }

                rigid.velocity = newVelocity;
            }
            else{
                if(horiz_move > 0){
                    sr.flipX = false;
                    interpolation = acceleration * Time.deltaTime;
                    if(newVelocity.x < MaxSpeed){
                        newVelocity.x = Mathf.Lerp(newVelocity.x, MaxSpeed, interpolation);
                    }
                    else{
                        newVelocity.x = MaxSpeed;
                    }
                }
                else if(horiz_move < 0){
                    sr.flipX = true;
                    interpolation = acceleration * Time.deltaTime;
                    if(newVelocity.x > -MaxSpeed){
                        newVelocity.x = Mathf.Lerp(newVelocity.x, -MaxSpeed, interpolation);
                    }
                    else{
                        newVelocity.x = -MaxSpeed;
                    }
                }
                else{
                    newVelocity.x = Mathf.Lerp(newVelocity.x, 0, (interpolation * 2));
                }
                rigid.velocity = newVelocity;
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Speed Unlock"){
            Destroy(other.gameObject);
            SpeedGainAccess = true;
        }
        if (other.tag == "Boost Pad" && SpeedGainToggle){
            Vector3 newVelocity = rigid.velocity;
            float direction = (newVelocity.x > 0) ? 1f : -1f;
            newVelocity.x = MaxSpeed * direction;
            rigid.velocity = newVelocity;
        }
    }    
}
