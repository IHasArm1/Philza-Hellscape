using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int enemyMaxHealth = 100;
    public int enemyHealth;
    public int enemyDamage;
    public Animator anim;
    public Rigidbody2D rb;
    [SerializeField] private ParticleSystem hurtPartical;
    [SerializeField] private ParticleSystem deathPartical;

    private void Update()
    {
        if(rb.velocity.x >= 45)
        {
            rb.velocity = new Vector2(40f, 0f);
        }
    }

    public void Hurt(int damage, Vector2 knockback)
    {
        //animation when hurt
        hurtPartical.Play();
        enemyHealth -= damage;
        if(enemyHealth > 0)
        {
            rb.AddRelativeForce(knockback);
        }
        if (enemyHealth <= 0)
        {
            anim.SetBool("enemyDead", true);
            StartCoroutine(EnemyDeath());
        }
    }


    IEnumerator EnemyDeath()
    {
        yield return new WaitForSeconds(0.15f);

        Destroy(gameObject);

    }
}
