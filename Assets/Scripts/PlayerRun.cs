using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    Rigidbody rigid;
    PlayerJump2 pj;
    PlayerState state;

    public bool paused = false;
    public bool transitioning = false;
    
    public bool SpeedGainAccess = false;
    public bool SpeedGainToggle = false;
    public float acceleration = 2.0f;
    public float MaxSpeed = 12f;

    public float moveSpeed = 5f;

    private float interpolation;

    void Awake(){
        rigid = this.GetComponent<Rigidbody>();
        pj = this.GetComponent<PlayerJump2>();
        state = this.GetComponent("PlayerState") as PlayerState;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.C) && SpeedGainAccess) SpeedGainToggle = ! SpeedGainToggle;
        if(!paused){
            Vector3 newVelocity = rigid.velocity;
            if(!SpeedGainAccess || !SpeedGainToggle){
                if(Input.GetAxis("Horizontal") != 0){
                    newVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
                }
                else{
                    newVelocity.x = pj.jumpMomentum;
                }
                rigid.velocity = newVelocity;
            }
            else{
                if(Input.GetAxis("Horizontal") > 0){
                    interpolation = acceleration * Time.deltaTime;
                    if(newVelocity.x < MaxSpeed){
                        newVelocity.x = Mathf.Lerp(newVelocity.x, MaxSpeed, interpolation);
                    }
                    else{
                        newVelocity.x = MaxSpeed;
                    }
                }
                else if(Input.GetAxis("Horizontal") < 0){
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
