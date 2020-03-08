using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform bar;
    public PlayerController controller;
    public float percentage;

    // Update is called once per frame
    private void Update()
    {
        percentage = (float)controller.health / (float)controller.maxHealth;
        bar.localScale = new Vector3(percentage, 1.0f, 0.0f);

        if(percentage > 0.3f)
        {
            bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
