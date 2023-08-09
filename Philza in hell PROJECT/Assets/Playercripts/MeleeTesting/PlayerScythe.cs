using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScythe : MonoBehaviour
{

    public Animator animator;
    [SerializeField] private CharacterController2D charcon;
    public int playerKnockback = 20;
    public Vector2 knockback;
    public int attackDamage = 10;
    public float attackRange = 0.5f;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public float attackRate = 2f;
    float nextAttackTime = 0f;

    void Update()
    {
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetAxisRaw("Fire1") > 0.9)
            {
                Attack();
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }
    }

    void Attack()
    {
        animator.SetTrigger("ScytheAttack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        knockback = new Vector2(playerKnockback, 0f);
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