using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Rewired;
using UnityEngine.UI;

public class CPlayerInput : MonoBehaviour 
{
	#region  "Events"
	public delegate void PickUpDelegate(EItem ei, int i);
	//public delegate void StrengthDelegate(float amount);
	public static event PickUpDelegate OnPickUp;
	//public static event StrengthDelegate OnStrength;
	#endregion

	[Header("Scripts Variables")]
	public CPlayerController cPlayerController;
	public CInventario cInventario;
	private CPlayerAnimation _cPlayerAnimation;




	[Header("GameObjects")]
	public GameObject proyectileProyection;
	public Transform shootPosition;
	private Rigidbody2D _rb;
	
	//public Image stregthBarImage;


	[Header("Objects")]
	public GameObject[] objects;
	private CInteractiveObject _cInteractiveObject;
	private Rigidbody2D _auxRigidbody2D;
    private CMoveRock _auxMoveRock;
    private CMoveRock _cMoveRock;


	[Header("Movement Parameters")]
	public float diveSpeed;
	public bool climbing;
	public float throwForce;
	public float climbVelocity = 10f;
	public bool onLadder = false;
	private bool _onHide = false;
	public float walkSpeed = 60f;
	public float runSpeed = 1.5f;
	private float horizontalMove = 0f;
	private float verticalMove = 0f;
	private bool jump = false;
	public bool crouch = false;
	private bool _needGround;


    [Header("States parameters")]
    public float paralizedTime;


    [Header("Collision Parameters")]
	[SerializeField] private LayerMask m_WhatIsInteractiveObjects;	
	public GameObject currentInteractiveObject;	
	[HideInInspector] private bool _caughtToObject;	
	public float distanceToPush;	


	[Header("Rewired Variables")]
	private int playerId;
	[HideInInspector] public Player player;
	[HideInInspector] private SpriteRenderer _spriteRenderer;

	/* 
	[Header("Reduction of strength")]
	public float runReduction;
	public float pushReduction;
	public float climbReduction;
	public float throwReduction;
	public float jumpLadderReduction;*/

    public CPlayerTrap trap;


	void Awake()
	{
		
		this.player = ReInput.players.GetPlayer(0);
		this._rb = this.GetComponent<Rigidbody2D>();
		this._needGround = true;
		this._caughtToObject = false;
		this._onHide = false;
		_spriteRenderer = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		_cPlayerAnimation = this.GetComponent<CPlayerAnimation>();
	}


