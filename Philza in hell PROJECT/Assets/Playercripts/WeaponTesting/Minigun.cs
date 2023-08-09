using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//STRAIGHTSHOT RAYCAST
public class Minigun : MonoBehaviour
{
    public Animator animator;
    public Transform firePoint;
    public int attackDamage = 10;
    public Vector2 knockback = new(0, 0);
    //public GameObject impactEffect;
    public LineRenderer lineRenderer;

    private CharacterController2D charcon;
    public float attackRate = 2f; // how many times per second you can attack
    private float nextAttackTime = 0f;

    private void Start()
    {
        charcon = GetComponentInParent<CharacterController2D>();
    }

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetAxisRaw("Fire1") > 0)
            {
                StartCoroutine(Shoot());
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }
    }

    IEnumerator Shoot()
    {
        // shooting logic
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hitInfo)
        {
            EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                if (charcon.m_FacingRight)
                {
                    enemy.GetComponent<EnemyHealth>().Hurt(attackDamage, knockback * 1000);
                }
                else
                {
                    enemy.GetComponent<EnemyHealth>().Hurt(attackDamage, -knockback * 1000);
                }

            }
            // Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, hitInfo.point);

        } else
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 1000);
        }

        lineRenderer.enabled = true;

        yield return .01;

        lineRenderer.enabled = false;
    }
}
