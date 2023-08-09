using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public Vector2 knockback = new(0, 0);
    public float explosionRange;
    public int damage = 40;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float speed;
    public LayerMask enemyLayers;
    public ParticleSystem explosionParticle;
    private SpriteRenderer gfx;
    // Start is called before the first frame update
    void Start()
    {
        gfx = GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(DespawnBullet());
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }



    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (!hitInfo.transform.CompareTag("DevstuffDONTHIT") && !hitInfo.transform.CompareTag("Player") && !hitInfo.transform.CompareTag("PlayerBullet"))
        {
            gfx.sprite = null;
            StartCoroutine(Explosion());

            //Instantiate(impactEffect, transform.position, transform.rotation);
        }

        

    }

    IEnumerator Explosion()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(gameObject.transform.position, explosionRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit enemy");
            enemy.GetComponent<EnemyHealth>().Hurt(damage, new(0,0));
        }
        if(hitEnemies != null)
        {
            rb.velocity = new(0,0);
            explosionParticle.Play();
        }
        yield return new WaitForSeconds(0.23f);
        Destroy(gameObject);
    }

    IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(10);
        Debug.Log("Destroying Bullet3");
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (gameObject.transform.position == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(gameObject.transform.position, explosionRange);
    }
}

