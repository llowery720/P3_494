using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
// text mesh system
using TMPro;

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
            StartingStatText(playerPanels[i]);
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
    void StartingStatText(GameObject panel)
    {

    }

    void UpdateStatText(GameObject panel)
    {
        foreach(Transform child in transform)
        {
            
        }
    }
}
