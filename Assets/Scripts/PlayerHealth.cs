using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public float health;
    float kbStrength = 4f;
	public int playerNumber;
    Color damageColor = Color.red;
    bool isFlashing = false;
    public float duration = 0.25f;
    SpriteRenderer renderer;
    Color originalColor;
    PlayerRun playerRun;
    PlayerAttack playerAttack;

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer> ();
        playerRun = GetComponent<PlayerRun>();
        originalColor = renderer.color;
        playerAttack = GetComponent<PlayerAttack>();
    }

    // Start is called before the first frame update
    void Start()
    {
        health = PlayerStatManager.playerStats[playerNumber - 1].Health * 10;
    }


    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.transform.parent == transform) return;
     	if (other.tag == "Bullet"){
            
            StartCoroutine(Flash());
            GetComponent<Rigidbody>().velocity = (transform.position - other.transform.parent.position).normalized * kbStrength;
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
        playerRun.beingKnocked = true;
        playerAttack.beingKnocked = true;
        renderer.color = damageColor;
        yield return new WaitForSeconds(duration);
        renderer.color = originalColor;
        playerRun.beingKnocked = false;
        playerAttack.beingKnocked = false;
        isFlashing = false;
    }
}
