using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAiming : MonoBehaviour
{
    public Vector2 knockback = new(0, 0);
    public int damage = 40;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
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
            EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.GetComponent<EnemyHealth>().Hurt(damage, knockback * 1000);
            }

            //Instantiate(impactEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
        
    }
    IEnumerator DespawnBullet()
    {
        yield return new WaitForSeconds(10);
        Debug.Log("Destroying Bullet2");
        Destroy(gameObject);
    }
}
