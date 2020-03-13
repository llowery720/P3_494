using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.InputSystem;
// text mesh system
using UnityEngine.UI;

// manages values on the stats screen
public class StatCounter : MonoBehaviour
{
    public GameObject[] playerPanels;
    public GameObject winText, directions;
    int playerCount = 4;
    public int maxWins = 4;
    bool gameOver = false;

    private float[] buttonDelay = new float[4];

    private List<Gamepad> gamePads = new List<Gamepad>(Gamepad.all);

    int currentPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < playerPanels.Length; i++)
        {
            UpdateStatText(playerPanels[i], i);
            buttonDelay[i] = .5f;

            // don't show inactive game panels
            if (i >= gamePads.Count && i > 1)
            {
                playerPanels[i].SetActive(false);
                playerCount--;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.Space)) {
            Reset();
        }

        for(int i = 0; i < playerPanels.Length; i++){
            if(PlayerStatManager.playerStats[i].Wins == maxWins) {
                EndGame(i + 1);
                return;
            }
        }

        if(currentPlayer > playerCount - 1) {
            for (int i = 0; i < playerPanels.Length; i++) PlayerStatManager.playerStats[i].roundBonus = 3;
            SceneManager.LoadScene("Raj");
        }

        if(gamePads.Count == 0) {
            KeyActionUpdate(currentPlayer);
            UpdateStatText(playerPanels[currentPlayer], currentPlayer);
            if(PlayerStatManager.playerStats[currentPlayer].roundBonus == 0) ++currentPlayer;
            return;
        }

        for (int i = 0; i < playerPanels.Length; i++)
        {
            ButtonActionUpdate(i);
            UpdateStatText(playerPanels[i], i);

            buttonDelay[i] += Time.deltaTime;
        }

        if (gamePads.Count > 0 && gamePads[0].startButton.isPressed)
        {
            for (int i = 0; i < playerPanels.Length; i++) PlayerStatManager.playerStats[i].roundBonus = 3;
            SceneManager.LoadScene("Raj");
        }
    }

    // Takes in an integer value corresponding to a controller and queries the gamepad for button presses
    // If player has round points left, increase the value of the chosen field and remove a bonus point
    void ButtonActionUpdate(int num)
    {
        Gamepad current = null;

        if (num < gamePads.Count)
        {
            current = gamePads[num];
        }


        if (current != null && PlayerStatManager.playerStats[num].roundBonus > 0 && buttonDelay[num] > .3f)
        {
            if (current.aButton.isPressed) // Increase Health
            {
                PlayerStatManager.playerStats[num].Health++;
                SubtractRoundPoints(num);
            }
            else if (current.bButton.isPressed) // Increase 
            {
                PlayerStatManager.playerStats[num].Speed++;
                SubtractRoundPoints(num);
            }
            else if (current.xButton.isPressed)
            {
                PlayerStatManager.playerStats[num].Jump++;
                SubtractRoundPoints(num);
            }
            else if (current.yButton.isPressed)
            {
                PlayerStatManager.playerStats[num].Attack++;
                SubtractRoundPoints(num);
            }
        }
    }

    void KeyActionUpdate(int num){
        if (PlayerStatManager.playerStats[num].roundBonus > 0)
        {
            if (Input.GetKeyDown(KeyCode.A)) // Increase Health
            {
                PlayerStatManager.playerStats[num].Health++;
                SubtractRoundPoints(num);
            }
            else if (Input.GetKeyDown(KeyCode.B)) // Increase 
            {
                PlayerStatManager.playerStats[num].Speed++;
                SubtractRoundPoints(num);
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                PlayerStatManager.playerStats[num].Jump++;
                SubtractRoundPoints(num);
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                PlayerStatManager.playerStats[num].Attack++;
                SubtractRoundPoints(num);
            }
        }
    }

    void SubtractRoundPoints(int playerNum)
    {
        PlayerStatManager.playerStats[playerNum].roundBonus--;
        buttonDelay[playerNum] = 0f;
    }

    // Takes in a canvas object and a number corresponding to the 0-indexed entry for the player
    // Updates on-screen elements based on new values from PlayerStatManager
    void UpdateStatText(GameObject panel, int num)
    {
        for (int i = 0; i < panel.transform.childCount; i++)
        {
            GameObject child = panel.transform.GetChild(i).gameObject;

            Text childLabelField = child.GetComponent<Text>();

            if (child.name == "PlayerNum")
            {
                childLabelField.text = "P" + (num + 1).ToString() + ": " + PlayerStatManager.playerStats[num].Wins + "W";
            }
            else if (child.name == "Health")
            {
                childLabelField.text = "Health: " + PlayerStatManager.playerStats[num].Health;
            }
            else if (child.name == "Attack")
            {
                childLabelField.text = "Attack: " + PlayerStatManager.playerStats[num].Attack;
            }
            else if (child.name == "Speed")
            {
                childLabelField.text = "Speed: " + PlayerStatManager.playerStats[num].Speed;

            }
            else if (child.name == "Jump")
            {
                childLabelField.text = "Jump: " + PlayerStatManager.playerStats[num].Jump;
            }
            else if (child.name == "BoostNum")
            {
                childLabelField.text = PlayerStatManager.playerStats[num].roundBonus.ToString();
            }
        }
    }

    void EndGame(int playerNum){
        gameOver = true;
        for(int i = 0; i < playerPanels.Length; i++){
            playerPanels[i].SetActive(false);
        }
        directions.SetActive(false);
        winText.SetActive(true);
        winText.GetComponent<Text>().text = "Player " + playerNum + " Wins!\n\'Space\' to restart";
    }

    void Reset() {
        for(int i = 0; i < playerCount; i++){
            playerPanels[i].SetActive(true);
        }
        directions.SetActive(true);
        winText.SetActive(false);
        gameOver = false;

        for(int i = 0; i < 4; i++) {
                PlayerStatManager.playerStats[i] = new PlayerStatManager.PlayerStatistics(3, 5, 3f, 4f, 3);
        }
    }
}
