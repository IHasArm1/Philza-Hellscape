using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDJ
{
    //Double jump
    int _currentDJ;
    int _currentMaxDJ;

    // properties

    public int DJ
    {
        get
        {
            return _currentDJ;
        }
        set
        {
            _currentDJ = value;
        }
    }
    public int MaxDJ
    {
        get
        {
            return _currentMaxDJ;
        }
        set
        {
            _currentMaxDJ = value;
        }
    }

    // Constructor
    public UnitDJ(int DJ, int maxDJ)
    {
        _currentDJ = DJ;
        _currentMaxDJ = maxDJ;
    }

    // Methods
    public void DmgUnit(int dmgAmount)
    {
        if (_currentDJ > 0)
        {
            _currentDJ -= dmgAmount;
        }
    }
    public void HealUnit(int healAmount)
    {
        if (_currentDJ < _currentMaxDJ)
        {
            _currentDJ += healAmount;
        }
        if (_currentDJ > _currentMaxDJ)
        {
            _currentDJ = _currentMaxDJ;
        }
    }

}

