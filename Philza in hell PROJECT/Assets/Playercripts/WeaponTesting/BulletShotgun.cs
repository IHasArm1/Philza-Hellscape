using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotgun : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 40;
    public Vector2 knockback = new(0, 0);
    public bool _facingRight;
    public Rigidbody2D rb;
    public float rotation;
    public float DamageDropoff;
    private float nextDamageFalloff;

    //public GameObject impactEffect;

    private void Update()
    {
        if (Time.time >= nextDamageFalloff)
        {
            damage -= 2;
            nextDamageFalloff = Time.time + (1f / DamageDropoff);
        }

        if(damage <= 3)
        {
            Destroy(gameObject);
        }

    }

    // use this for initialization
    void Start()
    {
        StartCoroutine(DespawnBullet());
        if (_facingRight)
        {
            rb.velocity = transform.right * speed;
        } else
        {
            rb.velocity = -(transform.right * speed);
        }

        gameObject.transform.rotation = Quaternion.Euler(0, 0, rotation);


    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.transform.CompareTag("Player") && !hitInfo.transform.CompareTag("DevstuffDONTHIT") && !hitInfo.transform.CompareTag("PlayerBullet"))
        {
            Debug.Log(hitInfo.transform.name);
            EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.GetComponent<EnemyHealth>().Hurt(damage, knockback * 1000);
            }

            // Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        
    }

    IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(10);
        Debug.Log("Destroying Bulletorg");
        Destroy(gameObject);
    }

}
