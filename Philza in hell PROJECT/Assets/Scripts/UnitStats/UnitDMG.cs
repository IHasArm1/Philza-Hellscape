using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDMG
{

    //Damage
    int _currentDMG;
    int _currentMaxDMG;

    // properties


    public int DMG
    {
        get
        {
            return _currentDMG;
        }
        set
        {
            _currentDMG = value;
        }
    }
    public int MaxDMG
    {
        get
        {
            return _currentMaxDMG;
        }
        set
        {
            _currentMaxDMG = value;
        }
    }

    // Constructor
    public UnitDMG(int DMG, int maxDMG)
    {
        _currentDMG = DMG;
        _currentMaxDMG = maxDMG;
    }

    // Methods
    public void DmgDMG(int dmgAmount)
    {
        if (_currentDMG > 0)
        {
            _currentDMG -= dmgAmount;
        }
    }
    public void HealDMG(int healAmount)
    {
        if (_currentDMG < _currentMaxDMG)
        {
            _currentDMG += healAmount;
        }
        if (_currentDMG > _currentMaxDMG)
        {
            _currentDMG = _currentMaxDMG;
        }
    }

}