	void Update () 
	{

        //this.paralizedTime =Time.time + 2000;

        #region "For events"
		/* 
        if (runSpeed == 2.5f && stregthBarImage.fillAmount >= runReduction)
			if(OnStrength != null)
                        OnStrength(-runReduction);

		if(runSpeed == 2.5f && stregthBarImage.fillAmount < runReduction)
			runSpeed = 1.5f;
		
		if(_caughtToObject && stregthBarImage.fillAmount >= pushReduction && this.player.GetAxisRaw("Move Horizontal") != 0)
			if(OnStrength != null)
                        OnStrength(-pushReduction);

		if(_caughtToObject && stregthBarImage.fillAmount < pushReduction)
			DisengageObject();*/

		
		#endregion

		#region "Free movement Input"
		if(cPlayerController.eInputMode == EInputMode.FREEMOVEMENT)
		{
			if((this.player.GetNegativeButtonDoublePressDown("Move Horizontal") || 
			this.player.GetButtonDoublePressDown("Move Horizontal")) && !this.crouch && currentInteractiveObject == null &&
			cPlayerController.m_Grounded)
				runSpeed = 2.5f;
		

			else if((this.player.GetNegativeButtonUp("Move Horizontal") || this.player.GetButtonUp("Move Horizontal")) 
			&& runSpeed == 2.5f )
				runSpeed = 1.5f;
				

			/* 
            if (cPlayerController.eState == EState.PARALIZED)
            {
                float lastVelocity = runSpeed; 
                while(paralizedTime > Time.time)
                {
                    runSpeed = 0;
                }
                runSpeed = lastVelocity;
                cPlayerController.eState = EState.NORMAL;
            }

            if(cPlayerController.eState == EState.TIRED)
            {
                //reduzco maximo barra
                //stregthBarImage. -= 0.1;
                //stregthBarImage.fillAmount -= tiredReduction;
            }
            if (cPlayerController.eState == EState.SLEEPING)
            {
                //Vuelvo Todo a la normalidad
                //reduzco maximo barra
                //stregthBarImage. -= 0.1;
                //stregthBarImage.fillAmount -= tiredReduction;
            }
			*/

			horizontalMove = this.player.GetAxisRaw("Move Horizontal") * walkSpeed * runSpeed;
			verticalMove = this.player.GetAxisRaw("Move Vertical");
			

			
			

			if(this.player.GetButtonDown("Jump") && !cPlayerController.m_OnWater && !crouch 
			&& cPlayerController.m_Grounded && currentInteractiveObject == null)
				jump = true;

			
				

			//Agachar
			if(this.player.GetButtonDown("Down") && !crouch && currentInteractiveObject == null &&
			!jump && !onLadder && !cPlayerController.m_OnWater)
			{
				//runSpeed = 1.5f;
				print("ingreso true");
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.CROUCH ,"OnCrouch");
				crouch = true;
			}

			//Parar
			else if(this.player.GetButtonDown("Down") && cPlayerController.CheckGroundHeadCollision() == null)
			{
				print("ingreso false");
				crouch = false;
			} 
				
			
			if(this.player.GetButtonDown("Throw") && cPlayerController.m_Grounded && !cPlayerController.m_OnWater && !crouch)
			{
				//I check if there are rocks in my inventory
				if(cInventario.getItemAmount(EItem.ROCK) > 0)
				{
					ResetVelocity();
					this.proyectileProyection.SetActive(true);
					cPlayerController.eInputMode = EInputMode.THROW;
				}					
			}
	
		}
		#endregion

		#region "Throw Input"
		else if(cPlayerController.eInputMode == EInputMode.THROW )
		{
			if(this.player.GetButtonDown("Cancel"))
			{
				this.proyectileProyection.SetActive(false);
				cPlayerController.eInputMode = EInputMode.FREEMOVEMENT;	
			}

			if(this.player.GetButtonDown("Throw") )
			//&& stregthBarImage.fillAmount >= throwReduction)
			{
				/*if(OnStrength != null)
                        OnStrength(-throwReduction);*/

				cInventario.removeItem(EItem.ROCK);

				float angle = proyectileProyection.transform.rotation.eulerAngles.z;
				GameObject rock = Instantiate(objects[0], shootPosition.position , Quaternion.identity);

				rock.GetComponent<Rigidbody2D>().AddForce(new Vector2 (Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * throwForce);
				Destroy(rock,1.5f);


				this.proyectileProyection.SetActive(false);
				cPlayerController.eInputMode = EInputMode.FREEMOVEMENT;	
			}
		}

        #endregion



        #region "Caught Object Input"
        if (currentInteractiveObject == null && !_onHide && !cPlayerController.m_OnWater)
		{
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), 
			new Vector2(this.transform.localScale.x,0),distanceToPush,m_WhatIsInteractiveObjects);
      
