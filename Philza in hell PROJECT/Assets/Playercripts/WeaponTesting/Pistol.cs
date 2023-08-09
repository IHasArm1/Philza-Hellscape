using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

//AIMING RAYCAST
public class Pistol : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    public GameObject rotatePoint;
    public Transform bulletTransform;
    public LineRenderer lineRenderer;
    private float nextAttackTime;
    MainPlayerInput controls;
    [Header("Changes")]
    public float attackRate = 2f; // how many times per second you can attack
    public int attackDamage = 40;
    public Vector2 knockback = new(0, 0);

    private void Awake()
    {
        controls.Player.Aiming.performed += OnMousePos;
    }
    void Start()
    {
        
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void OnMousePos (InputAction.CallbackContext context)
    {
        mousePos = mainCam.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {

        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
            //Ray2D ray = mainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

            //RaycastHit2D hit;
            //if (Physics2D.Raycast(ray, hit))
            //{
               // mousePos = hit.point;
            //}
        //}

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - rotatePoint.transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        rotatePoint.transform.rotation = Quaternion.Euler(0, 0, rotZ);

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
        RaycastHit2D hitInfo = Physics2D.Raycast(bulletTransform.transform.position, bulletTransform.transform.right);

        if (hitInfo)
        {
            EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.GetComponent<EnemyHealth>().Hurt(attackDamage, knockback * 1000);
            }
            // Instantiate(impactEffect, hitInfo.point, Quaternion.identity);

            lineRenderer.SetPosition(0, bulletTransform.transform.position);
            lineRenderer.SetPosition(1, hitInfo.point);

        }
        else
        {
            lineRenderer.SetPosition(0, bulletTransform.transform.position);
            lineRenderer.SetPosition(1, bulletTransform.transform.position + bulletTransform.transform.right * 1000);
        }

        lineRenderer.enabled = true;

        yield return .01;

        lineRenderer.enabled = false;
    }

}
