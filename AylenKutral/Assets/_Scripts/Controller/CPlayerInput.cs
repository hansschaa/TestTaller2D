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
	private CInteractiveObject _auxInteractiveObject;


	[Header("Movement Parameters")]
	public bool climbing;
	public float throwForce;
	public float climbVelocity = 10f;
	public bool onLadder = false;
	private bool _onHide = false;
	public float walkSpeed = 60f;
	private float runSpeed = 1.5f;
	private float horizontalMove = 0f;
	private float verticalMove = 0f;
	private bool jump = false;
	public bool crouch = false;
	private bool _needGround;


    [Header("States parameters")]
    public float paralizedTime;


    [Header("Collision Parameters")]
	[SerializeField] private LayerMask m_WhatIsInteractiveObjects;	
	[HideInInspector] public GameObject currentInteractiveObject;	
	[HideInInspector] private bool _caughtToObject;		


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
			this.GetComponent<CPlayerController>().m_Grounded)
			//&& stregthBarImage.fillAmount >= runReduction)
				runSpeed = 2.5f;

			/* 
			if(player.GetButtonUp("Action") && _spriteRenderer.sortingLayerName.Equals(hideLayerID))
				_spriteRenderer.sortingLayerName = NormalLayerID;*/

			if((this.player.GetNegativeButtonUp("Move Horizontal") || this.player.GetButtonUp("Move Horizontal")) 
			&& runSpeed == 2.5f )
				runSpeed = 1.5f;

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

			horizontalMove = this.player.GetAxisRaw("Move Horizontal") * walkSpeed * runSpeed;

			

			if(this.player.GetButtonDown("Jump") && !cPlayerController.m_OnWater && !crouch 
			&& cPlayerController.m_Grounded && currentInteractiveObject == null)
			{
				jump = true;
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.JUMP,"Jump",2,1,0);
				/*if(onLadder && stregthBarImage.fillAmount >= jumpLadderReduction)
					if(OnStrength != null)
                        OnStrength(-jumpLadderReduction);*/

			}

			//Agachar
			if(this.player.GetButtonDown("Down") && !crouch && currentInteractiveObject == null &&
			!jump && !onLadder )
			{
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.CROUCH,"Crouch",1f,0,0.05f);
				crouch = true;
			}
				

			//Parar
			else if(this.player.GetButtonDown("Down") && cPlayerController.CheckHeadCollision() == null)
			{
				crouch = false;/* 
				if(runSpeed == 2.5f)
					_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.RUN,"Run",2f,0,0.05f);

				if(runSpeed == 1.5f)
					_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.WALK,"Walk",2f,0,0.05f);*/
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
				Destroy(rock,3f);


				this.proyectileProyection.SetActive(false);
				cPlayerController.eInputMode = EInputMode.FREEMOVEMENT;	
			}
		}

        #endregion



        #region "Caught Object Input"
        if (currentInteractiveObject == null)
		{
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), 
			new Vector2(this.transform.localScale.x,0),1f,m_WhatIsInteractiveObjects);
      
			if (hit.collider != null)
			{
				_auxInteractiveObject = hit.collider.GetComponent<CInteractiveObject>();
				_cInteractiveObject = hit.collider.GetComponent<CInteractiveObject>();
				_auxRigidbody2D = hit.collider.GetComponent<Rigidbody2D>();
				_cInteractiveObject.ShowButton(true);
				

				if(hit.collider.gameObject.CompareTag("MovilObject") )
				//&& stregthBarImage.fillAmount >= pushReduction)
				{
					if(this.player.GetButtonDown("Action"))
					{
						_cInteractiveObject.ShowButton(false);
						_auxInteractiveObject = null;
						currentInteractiveObject = hit.collider.gameObject;
						_cInteractiveObject.inInteraction = true;
						_caughtToObject = true;
						_auxRigidbody2D.constraints = RigidbodyConstraints2D.None;
						//_auxRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
					}
				}
			}

			else if(_auxInteractiveObject != null)
			{
				_cInteractiveObject.ShowButton(false);
				_auxInteractiveObject = null;
			}
		}

		else if(this.player.GetButtonUp("Action") && currentInteractiveObject != null)
			DisengageObject();
        

        #endregion

    
		#region "Update Animation"
		//Duda que serás mas cercano al optimo
		//Si hacer la validacion acá o en la clase CPlayerAnimationState
		//Pues envio datos que quizas no usaré porque la animacion a cambiar es la misma que la actual
		
		if(currentInteractiveObject == null)
		{
			if(!jump && !onLadder )
			{
				if(!crouch && !cPlayerController.m_wasCrouching && !cPlayerController.m_OnWater && cPlayerController.m_Grounded && _rb.velocity.y <= 0)
				{
					if(horizontalMove != 0 && runSpeed==1.5f )
						_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.WALK,"Walk",2f,0,0.05f);
					
					else if(horizontalMove != 0 && runSpeed==2.5f)
						_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.RUN,"Run",2f,0,0.05f);

					else if(horizontalMove == 0)
						_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.IDLE,"Idle",1.4f,0, 0.05f);	
				}	
			}

			if(_rb.velocity.y < 0 && !onLadder && !cPlayerController.m_Grounded && !crouch && !cPlayerController.m_OnWater)
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.FALL,"Fall",1,0,0.05f);	

			else if(cPlayerController.m_OnWater)
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.SWIM,"Swim",1,-1,0.05f);
		}

		else if(currentInteractiveObject != null)
		{
			if(cPlayerController.m_FacingRight && horizontalMove >= 0 || !cPlayerController.m_FacingRight && horizontalMove <= 0)
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.PUSH,"Push",1,0,0.05f);	

			if(cPlayerController.m_FacingRight && horizontalMove < 0 || !cPlayerController.m_FacingRight && horizontalMove > 0)
				_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.POP,"Pop",1,0,0.05f);	
		}
		#endregion
	
	}

	void FixedUpdate()
	{
		if(cPlayerController.eInputMode == EInputMode.FREEMOVEMENT && !_onHide)
		{
			cPlayerController.Move(horizontalMove * Time.deltaTime, _caughtToObject, crouch,jump);
			jump = false;
		}
	}
	/* 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Buildeable") && player.GetButtonDown("E"))
        {
            if (trap.Type == ETrapType.PARALYZING)
            {
                List<EItem> items = new List<EItem>();
                items.Add(EItem.ROCK);
                items.Add(EItem.ROCK);
                trap.OnUse(items);
            }
        }
    }*/

    void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Ladder") && onLadder) 
		{
			_rb.gravityScale = 3;
			onLadder = false;
			other.GetComponent<CLadderController>().effectorCollider.enabled = true;	
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
		&& cPlayerController.m_Grounded) 
		{
			if(this.player.GetAxisRaw("Move Vertical") != 0)
			{
				onLadder = true;
			}

		}

		else if (onLadder) 
		{
			/*if(stregthBarImage.fillAmount >= climbReduction)
			{*/
				if(this.player.GetAxisRaw("Move Vertical") != 0)
				{
					_rb.velocity = new Vector2(_rb.velocity.x, this.player.GetAxisRaw("Move Vertical") * climbVelocity);
					_rb.gravityScale = 0;
					other.GetComponent<CLadderController>().effectorCollider.enabled = false;
					_cPlayerAnimation.ChangeAnimation(EPlayerAnimationState.CLIMB,"Climb",1,0,0.05f);


					/*if(OnStrength != null)
                        OnStrength(-climbReduction);*/
				}

				else if(this.player.GetAxisRaw("Move Vertical") == 0)
					_rb.velocity = new Vector2(_rb.velocity.x,0);
			/*}	

			else
			{
				onLadder=false;
				_rb.gravityScale = 3;
				_rb.velocity = new Vector2(_rb.velocity.x,0);
			}*/
				
		}

		else if (other.CompareTag("HideZone") && player.GetButtonDown("Action") && !_onHide)
		{
			this._rb.velocity = Vector2.zero;
			//_spriteRenderer.sortingLayerName = hideLayerID;
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
		_auxInteractiveObject = null;
		_cInteractiveObject.inInteraction = false;
		_caughtToObject = false;

		if(!currentInteractiveObject.GetComponent<CMoveRock>().onWater)
			_auxRigidbody2D.constraints = RigidbodyConstraints2D.FreezePositionX;

		currentInteractiveObject = null;
		
	}
}
