using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int attackDamage;
    public float attackRate;
    public float bulletSpeed;
    public Vector2 knockback = new(0, 0);
    public Transform firePoint;
    public Transform firePointTop;
    public Transform firePointTopMid;
    public Transform firePointBottom;
    public Transform firePointBottomMid;
    public GameObject bullet;
    public CharacterController2D charcon;
    public bool facingRight;
    private float nextAttackTime;

    private Quaternion Rot1;
    private Quaternion Rot2;
    private Quaternion Rot3;
    private Quaternion Rot4;

    private void Start()
    {
        Rot1 = Quaternion.Euler(0, 0, -20);
        Rot2 = Quaternion.Euler(0, 0, -10);
        Rot3 = Quaternion.Euler(0, 0, 20);
        Rot4 = Quaternion.Euler(0, 0, 10);
    }
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetAxisRaw("Fire1") > 0)
            {
                if (charcon.m_FacingRight)
                {
                    Shoot();
                } else
                {
                    ShootLEFT();
                }
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }
        if (charcon.m_FacingRight)
        {
            facingRight = true;
        } else
        {
            facingRight = false;
        }
    }

    void Shoot()
    {
        // shooting logic
        GameObject BulletClone1 = (GameObject)Instantiate(bullet, firePoint.position, Quaternion.identity);
        BulletClone1.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone1.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone1.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone1.GetComponent<BulletShotgun>()._facingRight = facingRight;
        //shoot top bullet
        GameObject BulletClone2 = (GameObject)Instantiate(bullet, firePointTop.position, firePointTop.rotation);
        BulletClone2.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone2.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone2.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone2.GetComponent<BulletShotgun>()._facingRight = facingRight;
        //shoot bottom bullet
        GameObject BulletClone3 = (GameObject)Instantiate(bullet, firePointBottom.position, firePointTopMid.rotation);
        BulletClone3.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone3.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone3.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone3.GetComponent<BulletShotgun>()._facingRight = facingRight;
        //shoot bottom bullet
        GameObject BulletClone4 = (GameObject)Instantiate(bullet, firePointBottom.position, firePointBottom.rotation);
        BulletClone4.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone4.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone4.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone4.GetComponent<BulletShotgun>()._facingRight = facingRight;
        //shoot bottom bullet
        GameObject BulletClone5 = (GameObject)Instantiate(bullet, firePointBottom.position, firePointBottomMid.rotation);
        BulletClone5.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone5.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone5.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone5.GetComponent<BulletShotgun>()._facingRight = facingRight;

    }
    private void ShootLEFT()
    {
        // shooting logic
        GameObject BulletClone1 = (GameObject)Instantiate(bullet, firePoint.position, Quaternion.identity);
        BulletClone1.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone1.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone1.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone1.GetComponent<BulletShotgun>()._facingRight = facingRight;
        //shoot top bullet
        GameObject BulletClone2 = (GameObject)Instantiate(bullet, firePointTop.position, Rot1);
        BulletClone2.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone2.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone2.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone2.GetComponent<BulletShotgun>()._facingRight = facingRight;
        //shoot bottom bullet
        GameObject BulletClone3 = (GameObject)Instantiate(bullet, firePointBottom.position, Rot2);
        BulletClone3.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone3.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone3.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone3.GetComponent<BulletShotgun>()._facingRight = facingRight;
        //shoot bottom bullet
        GameObject BulletClone4 = (GameObject)Instantiate(bullet, firePointBottom.position, Rot3);
        BulletClone4.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone4.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone4.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone4.GetComponent<BulletShotgun>()._facingRight = facingRight;
        //shoot bottom bullet
        GameObject BulletClone5 = (GameObject)Instantiate(bullet, firePointBottom.position, Rot4);
        BulletClone5.GetComponent<BulletShotgun>().damage = attackDamage;
        BulletClone5.GetComponent<BulletShotgun>().speed = bulletSpeed;
        BulletClone5.GetComponent<BulletShotgun>().knockback = knockback;
        BulletClone5.GetComponent<BulletShotgun>()._facingRight = facingRight;
    }

}
