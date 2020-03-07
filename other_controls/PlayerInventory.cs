using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public bool hasMorphBall = false;
    public bool hasLongBeam = false;
    public int missileCount = 0;
    public bool hasMissiles = false;
    public int maxMissiles = 20;
    public Text rocketText;
    public AudioClip pickupNoise;
    private AudioSource audio;
    private PlayerHealth playerHealth;

    private void Awake() {
        audio = this.gameObject.GetComponent<AudioSource>();
        playerHealth = this.gameObject.GetComponent<PlayerHealth>();
    }

    private void Update() {
        if (playerHealth.invincible && missileCount < maxMissiles){
            missileCount = maxMissiles;
            rocketText.text = ": " + missileCount.ToString();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.tag == "MorphBall"){
            Destroy(other.gameObject);
            hasMorphBall = true;
        }

        if (other.tag == "Long Beam"){
            Destroy(other.gameObject);
            hasLongBeam = true;
        }

        if (other.tag == "Missile Unlock"){
            Destroy(other.gameObject);
            hasMissiles = true;
        }

        if (other.tag == "Missile Pickup"){
            Destroy(other.gameObject);
            audio.PlayOneShot(pickupNoise);
            ++missileCount;
            missileCount = (missileCount < maxMissiles) ? missileCount : maxMissiles;
            rocketText.text = ": " + missileCount.ToString();
            
        }
        
    }

    public bool HasMorphBall(){
        return hasMorphBall;
    }
}
