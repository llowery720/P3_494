using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public bool invincible;
    public bool lavaInvincible;
    public float health = 30;
    public float knockbackForce;
    public Text healthUi;
    private AudioSource audio;
    public AudioClip pickupNoise;
    private void Awake() {
        audio = this.gameObject.GetComponent<AudioSource>();
        invincible = false;
        healthUi.text = "EN: " + health.ToString();
        
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            invincible = !invincible;
            lavaInvincible = invincible;
        }
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Enemy") && !invincible){
            health -= 10;
            healthUi.text = "EN: " + health.ToString();
            Vector3 knockbackDirection = this.gameObject.GetComponent<Rigidbody>().transform.position
                - other.transform.position;
            this.gameObject.GetComponent<Rigidbody>().AddForce(knockbackDirection * -knockbackForce, ForceMode.Impulse);
            StartCoroutine(iFrames());
            if (health <= 0){
                this.gameObject.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        else if (other.tag == "Health Pickup"){
            audio.PlayOneShot(pickupNoise);
            health += 3;
            healthUi.text = "EN: " + health.ToString();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Lava" && !lavaInvincible){
            health--;
            healthUi.text = "EN: " + health.ToString();
            if (health <= 0){
                this.gameObject.SetActive(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else StartCoroutine(shortLavaInvincible(5));
        }
    }

    IEnumerator shortLavaInvincible(int frames){
        lavaInvincible = true;
        for (int i = 0; i < frames; ++ i){
            yield return 0;
        }
        lavaInvincible = false;
    }
    IEnumerator iFrames(){
        invincible = true;
        //Animator anim = this.gameObject.GetComponentInChildren<Animator>();
        //anim.SetTrigger("TookDamage");
        bool flip = false;
        for (int i = 0; i < 60; ++i){
            if (i % 5 == 0) flip = !flip;
            this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = flip;
            yield return 0;
            //this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

        }
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        //yield return new WaitForSeconds(1);
        //anim.ResetTrigger("TookDamage");
        invincible = false;
        yield break;
    }
}
