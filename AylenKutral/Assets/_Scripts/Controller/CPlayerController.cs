using System;
using UnityEngine;
using UnityEngine.Events;
using Yarn.Unity.Example;
using UnityEngine.SceneManagement;

public class CPlayerController : MonoBehaviour 
{

	[Header ("Managers")]
	public CSpiritManager cSpiritManager;


	private CSavePoint lastSavePoint;
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private CapsuleCollider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
	[SerializeField] private Collider2D m_CrouchAbleCollider;				    // A collider that will be abled when crouching

	public GameObject arbolCaido1;
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    public bool m_Grounded;
	public bool m_OnWater;
	

	[HideInInspector] public float textureWidth;

	// Whether or not the player is grounded.
	const float k_CeilingRadius = .5f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	public RaycastHit2D raycasthit2d ;


	public bool m_wasCrouching = false;
	public EInputMode eInputMode;
    public EState eState;
	private CPlayerInput cPlayerInput;
    public LayerMask m_WhatIsWater;


    private void Awake()
	{	
		eInputMode = EInputMode.FREEMOVEMENT;
        eState = EState.NORMAL;
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		cPlayerInput = this.GetComponent<CPlayerInput>();
	}

	

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		
		m_OnWater =  Physics2D.Raycast(m_CeilingCheck.position, Vector3.down , 1.5f, m_WhatIsWater);
		if(eInputMode == EInputMode.FREEMOVEMENT && m_OnWater)
		{
			cSpiritManager.TemporalyAnchimallenDissapear();
			//cSpiritManager.ChangeSpirit(ESpirit.NONE);
			eInputMode = EInputMode.SWIM;
			ChangeColliderOrientation(false);
		}
			


		else if(eInputMode == EInputMode.SWIM && !m_OnWater)	
		{
			ChangeColliderOrientation(true);
			//m_Rigidbody2D.gravityScale = 3;
			cSpiritManager.ActivateAnchimallen();
			eInputMode = EInputMode.FREEMOVEMENT;
		}
	}

    public void goToScene()
	{
		SceneManager.LoadScene(1);
	}
	

	private void FixedUpdate()
	{

		bool wasGrounded = m_Grounded;
		raycasthit2d = Physics2D.Raycast(m_GroundCheck.position, Vector3.down , .5f, m_WhatIsGround);

		if(!m_Grounded && raycasthit2d)
		{
			//Check the y Velocity
			if(m_Rigidbody2D.velocity.y < -30)
			{
				print("Die");
				cPlayerInput.crouch = false;
				cPlayerInput.onDie = true;

				Invoke("goToScene", 2f);

			}
		}


		

		//print(raycasthit2d);
		//Raycast collision whit ground gameObject
		
		//print("RayCast: " + raycasthit2d.collider.gameObject);
		
		m_Grounded = raycasthit2d;
		

		//NormalizeSlope();

		//print(m_Rigidbody2D.velocity);

		
		//print("Angle: " + Vector2.Angle(Vector3.up,raycasthit2d.normal));
		
		/* 
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
		
			}*/
			

		
			

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

		//Allow know if the player collided whit the peak of the surface for do animation
		if(cPlayerInput.onLadder)
			raycasthit2d = Physics2D.Raycast(m_CeilingCheck.position, Vector3.down , .3f, m_WhatIsGround);
			if(raycasthit2d.collider != null && raycasthit2d.collider.gameObject.CompareTag("Peak"))
				if(raycasthit2d.collider.gameObject.CompareTag("Peak"))
					print("Hacer animación de peak");
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
				Vector3.SmoothDamp(m_Rigidbody2D.velocity, 
				targetVelocity, ref m_Velocity, m_MovementSmoothing);
			}
				
			else
				targetVelocity = new Vector2(move * 10f,m_Rigidbody2D.velocity.y);

			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
			
			

			if(cPlayerInput.currentInteractiveObject == null)
			// If the input is moving the player right and the player is facing left...
				if (move > 0 && !m_FacingRight && !cPlayerInput.onLadder)
				{
					// ... flip the player.
					Flip();
				}
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (move < 0 && m_FacingRight && !cPlayerInput.onLadder)
				{
					// ... flip the player.
					Flip();
				}
		}
		// If the player should jump...
		if (m_Grounded && jump && !m_OnWater)
		{
			print("entro");
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

		if(other.CompareTag("Hinge"))
		{
			AttachToHinge(other.gameObject);
		}
		

		/* 
		if(other.CompareTag("Water"))
		{
			m_OnWater = true;
			eInputMode = EInputMode.SWIM;
			
		}*/
	}

    private void AttachToHinge(GameObject gameObject)
    {
		gameObject.GetComponent<Collider2D>().enabled = false;

		this.GetComponent<HingeJoint2D>().connectedBody = gameObject.GetComponent<Rigidbody2D>();
		this.GetComponent<DistanceJoint2D>().connectedBody = gameObject.transform.parent.transform.GetChild(0).GetComponent<Rigidbody2D>();

        this.GetComponent<HingeJoint2D>().enabled = true;
		this.GetComponent<DistanceJoint2D>().enabled = true;
		
		eInputMode = EInputMode.INHINGE;
    }

    internal void DisengageToHinge()
    {
		eInputMode = EInputMode.FREEMOVEMENT;

		m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

		this.GetComponent<HingeJoint2D>().enabled = false;
		this.GetComponent<DistanceJoint2D>().enabled = false;

		this.GetComponent<HingeJoint2D>().connectedBody = null;
		this.GetComponent<DistanceJoint2D>().connectedBody = null;

        

        
    }

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Enemy/Movil"))
		{
			
			goToLastSave();
		}	
		
	}

	/* 
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Water"))
		{
			eInputMode = EInputMode.FREEMOVEMENT;
			m_OnWater = false;
			cPlayerInput.runSpeed = 1.5f;
		}

	}*/ 
	void OnEnable()
    {
        CGameOverController.OnGameOver += goToLastSave;
		ExampleDialogueUI.OnDialogueComplete += OnFinishDialogue;
    }
    
    void OnDisable()
    {
        CGameOverController.OnGameOver -= goToLastSave;
		ExampleDialogueUI.OnDialogueComplete -= OnFinishDialogue;
    }

    private void OnFinishDialogue()
    {
        eInputMode = EInputMode.FREEMOVEMENT;
		m_Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public void goToLastSave()
	{
		
		cPlayerInput.onDie = false;
		cPlayerInput._cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.IDLE, "Idle");
		this.transform.position = lastSavePoint.GetComponent<CSavePoint>().position;
	}

  

    public Collider2D CheckGroundHeadCollision()
	{
		return Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround);
	}

	public Collider2D CheckWaterHeadCollision()
	{
		return Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsWater);
	}

    internal void ChangeColliderOrientation(bool toVertical)
    {
		if(!toVertical)			
        	m_CrouchDisableCollider.direction = CapsuleDirection2D.Horizontal;

		else
			m_CrouchDisableCollider.direction = CapsuleDirection2D.Vertical;


		Vector2 currentSize =  m_CrouchDisableCollider.size;
		m_CrouchDisableCollider.size = new Vector2(currentSize.y, currentSize.x);
    }
}
