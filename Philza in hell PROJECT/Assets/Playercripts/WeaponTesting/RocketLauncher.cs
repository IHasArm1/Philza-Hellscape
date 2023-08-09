using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aiming Bullet Explosion
public class RocketLauncher : MonoBehaviour
{
    public float attackRate;
    private float nextAttackTime;
    private Camera mainCam;
    public GameObject rotatePoint;
    private Vector3 mousePos;
    public GameObject bullet;
    public Transform bulletTransform;
    public bool canFire;

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

        rotatePoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Time.time >= nextAttackTime)
        {
            if (Input.GetAxisRaw("Fire1") > 0)
            {
                Shoot();
                nextAttackTime = Time.time + (1f / attackRate);
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, bulletTransform.position, Quaternion.identity);
    }
}
