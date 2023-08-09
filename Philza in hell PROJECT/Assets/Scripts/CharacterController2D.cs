using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[SerializeField] private float m_WallJumpForce = 400f;                      // Amount of force added when the player wall jumps.
	[SerializeField] private float m_WallJumpPush = 400f;                       // Amount of force added to x when the player wall jumps
	[SerializeField] private float m_DashForce = 15f;                           // Amount of force added when the player dashes.
	[SerializeField] private float m_downForce = 15f;                           // Amount of force added when the player downwards dashes.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private LayerMask m_WhatIsWall;                            // A mask determining what is wall to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Transform m_WallCheck;                             // A position marking where to check for walls
	[SerializeField] private Transform m_WallCheck2;                             // A position marking where to check for walls
	[SerializeField] private Animator m_anim;                                   // A animator for the player character
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
	[SerializeField] private StatsBar m_statsbar;                               // A reference to the statsbar for functions
	[SerializeField] private playerBehaviour m_playerbeh;
	[SerializeField] private GameObject Ghosting;
    private Animator m_ghostingAnim;

	public float m_DashCooldown;
	public float m_NextDashTime;
	public float m_DownDashCooldown;
	public float m_NextDownDashTime;
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	const float k_WalledRadius = .2f;   // Radius of the overlap circle to determine if touching wall
	private bool m_Grounded;            // Whether or not the player is grounded
	private bool m_Walled;              // Whether or not the player is touching a wall
	const float k_CeilingRadius = .2f;  // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public MainPlayerInput controls;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;
	public UnityEvent OnDashEvent;
	public UnityEvent OnDownDashEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;



    private void Awake()
	{
		controls = new MainPlayerInput();
		//Controls Setup
		controls.Player.Dash.performed += ctx => Dash();



		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnDashEvent == null)
			OnDashEvent = new UnityEvent();

		if (OnDownDashEvent == null)
			OnDownDashEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}
    private void Start()
    {
        
    }

	private void Update()
	{

		if (m_Grounded)
		{
			m_Walled = false;
			m_anim.SetBool("isGrounded", true);
			m_playerbeh.DJHeal(2);
			m_playerbeh.STHeal(2);
		} else
        {
			m_anim.SetBool("isGrounded", false);
        }
        if (m_Walled)
        {
			m_anim.SetBool("isWalled", true);
			m_playerbeh.STHeal(2);
		}
	}
    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;
		bool wasWalled = m_Walled;
		m_Walled = false;


		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        Collider2D[] colliderwall = Physics2D.OverlapCircleAll(m_WallCheck.position, k_WalledRadius, m_WhatIsWall);
        Collider2D[] colliderwall2 = Physics2D.OverlapCircleAll(m_WallCheck2.position, k_WalledRadius, m_WhatIsWall);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
		for (int i = 0; i < colliderwall.Length; i++)
		{
			if (colliderwall[i].gameObject != gameObject)
			{
				m_Walled = true;
				if (!wasWalled)
					OnLandEvent.Invoke();
			}
		}
		for (int i = 0; i < colliderwall2.Length; i++)
		{
			if (colliderwall2[i].gameObject != gameObject)
			{
				m_Walled = true;
				if (!wasWalled)
					OnLandEvent.Invoke();
			}
		}
	}


	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded//walled or airControl is turned on
		if (m_Grounded || m_AirControl || m_Walled)
		{

			// If crouching
			if (Input.GetButtonDown("Crouch"))
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (GameManager.gameManager._playerDJ.DJ >= 1 && jump)
		{
			// Add a vertical force to the player.
			m_playerbeh.TakeDJ(1);
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
		// If the player should wall jump...
		if (m_Walled && m_FacingRight && jump)
		{
			// Add a vertical & horizontal force to the player.
			m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * m_WallJumpPush, m_WallJumpForce));
		}
		if (m_Walled && !m_FacingRight && jump)
		{
			// Add a vertical & horizontal force to the player.
			m_Rigidbody2D.AddForce(new Vector2(-transform.localScale.x * m_WallJumpPush, m_WallJumpForce));
		}
		

		if(Time.time > m_NextDownDashTime)
        {
			if (Input.GetAxisRaw("DownDash") < 0 && !m_Grounded && !m_Walled)
			{
				m_NextDownDashTime = Time.time + m_DownDashCooldown;

				m_Rigidbody2D.AddForce(new Vector2(0f, -m_downForce * 100));
				OnDownDashEvent.Invoke();
			}
		}
		
	}

	void Dash()
    {
		if (Time.time > m_NextDashTime)
		{
			if (GameManager.gameManager._playerST.ST >= 1 && !m_Grounded && !m_Walled)
			{
				m_NextDashTime = Time.time + m_DashCooldown;
				StartCoroutine(OnDashing());
				if (m_FacingRight)
				{
					// Add a horizontal force to the player.
					m_playerbeh.TakeST(1);
					m_Rigidbody2D.AddForce(new Vector2(transform.localScale.x * (m_DashForce * 10), 0f));
					OnDashEvent.Invoke();
				}
				else if (!m_FacingRight)
				{
					// Add a horizontal force to the player.
					m_playerbeh.TakeST(1);
					m_Rigidbody2D.AddForce(new Vector2(-transform.localScale.x * (m_DashForce * 10), 0f));
					OnDashEvent.Invoke();
				}
			}
		}
	}

	IEnumerator StopDashing()
    {
		yield return new WaitForSeconds(0.05f);

    }

	IEnumerator OnDashing()
    {
		Ghosting.SetActive(true);
		m_ghostingAnim = Ghosting.transform.GetComponent<Animator>();
		m_ghostingAnim.SetBool("Dashing", true);
		yield return new WaitForSeconds(0.1f);
		Ghosting.SetActive(false);
		m_ghostingAnim.SetBool("Dashing", false);
    }


	public void OnDownDash()
    {
		m_anim.SetBool("isDownDashing", true);
		m_anim.SetBool("isGrounded", false);
	}

	public void OnDash()
	{
		m_anim.SetBool("isDashing", true);
		m_anim.SetBool("isGrounded", false);
		StartCoroutine(StopDashing());
	}


	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		transform.Rotate(0f, 180f, 0f);
	}
}
