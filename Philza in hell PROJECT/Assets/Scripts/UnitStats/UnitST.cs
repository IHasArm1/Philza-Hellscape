using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitST
{

    //Stamina
    int _currentST;
    int _currentMaxST;

    // properties


    public int ST
    {
        get
        {
            return _currentST;
        }
        set
        {
            _currentST = value;
        }
    }
    public int MaxST
    {
        get
        {
            return _currentMaxST;
        }
        set
        {
            _currentMaxST = value;
        }
    }

    // Constructor
    public UnitST(int ST, int maxST)
    {
        _currentST = ST;
        _currentMaxST = maxST;
    }

    // Methods
    public void DmgST(int dmgAmount)
    {
        if (_currentST > 0)
        {
            _currentST -= dmgAmount;
        }
    }
    public void HealST(int healAmount)
    {
        if (_currentST < _currentMaxST)
        {
            _currentST += healAmount;
        }
        if (_currentST > _currentMaxST)
        {
            _currentST = _currentMaxST;
        }
    }

}
