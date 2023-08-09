using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStatsTESTING : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HealthCount;
    [SerializeField] private TextMeshProUGUI LevelCount;

    public int level = 1;
    public int health = 1;


    private void Update()
    {
        HealthCount.text = health.ToString();
        LevelCount.text = level.ToString();
    }

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        playerCombat data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;
        Vector3 GunStats;
        GunStats.x = data.GunStats[0];
        GunStats.y = data.GunStats[1];
        GunStats.z = data.GunStats[2];
        transform.position = GunStats;
    }

    public void AddHealthTest()
    {
        health++;
        
    }
    public void TakeHealthTest()
    {
        health--;
    }
    public void AddLevelTest()
    {
        level++;
    }
    public void TakeLevelTest()
    {
        level--;
    }

}
