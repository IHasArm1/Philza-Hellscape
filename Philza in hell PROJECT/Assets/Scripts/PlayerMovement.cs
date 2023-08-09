using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction crouchAction;
    public InputActionAsset controls;

    // movement
    public CharacterController2D controller;
    public Animator anim;
    public Rigidbody2D rb;
    public BoxCollider2D boxTrig;

    public float maxVel = 20;
    public int runSpeed;
    public bool canMove = true;

    float xMove = 0f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    [SerializeField] private LayerMask m_WhatIsEnemy;

    private void Awake()
    {
        controls.FindAction("Jump").performed += ctx => Jump();

        controls.FindAction("Move").performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void Jump()
    {
        jump = true;
        anim.SetBool("isJumping", true);
    }
    void Move(Vector2 direction)
    {
        xMove = direction.x;
    }
    void Crouch()
    {
        crouch = true;
        anim.SetBool("isCrouching", true);
    }
    void CrouchUp()
    {
        crouch = false;
        anim.SetBool("isCrouching", false);
    }
    // Update is called once per frame
    void Update()
    {
        horizontalMove = xMove * runSpeed;
        // Movement
        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

    }
    public void OnLanding()
    {
        anim.SetBool("isGrounded", true);
        anim.SetBool("isJumping", false);
        anim.SetBool("isDashing", false);
        anim.SetBool("isDownDashing", false);
    }
    public void OnCrouch()
    {
        anim.SetBool("isCrouching", true);
    }


    private void FixedUpdate()
    {
        if (controls.FindAction("Crouch").WasPressedThisFrame())
        {
            Crouch();
        }
        if (controls.FindAction("Crouch").WasReleasedThisFrame())
        {
            CrouchUp();
        }

        if (canMove)
        {
            // movement
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;

            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVel);
        }
    }
}
