using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStatManager
{
    public static PlayerStatistics[] playerStats = new PlayerStatistics[4];
}

public class PlayerStatistics
{
    public int totalHealth = 10;
    public int attackPower = 10;
    public int jumpPower = 10;
    public int speed = 10;
}
