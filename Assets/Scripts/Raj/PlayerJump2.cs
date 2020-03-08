using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump2 : MonoBehaviour
{
    public bool grounded;
    public float jumpForce;
    public float jumpMomentum;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public bool paused = false;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        jumpMomentum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!paused){
            grounded = isGrounded();
             //if (grounded) jumpMomentum = 0;
            if (Input.GetKeyDown(KeyCode.Z) && grounded){
                jump();
            }
            if (rb.velocity.y < 0){
                rb.velocity -= Vector3.up * fallMultiplier / 60;
            }
            else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Z)){
                rb.velocity -= Vector3.up * lowJumpMultiplier / 60;
            }
            if (rb.velocity.y > 0){
                checkCeiling();
            }
        }
    }

    private void jump(){
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        if (Mathf.Abs(rb.velocity.x) > .1){
            jumpMomentum = rb.velocity.x * .5f;
            StartCoroutine(runningJump());
        }
        
    }

    private bool isGrounded(){
        Collider col = this.gameObject.GetComponentInChildren<Collider>();
        // Ray from the center of the collider down.
        Ray ray = new Ray(col.bounds.center, Vector3.down);
        RaycastHit raycastHit;
        // A bit smaller than the actual radius so it doesn't catch on walls.
        float radius = col.bounds.extents.x - .05f;

        // A bit below the bottom
        float fullDistance = col.bounds.extents.y + .05f;
        if (Physics.SphereCast(ray, radius, out raycastHit, fullDistance))
            if (raycastHit.collider.gameObject.CompareTag("Wall")) return true;
        return false;
    }

    private void checkCeiling(){
        Collider col = this.gameObject.GetComponentInChildren<Collider>();
        // Ray from the center of the collider down.
        Ray ray = new Ray(col.bounds.center, Vector3.up);
        RaycastHit raycastHit;
        // A bit smaller than the actual radius so it doesn't catch on walls.
        float radius = col.bounds.extents.x - .05f;

        // A bit below the bottom
        float fullDistance = col.bounds.extents.y + .05f;
        if (Physics.SphereCast(ray, radius, out raycastHit, fullDistance))
            if (raycastHit.collider.gameObject.CompareTag("Wall")) {
                rb.velocity = rb.velocity - new Vector3(0, rb.velocity.y,0);
            }
    }

    private IEnumerator runningJump(){
        for (int i = 0; i < 5; ++i){
            yield return 0;
        }
        

        while(jumpMomentum != 0){
            Collider col = this.gameObject.GetComponentInChildren<Collider>();
            Vector3 origin = col.bounds.center;
            Vector3 direction = -this.gameObject.transform.up;
            Ray checkGround = new Ray(origin, direction);
            Ray checkForward = new Ray(origin, this.gameObject.transform.forward);
            RaycastHit hitForward;
            RaycastHit hitGround;
            float checkDist = 2f;
            col.enabled = false;

            if (Physics.Raycast(checkForward, out hitForward, .5f)){
                if (hitForward.collider.gameObject.CompareTag("Wall")){
                    jumpMomentum = 0;
                    col.enabled = true;
                    break;
                }
            }

            if (Physics.Raycast(checkGround, out hitGround, checkDist)){
                if (hitGround.collider.gameObject.CompareTag("Wall") && rb.velocity.y < 0){
                    jumpMomentum = 0;
                    col.enabled = true;
                    break;
                }
            }
            jumpMomentum = rb.velocity.x;
            col.enabled = true;
            yield return 0;
        }
    }
}
