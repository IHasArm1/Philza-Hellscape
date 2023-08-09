using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class playerBehaviour : MonoBehaviour
{
    float enemyDamage;

    [SerializeField] private CharacterController2D charcontrol;
    [SerializeField] private StatsBar _statsbar;
    [SerializeField] private HealthCounter _healthcounter;
    public float m_DMGCooldown;
    public float m_NextDMGTime;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (GameManager.gameManager._playerHealth.Health <= 0)
        {
            SceneManager.LoadScene("level1");
        }

    }

    public void PlayerTakeDamage(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        _statsbar.SetHealth(GameManager.gameManager._playerHealth.Health, GameManager.gameManager._playerHealth.MaxHealth);
        _healthcounter.SetHealthCounter(GameManager.gameManager._playerHealth.Health);
    }

    public void PlayerHeal(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        _statsbar.SetHealth(GameManager.gameManager._playerHealth.Health, GameManager.gameManager._playerHealth.MaxHealth);
        _healthcounter.SetHealthCounter(GameManager.gameManager._playerHealth.Health);
    }

    public void TakeDJ(int dmg)
    {
        GameManager.gameManager._playerDJ.DmgUnit(dmg);
        _statsbar.SetDJ(GameManager.gameManager._playerDJ.DJ, GameManager.gameManager._playerDJ.MaxDJ);
    }

    public void DJHeal(int healing)
    {
        GameManager.gameManager._playerDJ.HealUnit(healing);
        _statsbar.SetDJ(GameManager.gameManager._playerDJ.DJ, GameManager.gameManager._playerDJ.MaxDJ);
    }

    public void TakeST(int dmg)
    {
        GameManager.gameManager._playerST.DmgST(dmg);
        _statsbar.SetST(GameManager.gameManager._playerST.ST, GameManager.gameManager._playerST.MaxST);
    }

    public void STHeal(int healing)
    {
        GameManager.gameManager._playerST.HealST(healing);
        _statsbar.SetST(GameManager.gameManager._playerST.ST, GameManager.gameManager._playerST.MaxST);
    }

    public void PlayerShoot()
    {

    }

    //EnemyMelee collision checker
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Time.time > m_NextDMGTime)
        {
            if (other.gameObject.tag == "EnemProj10")
            {
                PlayerTakeDamage(10);
                m_NextDMGTime = Time.time + m_DMGCooldown;
            }

            if (other.gameObject.tag == "Dmg20")
            {
                PlayerTakeDamage(10);
                m_NextDMGTime = Time.time + m_DMGCooldown;
            }
        }

    }

}
