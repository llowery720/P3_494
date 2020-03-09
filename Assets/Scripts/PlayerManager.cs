using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject stats;

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount == 1) {
            ++PlayerStatManager.playerStats[transform.GetChild(0).GetComponent<PlayerAttack>().playerNum - 1].Wins;
            SceneManager.LoadScene("BetweenRounds");
        }
        if(transform.childCount == 0) {
            SceneManager.LoadScene("BetweenRounds");
        }
    }
}
