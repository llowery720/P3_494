using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public bool enableControls = true;
    public bool jumping = false;
    public bool landing = false;
    public bool dashing = false;
    public bool onGround = false;
    public string promptText = "";

    public int currentLevel;

    private bool takingDamage = false;
    private PlayerDash dash;
    private PlayerJump jump;

    private void Start()
    {
        dash = GetComponent<PlayerDash>();
        jump = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        dashing = dash.inDash;
        jumping = jump.jumping;
        onGround = jump.grounded;
    }

    public void adjustHealth(int value)
    {
        if (health + value > maxHealth)
            health = maxHealth;
        else
            health += value;

        if (health <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (true)
        {
            //!collision.gameObject.CompareTag("Jumpable")
            // Debug.Log(collision.gameObject.tag);
            // Debug.Log(collision.gameObject.name);

        }
        if (collision.gameObject.CompareTag("InstaDeath"))
        {
            Death();
        }
        else if(collision.gameObject.CompareTag("Flag"))
        {
            string scene;
            if (currentLevel == 4)
                scene = "Credits";
            else
                scene = "Stage" + (currentLevel + 1).ToString();

            SceneManager.LoadScene(scene);
        }
        else if(collision.gameObject.CompareTag("Exit"))
        {
            Application.Quit();
        }
    }

    private IEnumerator DamageTimer(float time)
    {
        float start_time = Time.time;
        float progress = (Time.time - start_time) / time;
        while (progress < 1.0f)
        {
            progress = (Time.time - start_time) / time;
            yield return null;
        }
        takingDamage = false;
    }

    private void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
