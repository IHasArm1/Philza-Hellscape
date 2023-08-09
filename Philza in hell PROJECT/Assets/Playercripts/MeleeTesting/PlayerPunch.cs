using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{

    public Animator animator;
    public Vector2 knockback = new(0, 0);
    public int attackDamage = 10;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private CharacterController2D charcon;
    public float attackRate = 2f; // how many times per second you can attack
    private float nextAttackTime = 0f;

    private void Start()
    {
        charcon = GetComponentInParent<CharacterController2D>();
    }
    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Punch"))
            {
                Attack();
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("Punch");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            if(charcon.m_FacingRight)
            {
                enemy.GetComponent<EnemyHealth>().Hurt(attackDamage, knockback * 1000);
            } else
            {
                enemy.GetComponent<EnemyHealth>().Hurt(attackDamage, -knockback * 1000);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
