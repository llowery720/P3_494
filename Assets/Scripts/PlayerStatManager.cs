using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds Static Variables corresponding to stat changes
public class PlayerStatManager: MonoBehaviour
{
    public static PlayerStatManager _instance;


    public static PlayerStatistics[] playerStats;




    void Awake()
    {
        // singleton enforcement
        if (FindObjectsOfType(typeof(PlayerStatManager)).Length > 1)
        {
            Debug.Log("PSM: Already found instance of script in scene; destroying.");
            DestroyImmediate(gameObject);
        }

        playerStats = new PlayerStatistics[4];

        for(int i = 0; i < 4; i++)
        {
            playerStats[i] = new PlayerStatistics(10, 10, 4.0f, 3.0f, 5);
        }

        DontDestroyOnLoad(gameObject);
    }

    public class PlayerStatistics
    {
        public int Health;
        public int Attack;
        public float Jump;
        public float Speed;

        public int roundBonus;

        public PlayerStatistics(int h, int a, float j, float s, int rb)
        {
            Health = h;
            Attack = a;
            Jump = j;
            Speed = s;

            roundBonus = rb;
        }
    }
}


