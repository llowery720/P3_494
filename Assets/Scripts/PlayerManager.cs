using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    // public GameObject stats;
    public int numPlayers;

    // Update is called once per frame
    void Update()
    {
        numPlayers = transform.childCount;

        if(transform.childCount == 1) {
            ++PlayerStatManager.playerStats[transform.GetChild(0).GetComponent<PlayerAttack>().playerNum - 1].Wins;
            SceneManager.LoadScene("BetweenRounds");
        }
        if(transform.childCount == 0) {
            SceneManager.LoadScene("BetweenRounds");
        }
    }

    public int getNumPlayers()
    {
        return numPlayers;
    }
}