			if (hit.collider != null)
			{
				_auxMoveRock = hit.collider.GetComponent<CMoveRock>();
				_cMoveRock = hit.collider.GetComponent<CMoveRock>();
				_auxRigidbody2D = hit.collider.GetComponent<Rigidbody2D>();
				_cMoveRock.ShowButton(true);
				

				if(hit.collider.gameObject.CompareTag("MovilObject") )
				//&& stregthBarImage.fillAmount >= pushReduction)
				{
					if(this.player.GetButtonDown("Action"))
					{
						_cMoveRock.ShowButton(false);
						_auxMoveRock = null;
						//runSpeed = .8f;
						currentInteractiveObject = hit.collider.gameObject;
						//GetComponent<FixedJoint2D>().connectedBody = currentInteractiveObject.GetComponent<Rigidbody2D>();
						currentInteractiveObject.GetComponent<CapsuleCollider2D>().sharedMaterial = _cMoveRock.slipperyPhysicMaterial2D;
						
						
						_cMoveRock.inInteraction = true;
						_caughtToObject = true;
						//_auxRigidbody2D.constraints = RigidbodyConstraints2D.None;
						_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.PUSH, "OnPush");


						
						//_auxRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
					}
				}
			}

			else if(_cMoveRock != null)
			{
				_cMoveRock.ShowButton(false);
				currentInteractiveObject = null;
			}
		}

		else if(this.player.GetButtonUp("Action") && currentInteractiveObject != null)
			DisengageObject();
        
        #endregion

		#region "Swim Input"
		if(cPlayerController.eInputMode == EInputMode.SWIM)
		{
			
			verticalMove = this.player.GetAxisRaw("Move Vertical");
			horizontalMove = this.player.GetAxisRaw("Move Horizontal") * walkSpeed * runSpeed;

			/* 
			if(this.player.GetButtonDown("Dive") && !onDive)
			{
				onDive = true;
				cPlayerController.ChangeColliderOrientation(false);
				//_rb.gravityScale = 4;
				
			}

			else if(this.player.GetButtonDown("Dive") && onDive)
			{
				//_rb.gravityScale = 3;
				onDive = false;
				cPlayerController.ChangeColliderOrientation(true);
				//horizontalMove = this.player.GetAxisRaw("Move Horizontal") * walkSpeed * diveSpeed;
			}*/

			if(verticalMove != 0 )
				_rb.velocity = new Vector2(_rb.velocity.x, verticalMove * diveSpeed);


		}
		#endregion

		#region "Update Animation"
		//Duda que serás mas cercano al optimo
		//Si hacer la validacion acá o en la clase CPlayerAnimationState
		//Pues envio datos que quizas no usaré porque la animacion a cambiar es la misma que la actual
		
		if(currentInteractiveObject == null)
		{
			if(this._cPlayerAnimation.ePlayerAnimation != EPlayerAnimationState.JUMP
			 && !onLadder )
			{
				if(!crouch && !cPlayerController.m_OnWater && cPlayerController.m_Grounded )//&& _rb.velocity.y <= 0)
				{
					
					if(horizontalMove != 0)
					{
						if(runSpeed==2.5f)
						{
							_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.RUN, "OnRun");	
							//_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.RUN,"Run",2f,0,0.05f);
							//_playerAnimator.SetTrigger("OnRun");
						}
							

						else if(runSpeed == 1.5f)
						{
							_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.WALK, "OnWalk");	
							//_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.WALK,"Walk",2f,0,0.05f);
							//_playerAnimator.SetTrigger("OnWalk");
						}
							
					}
					
					else 
					{
						_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.IDLE, "OnIdle");	
						
					}
						
				}
				 
				else if(crouch)
				{
					_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.CROUCH ,"OnCrouch");
				}
			}

			
			if(!onLadder)
			{
				if(_rb.velocity.y < 0 && !cPlayerController.m_Grounded && !crouch && !cPlayerController.m_OnWater)
					_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.FALL, "OnFall");	

				else if(cPlayerController.m_OnWater)
					_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.SWIM, "OnDive");	

				/* 
				else if(cPlayerController.m_OnWater && this.onDive)
					_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.DIVE, "OnDive");	*/	
			}	

			if(jump)
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.JUMP, "OnJump");	
		}

		else if(currentInteractiveObject != null)
		{
			if(cPlayerController.m_FacingRight && horizontalMove > 0 || !cPlayerController.m_FacingRight && horizontalMove < 0)
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.PUSH, "OnPush");	

			else if(cPlayerController.m_FacingRight && horizontalMove < 0 || !cPlayerController.m_FacingRight && horizontalMove > 0)
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.POP, "OnPop");	
		}

        if (onLadder)
        {
            _cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.CLIMB, "OnClimb");	
        }

		//Stop some animations if the horizontal input or vertical input is 0
		if((horizontalMove == 0 && (int)_cPlayerAnimation.ePlayerAnimation  < 3) || 
		(this.player.GetAxisRaw("Move Vertical") == 0 && (int)_cPlayerAnimation.ePlayerAnimation == 3) ||
		(cPlayerController.m_OnWater && horizontalMove == 0 && this.player.GetAxisRaw("Move Vertical") == 0 ))
			_cPlayerAnimation.StopCurrentAnimation();
		
		
        #endregion
		//print("Animacion: " + _cPlayerAnimation.ePlayerAnimation.ToString());
    }

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		float angleInRadians = Vector2.Angle(Vector3.up,cPlayerController.raycasthit2d.normal) * Mathf.Deg2Rad ;
		//Vector2 a = (Vector2)transform.position + Vector2.right * transform.localScale.x * distanceToPush;
		Gizmos.DrawLine (transform.position, new Vector2(transform.position.x + ( distanceToPush  * Mathf.Cos(angleInRadians)), 
															transform.position.y + ( distanceToPush  * Mathf.Sin(angleInRadians))));
	}
	

	void FixedUpdate()
	{
		if((cPlayerController.eInputMode == EInputMode.FREEMOVEMENT || cPlayerController.eInputMode == EInputMode.SWIM) && !_onHide)
		{
			cPlayerController.Move(horizontalMove * Time.deltaTime, _caughtToObject, crouch,jump);
			jump = false;
		}
	}

    

    void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Ladder") && onLadder) 
		{
			_rb.gravityScale = 3;
			onLadder = false;
			//other.GetComponent<CLadderController>().effectorCollider.enabled = true;	
		}		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Object") && this.player.GetButtonDown("PickUp"))
		{
			for(int i = 0; i < cInventario.slots.Length; i++)
            {
                if(!cInventario.isFull[i])
                {
                    if(OnPickUp != null)
                        OnPickUp(other.GetComponent<CPickup>().eItem ,i);
                    
                    Destroy(other.gameObject);

                    cInventario.isFull[i] = true;
                    break;
                }
            }

		}

		
		else if (other.CompareTag("Ladder") && !onLadder 
		//&& stregthBarImage.fillAmount >= climbReduction * 4 
		&& (cPlayerController.m_Grounded || cPlayerController.m_OnWater))
		{
			if(this.player.GetAxisRaw("Move Vertical") != 0)
			{
				_rb.gravityScale = 0;
				onLadder = true;
				//other.GetComponent<CLadderController>().effectorCollider.enabled = false;
				//_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.CLIMB,"Climb",1,0,0.05f);
			}

		}

		else if (other.CompareTag("Ladder") && onLadder) 
		{
				if(this.player.GetAxisRaw("Move Vertical") != 0)
					_rb.velocity = new Vector2(_rb.velocity.x, this.player.GetAxisRaw("Move Vertical") * climbVelocity);

				else if(this.player.GetAxisRaw("Move Vertical") == 0)
					_rb.velocity = new Vector2(_rb.velocity.x,0);
				
		}

		else if (other.CompareTag("HideZone") && player.GetButtonDown("Action") && !_onHide)
		{
			this._rb.velocity = Vector2.zero;
			jump = false;
			this.transform.GetChild(0).gameObject.SetActive(false);
			this.transform.GetChild(1).gameObject.SetActive(true);
			_onHide = true;
		}

		else if(other.CompareTag("HideZone") && player.GetButtonDown("Action") && _onHide)
		{
			_onHide = false;
			this.transform.GetChild(0).gameObject.SetActive(true);
			this.transform.GetChild(1).gameObject.SetActive(false);
		}
	}

	public void ResetVelocity()
	{
		_rb.velocity = Vector2.zero;
		runSpeed = 1.5f;
	}	

	public void DisengageObject()
	{
		//_auxRigidbody2D.velocity = Vector2.zero;
		currentInteractiveObject.GetComponent<CapsuleCollider2D>().sharedMaterial = _cMoveRock.rockPhysicMaterial2D;
		
		//_cMoveRock = null;
		_cMoveRock.inInteraction = false;
		_auxMoveRock = null;
		_caughtToObject = false;

		/*if(!currentInteractiveObject.GetComponent<CMoveRock>().onWater)
			_auxRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;*/

		currentInteractiveObject = null;
		
	}
}
