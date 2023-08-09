using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aiming Bullet
public class SniperRifle : MonoBehaviour
{
    public int attackDamage;
    public float bulletSpeed;
    public float attackRate = 0;
    public Vector2 knockback = new(0, 0);
    private float nextAttackTime = 0;
    private Camera mainCam;
    public GameObject rotatePoint;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - rotatePoint.transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        bulletTransform.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetAxisRaw("Fire1") > 0)
            {
                SpawnBullet();
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }
    }

    void SpawnBullet()
    {
        GameObject BulletClone = (GameObject)Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        BulletClone.GetComponent<BulletAiming>().damage = attackDamage;
        BulletClone.GetComponent<BulletAiming>().speed = bulletSpeed;
        BulletClone.GetComponent<BulletAiming>().knockback = knockback;
    }

}
