using UnityEngine;
using UnityEngine.Events;

public class CPlayerController : MonoBehaviour 
{

	private CSavePoint lastSavePoint;
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
	[SerializeField] private Collider2D m_CrouchAbleCollider;				    // A collider that will be abled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;
	public bool m_OnWater;
	

	[HideInInspector] public float textureWidth;

	// Whether or not the player is grounded.
	const float k_CeilingRadius = .5f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]


	public bool m_wasCrouching = false;
	public EInputMode eInputMode;
    public EState eState;
	private CPlayerInput cPlayerInput;

	private void Awake()
	{	
		eInputMode = EInputMode.FREEMOVEMENT;
        eState = EState.NORMAL;
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		cPlayerInput = this.GetComponent<CPlayerInput>();
		//textureWidth = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().bounds.size.x;
	}

	

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		
		//m_Grounded = false;

		var raycasthit2d = Physics2D.Raycast(m_GroundCheck.position, Vector3.down , 1f, m_WhatIsGround);
		//print(raycasthit2d);
		//Raycast collision whit ground gameObject
		
		m_Grounded = raycasthit2d;

		//NormalizeSlope();

		//print(m_Rigidbody2D.velocity);

		
		//print("Angle: " + Vector2.Angle(Vector3.up,raycasthit2d.normal));
		
		
		if(!cPlayerInput.onLadder && !m_wasCrouching)
			if(Vector2.Angle(Vector3.up,raycasthit2d.normal) != 0)
			{
				//m_Rigidbody2D.mass = 0.01f;
				m_Rigidbody2D.gravityScale = 16;
				//Vector2 amountDiscount = new Vector2(Physics2D.gravity * Mathf.Cos(Vector2.Angle(Vector3.up,raycasthit2d.normal)),
		
			}
					
			
			else
			{
				//m_Rigidbody2D.mass = 1f;
				m_Rigidbody2D.gravityScale = 3;
		
			}
			

		
			

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		/* 
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				
				m_Grounded = true;

				break;
				/*if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}	*/
	}

	

	public void Move(float move, bool moveWhitObject, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch && m_wasCrouching)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
				crouch = true;
			
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
				{
					m_CrouchDisableCollider.enabled = false;
					m_CrouchAbleCollider.enabled = true;
				}
			} 
			
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
				{
					m_CrouchAbleCollider.enabled = false;
					m_CrouchDisableCollider.enabled = true;
				}
					

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
				}
			}

			Vector3 targetVelocity;
			// Move the character by finding the target velocity
			if(moveWhitObject)
			{
				targetVelocity = new Vector2(move * 5f,m_Rigidbody2D.velocity.y);

				GetComponent<CPlayerInput>().currentInteractiveObject.GetComponent<Rigidbody2D>().velocity = 
				Vector3.SmoothDamp(GetComponent<CPlayerInput>().currentInteractiveObject.GetComponent<Rigidbody2D>().velocity, 
				targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
				
			else
				targetVelocity = new Vector2(move * 10f,m_Rigidbody2D.velocity.y);

			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			
			

			if(cPlayerInput.currentInteractiveObject == null)
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
		if (m_Grounded && jump && !m_OnWater && !this.GetComponent<CPlayerInput>().climbing)
		{
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		this.GetComponent<CPlayerInput>().throwForce *= -1;

		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}

	

	

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("SavePoint"))
		{
			if(lastSavePoint == null)
				lastSavePoint = other.gameObject.GetComponent<CSavePoint>();

			else if(other.GetComponent<CSavePoint>().id > lastSavePoint.id )
				lastSavePoint = other.gameObject.GetComponent<CSavePoint>();
		}	

		if(other.CompareTag("Water"))
		{
			m_OnWater = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Water"))
		{
			m_OnWater = false;
		}

	}
	void OnEnable()
    {
        CGameOverController.OnGameOver += goToLastSave;
    }
    
    void OnDisable()
    {
        CGameOverController.OnGameOver -= goToLastSave;
    }

	public void goToLastSave()
	{
		this.transform.position = lastSavePoint.GetComponent<CSavePoint>().position;
	}
}
