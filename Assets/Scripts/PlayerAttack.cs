using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	public GameObject projectile, weapon;
    SpriteRenderer sr;

	public Transform firePoint;
	public float speed = 3.0f;
    public int playerNum;
    public float reloadTime = 5f;
    public float attackRate, swingDuration;

    bool isReloading = false, isSwinging = false;

    private List<Gamepad> gamePads = new List<Gamepad>(Gamepad.all);

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
        if (isReloading)
            return;

        if ((playerNum == 1 && Input.GetKeyDown(KeyCode.Period)) || (playerNum == 2 && Input.GetKeyDown(KeyCode.B))) {
            //ShootForward();
            StartCoroutine(SwingForward());
        }
            

        //float shootDirectionX = gamePads[playerNum - 1].rightStick.x.ReadValue();
        //float shootDirectionY = gamePads[playerNum - 1].rightStick.y.ReadValue();

        //if (shootDirectionX != 0.0 || shootDirectionY != 0.0)
        //{
        //    Shoot(shootDirectionX, shootDirectionY);
        //}

    }

    void Shoot(float x_dir, float y_dir)
    {
        StartCoroutine(Reload());

        Vector3 offset = Vector3.Normalize(new Vector3(x_dir, y_dir, 0));

        GameObject shot = Instantiate(projectile, firePoint.position + offset * 0.35f, firePoint.rotation);
        Rigidbody rb = shot.GetComponent<Rigidbody>();
        rb.velocity = offset * speed;
    }

    void ShootForward(){
        StartCoroutine(Reload());

        if (!sr.flipX)
        {
            GameObject shot = Instantiate(projectile, firePoint.position + Vector3.right * 0.25f, firePoint.rotation);
            Rigidbody rb = shot.GetComponent<Rigidbody>();
            rb.velocity = firePoint.right * speed;
        }
        else
        {
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

    IEnumerator SwingForward() {
        StartCoroutine(Reload());

        isSwinging = true;
        GameObject swing;
        if(!sr.flipX) {
            swing = Instantiate(weapon, firePoint.position + Vector3.right * 0.25f, firePoint.rotation);
        }
        else {
            swing = Instantiate(weapon, firePoint.position + Vector3.left * 0.25f, firePoint.rotation);
        }
        swing.transform.parent = transform;
        yield return new WaitForSeconds(swingDuration);
        Destroy(swing);

        isSwinging = false;
    }
}
