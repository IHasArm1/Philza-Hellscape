using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(100, 100);
    public UnitDJ _playerDJ = new UnitDJ(2, 2);
    public UnitST _playerST = new UnitST(2, 2);
    public UnitDMG _playerDMG = new UnitDMG(5, 200);
    public UnitHealth _zombieHealth = new UnitHealth(50, 50);

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

    
}
