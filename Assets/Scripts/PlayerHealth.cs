﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public float health;
	public int playerNumber;
    Color damageColor = Color.red;
    bool isFlashing = false;
    public float duration = 0.25f;
    SpriteRenderer renderer;
    Color originalColor;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer> ();
        originalColor = renderer.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = PlayerStatManager.playerStats[playerNumber - 1].Health * 10;
    }


    private void OnTriggerEnter(Collider other) {
     	if (other.tag == "Bullet"){
            StartCoroutine(Flash());
            health -= 10;
            if (health <= 0){

                this.gameObject.SetActive(false);
                Destroy(gameObject);
                //Add other game over stuff
            }
        }
    }

    IEnumerator Flash()
    {
        isFlashing = true;
        renderer.color = damageColor;
        yield return new WaitForSeconds(duration);
        renderer.color = originalColor;
        isFlashing = false;
    }
}
