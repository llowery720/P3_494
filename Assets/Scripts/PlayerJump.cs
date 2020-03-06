using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rigid;
    Collider col;
    PlayerState state;
    public float jumpPower = 6;
    public GameObject standing;
    public GameObject morphed;

    public float jumpTimer = .5f;
    private float jumpLength;
    private bool jumping = false;

    
    void Awake(){
        rigid = this.GetComponentInParent<Rigidbody>();
        col = standing.GetComponent<Collider>();
        state = this.GetComponent("PlayerState") as PlayerState;
    }
    void Update() {
        Vector3 newVelocity = rigid.velocity;
     // Vertical
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded() && !jumping && !state.MorphedCheck()){
        	jumping =  true;
            newVelocity.y = jumpPower;
            rigid.velocity = newVelocity;
        } 

        if(Input.GetKey(KeyCode.Z) && jumping){
        	if(jumpTimer > 0){
        		newVelocity.y = jumpPower;
                rigid.velocity = newVelocity;
                jumpTimer -= Time.deltaTime;
        	}
        	else{
        		jumping = false;
        		jumpTimer = .5f;
        	}
        	
        }  

        

        if (Input.GetKeyUp(KeyCode.Z) && jumping){
        	jumping = false;
        	newVelocity.y = -jumpPower * .75f;
            rigid.velocity = newVelocity;

        }

    }
    public bool isGrounded(){
        // Ray from the center of the collider down.
        Ray ray = new Ray(col.bounds.center, Vector3.down);

        // A bit smaller than the actual radius so it doesn't catch on walls.
        float radius = col.bounds.extents.x - .05f;

        // A bit below the bottom
        float fullDistance = col.bounds.extents.y + .05f;

        if (Physics.SphereCast(ray, radius, fullDistance))
            return true;
        else
            return false;
    }
}
