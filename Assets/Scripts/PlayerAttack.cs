using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public GameObject projectile;
    SpriteRenderer sr;
	public Transform firePoint;
	public float speed = 3.0f;
    public int playerNum;
    public float reloadTime = 5f;
    public float attackRate;
    bool isReloading = false;
    // Start is called before the first frame update
    void Start()
    {
        firePoint = this.gameObject.transform.GetChild(0);
        sr = GetComponent<SpriteRenderer>();
        attackRate = PlayerStatManager.playerStats[playerNum - 1].Attack;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReloading) return;

        if(playerNum == 1) {
            if (Input.GetKeyDown(KeyCode.Period)){
                Shoot();
            }
        }
        else if(playerNum == 2) {
            if (Input.GetKeyDown(KeyCode.B)){
                Shoot();
    	    }
        }
    	
    }

    void Shoot(){
        StartCoroutine(Reload());
        if(!sr.flipX) {
            GameObject shot = Instantiate(projectile, firePoint.position + Vector3.right * 0.25f, firePoint.rotation);
            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.velocity = firePoint.right * speed;
        }
        else {
            GameObject shot = Instantiate(projectile, firePoint.position + Vector3.left * 0.25f, firePoint.rotation);
            shot.GetComponent<SpriteRenderer>().flipX = true;
            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.velocity = firePoint.right * -speed;
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime / attackRate);
        isReloading = false;
    }
}
