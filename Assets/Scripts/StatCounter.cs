using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
// text mesh system
using UnityEngine.UI;

// manages values on the stats screen
public class StatCounter : MonoBehaviour
{
    public GameObject[] playerPanels;

    private List<Gamepad> gamePads = new List<Gamepad>(Gamepad.all);

    

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < playerPanels.Length; i++)
        {
            StartingStatText(playerPanels[i], i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ButtonUpdate()
    {

    }


    // update text with new values
    void StartingStatText(GameObject panel, int num)
    {
        for(int i = 0; i < panel.transform.childCount; i++)
        {
            GameObject child = panel.transform.GetChild(i).gameObject;

            Text childLabelField = child.GetComponent<Text>();
            string childLabel = child.GetComponent<Text>().text;

            if (childLabel.Contains("Health"))
            {
                childLabelField.text = "Health: " + PlayerStatManager.playerStats[num].Health;
            }
            else if (childLabel.Contains("Attack"))
            {
                childLabelField.text = "Attack: " + PlayerStatManager.playerStats[num].Attack;
            }
            else if (childLabel.Contains("Speed"))
            {
                childLabelField.text = "Speed: " + PlayerStatManager.playerStats[num].Speed;

            }
            else if (childLabel.Contains("Jump"))
            {
                childLabelField.text = "Jump: " + PlayerStatManager.playerStats[num].Jump;
            }
        }


    }

    void UpdateStatText(GameObject panel)
    {
        foreach(Transform child in transform)
        {
            
        }
    }
}
