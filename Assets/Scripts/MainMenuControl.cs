using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    Gamepad playerPad;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPad = Gamepad.current;

        if (playerPad != null)
        {
            if (playerPad.aButton.wasPressedThisFrame)
            {
                SceneManager.LoadScene("BetweenRounds");
            }
            else if (playerPad.bButton.wasPressedThisFrame)
            {
                Application.Quit(0);
            }
        }
    }
}
