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

    int playerCount = 2;
    public int maxWins = 4;

    bool gameOver = false;

    private float[] buttonDelay = new float[4];

    private List<Gamepad> gamePads = new List<Gamepad>(Gamepad.all);

    int currentPlayer = 0;


    private string[] wordBank = new string[3] {"Attack", "Speed", "Jump" };
    private int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        // currentPlayer = 0;

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
        if(gameOver && Input.GetKeyDown(KeyCode.Return)) {
            Reset();
        }

        for(int i = 0; i < playerPanels.Length; i++){
            if(PlayerStatManager.playerStats[i].Wins == maxWins) {
                EndGame(i + 1);
                return;
            }
        }


        if(currentPlayer == 0)
        {
            for (int i = 0; i < playerPanels[1].transform.childCount; i++)
            {
                GameObject child = playerPanels[1].transform.GetChild(i).gameObject;

                Text childLabelField = child.GetComponent<Text>();

                childLabelField.color = Color.gray;
            }
            for (int i = 0; i < playerPanels[0].transform.childCount; i++)
            {
                GameObject child = playerPanels[0].transform.GetChild(i).gameObject;

                Text childLabelField = child.GetComponent<Text>();

                childLabelField.color = Color.white;
            }
        }
        else if(currentPlayer == 1)
        {
            for (int i = 0; i < playerPanels[0].transform.childCount; i++)
            {
                GameObject child = playerPanels[0].transform.GetChild(i).gameObject;

                Text childLabelField = child.GetComponent<Text>();

                childLabelField.color = Color.gray;
            }
            for (int i = 0; i < playerPanels[1].transform.childCount; i++)
            {
                GameObject child = playerPanels[1].transform.GetChild(i).gameObject;

                Text childLabelField = child.GetComponent<Text>();

                childLabelField.color = Color.white;
            }
        }

        if(currentPlayer > playerCount - 1) {
            for (int i = 0; i < playerPanels.Length; i++) PlayerStatManager.playerStats[i].roundBonus = 3;
            SceneManager.LoadScene("Raj");
        }

        if(gamePads.Count == 0 && currentPlayer <= playerCount - 1) {
            KeyActionUpdate(currentPlayer);
            // Debug.Log(currentPlayer);
            UpdateStatText(playerPanels[currentPlayer], currentPlayer);

            if(PlayerStatManager.playerStats[currentPlayer].roundBonus == 0) ++currentPlayer;
            return;
        }


    }

    // Takes in an integer value corresponding to a controller and queries the gamepad for button presses
    // If player has round points left, increase the value of the chosen field and remove a bonus point
    void KeyActionUpdate(int num){
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentIndex--;
            if(currentIndex < 0)
            {
                currentIndex = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentIndex++;
            if (currentIndex > 2)
            {
                currentIndex = 0;
            }
        }

        directions.GetComponent<Text>().text = "<     Add 1 Point to " + wordBank[currentIndex] + "     >\n\n Press Space to Confirm";


        if (PlayerStatManager.playerStats[num].roundBonus > 0 && (Input.GetKeyDown(KeyCode.Space)))
        {
            //if (directions.GetComponent<Text>().text.Contains("Health")) // Increase Health
            //{
            //    PlayerStatManager.playerStats[num].Health++;
            //    SubtractRoundPoints(num);
            //}
            if (directions.GetComponent<Text>().text.Contains("Speed")) // Increase 
            {
                PlayerStatManager.playerStats[num].Speed++;
                SubtractRoundPoints(num);
            }
            else if (directions.GetComponent<Text>().text.Contains("Jump"))
            {
                PlayerStatManager.playerStats[num].Jump++;
                SubtractRoundPoints(num);
            }
            else if (directions.GetComponent<Text>().text.Contains("Attack"))
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
            //else if (child.name == "Health")
            //{
            //    childLabelField.text = "Health: " + PlayerStatManager.playerStats[num].Health;
            //}
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

            if (directions.GetComponent<Text>().text.Contains(child.name)){
                childLabelField.color = new Color(255, 215, 0);
            }
            else
            {
                childLabelField.color = Color.white;
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
        winText.GetComponent<Text>().text = "Player " + playerNum + " Wins!\n\'Enter\' to restart";
    }

    void Reset() {
        for(int i = 0; i < playerCount; i++){
            playerPanels[i].SetActive(true);
        }
        directions.SetActive(true);
        winText.SetActive(false);
        gameOver = false;

        for(int i = 0; i < 4; i++) {
            // PlayerStatManager.playerStats[i] = new PlayerStatManager.PlayerStatistics(5, 5, 3f, 4f, 3);
            PlayerStatManager.playerStats[i].Reset();
        }

        for (int i = 0; i < playerPanels.Length; i++)
        {
            UpdateStatText(playerPanels[i], i);
        }
    }
}
