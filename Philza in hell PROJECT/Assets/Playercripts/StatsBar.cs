using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBar : MonoBehaviour
{
    Slider _healthSlider;
    [SerializeField] private Image HealthBar;
    [SerializeField] private Image StaminaBar;
    [SerializeField] private Image JumpCountBar;
    // for converting int to float
    float healthf;
    float maxhealthf;
    float staminaf;
    float maxstaminaf;
    float jumpcountf;
    float maxjumpcountf;

    void Start()
    {
        _healthSlider = GetComponent<Slider>();
    }

    public void SetMaxHealth(int maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
    }

    public void SetHealth(int health, int maxHealth)
    {
        healthf = health;
        maxhealthf = maxHealth;
        HealthBar.fillAmount = (healthf / maxhealthf);
    }

    public void SetDJ(int DJ, int maxDJ)
    {
        jumpcountf = (float)DJ;
        maxjumpcountf = (float)maxDJ;
        JumpCountBar.fillAmount = (jumpcountf / maxjumpcountf);
    }

    public void SetST(int ST, int maxST)
    {
        staminaf = (float)ST;
        maxstaminaf = (float)maxST;
        StaminaBar.fillAmount = (staminaf / maxstaminaf);
    }

}
