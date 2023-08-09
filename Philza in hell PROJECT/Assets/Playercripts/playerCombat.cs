using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class playerCombat
{
    public int level;
    public int health;
    public float[] GunStats;

    public playerCombat (PlayerStatsTESTING player)
    {
        level = player.level;
        health = player.health;

        GunStats = new float[3];
        GunStats[0] = player.transform.position.x;
        GunStats[1] = player.transform.position.y;
        GunStats[2] = player.transform.position.z;
    }


}
