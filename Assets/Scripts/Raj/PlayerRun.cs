using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerRun : MonoBehaviour
{
    Rigidbody rigid;
    PlayerJump2 pj;

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
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.C) && SpeedGainAccess) SpeedGainToggle = ! SpeedGainToggle;
        if(!paused){
            Vector3 newVelocity = rigid.velocity;

            float horiz_move;
            // float vert_move;

            if (gamePads.Count == 0)
            {
                horiz_move = Input.GetAxis("Horizontal");
            }
            else
            {
                horiz_move = gamePads[playerNum - 1].leftStick.x.ReadValue();
            }

            if (!SpeedGainAccess || !SpeedGainToggle){

                if(horiz_move != 0){
                    newVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
                }
                else{
                    newVelocity.x = pj.jumpMomentum;
                }
                rigid.velocity = newVelocity;
            }
            else{
                if(horiz_move > 0){
                    interpolation = acceleration * Time.deltaTime;
                    if(newVelocity.x < MaxSpeed){
                        newVelocity.x = Mathf.Lerp(newVelocity.x, MaxSpeed, interpolation);
                    }
                    else{
                        newVelocity.x = MaxSpeed;
                    }
                }
                else if(horiz_move < 0){
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
