using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Rewired;

public class CPlayerInput : MonoBehaviour 
{
	#region  "Events"
	public delegate void PickUpDelegate(EItem ei, int i);
	public static event PickUpDelegate OnPickUp;
	#endregion

	[Header("Scripts Variables")]
	public CPlayerController cPlayerController;
	public CInventario cInventario;


	[Header("GameObjects")]
	public GameObject proyectileProyection;
	public Transform shootPosition;
	private Rigidbody2D rb;

	[Header("Objects")]
	public GameObject[] objects;



	[Header("Movement Parameters")]
	public bool climbing;
	public float throwForce;
	public float climbVelocity = 10f;
	public bool onLadder = false;
	public float walkSpeed = 60f;
	private float runSpeed = 1.5f;
	private float horizontalMove = 0f;
	private float verticalMove = 0f;
	private bool jump = false;
	private bool crouch = false;


	[Header("Rewired Variables")]
	private int playerId;
	[HideInInspector] public Player player;


	void Awake()
	{
		this.player = ReInput.players.GetPlayer(this.playerId);
		this.rb = this.GetComponent<Rigidbody2D>();
	}


	void Update () 
	{
		
		#region "Free movement Input"
		if(cPlayerController.eInputMode == EInputMode.FREEMOVEMENT)
		{
			if((this.player.GetNegativeButtonDoublePressDown("Move Horizontal") || 
			this.player.GetButtonDoublePressDown("Move Horizontal")) && !this.crouch && this.GetComponent<CPlayerController>().m_Grounded)
				runSpeed = 2.5f;

			if((this.player.GetNegativeButtonUp("Move Horizontal") || this.player.GetButtonUp("Move Horizontal")) && runSpeed == 2.5f)
				runSpeed = 1.5f;

			horizontalMove = this.player.GetAxisRaw("Move Horizontal") * walkSpeed * runSpeed;

			if(this.player.GetButtonDown("Jump"))
				jump = true;

			if(this.player.GetButton("Down"))
				crouch = true;
			
			if(this.player.GetButtonUp("Down"))
				crouch = false;

			if(this.player.GetButtonDown("Throw") && cPlayerController.m_Grounded && !cPlayerController.m_OnWater)
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

		#region "Throw"
		else if(cPlayerController.eInputMode == EInputMode.THROW )
		{
			if(this.player.GetButtonDown("Cancel"))
			{
				this.proyectileProyection.SetActive(false);
				cPlayerController.eInputMode = EInputMode.FREEMOVEMENT;	
			}

			if(this.player.GetButtonDown("Throw") )
			{
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

		
	}

	void FixedUpdate()
	{
		if(cPlayerController.eInputMode == EInputMode.FREEMOVEMENT)
		{
			cPlayerController.Move(horizontalMove * Time.deltaTime,crouch,jump);
			jump = false;
		}
	}

	/// <summary>
	/// Sent when another object leaves a trigger collider attached to
	/// this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Ladder") && onLadder) 
		{
			rb.gravityScale = 3;
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

		if (other.CompareTag("Ladder")) 
		{
			if(this.player.GetAxisRaw("Move Vertical") != 0)
			{
				print("Tratando de subir");
			    rb.velocity = new Vector2(rb.velocity.x, this.player.GetAxisRaw("Move Vertical") * climbVelocity);
				rb.gravityScale = 0;
				onLadder = true;
				other.GetComponent<CLadderController>().effectorCollider.enabled = false;
			}

			else if(this.player.GetAxisRaw("Move Vertical") == 0 && onLadder)
			{
				rb.velocity = new Vector2(rb.velocity.x,0);
			}
				
		}




		//Presiono para arriba estando en la escalera
		/* 
		if (other.CompareTag("Ladder") && this.player.GetButton("Climb") && cPlayerController.eInputMode == EInputMode.FREEMOVEMENT) 
		{
			if(!climbing)
				climbing = true;

			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.player.GetAxisRaw("Move Horizontal"), climbVelocity);
		} 
		
		//Presiono para abajo estando en la escalera
		else if (other.CompareTag("Ladder") && this.player.GetNegativeButton("Climb") && cPlayerController.eInputMode == EInputMode.FREEMOVEMENT) 
		{
			if(!climbing)
				climbing = true;

			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.GetComponent<Rigidbody2D> ().velocity.x, -climbVelocity);
		} 

		//no hago nada sobre la escalera check
		else if (other.CompareTag("Ladder") && this.climbing && cPlayerController.eInputMode == EInputMode.FREEMOVEMENT) 
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D> ().velocity.x, .5f);
		}

		
		if (other.CompareTag("Ladder") && cPlayerController.eInputMode == EInputMode.FREEMOVEMENT) 
		{
			print("Saltar");
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, 20), ForceMode2D.Impulse);
		}

		else if (other.CompareTag("Ladder") && this.player.GetButton("Climb") && cPlayerController.eInputMode == EInputMode.FREEMOVEMENT) 
		{
			if(!climbing)
				climbing = true;

			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.GetComponent<Rigidbody2D> ().velocity.x, climbVelocity);
		} 
		
		else if (other.CompareTag("Ladder") && this.player.GetNegativeButton("Climb") && cPlayerController.eInputMode == EInputMode.FREEMOVEMENT) 
		{
			if(!climbing)
				climbing = true;

			this.GetComponent<Rigidbody2D> ().velocity = new Vector2 (this.GetComponent<Rigidbody2D> ().velocity.x, -climbVelocity);
		} 

		if (other.CompareTag("Ladder") && this.player.GetNegativeButton("Move Horizontal") && this.player.GetButtonDown("Jump")  && cPlayerController.eInputMode == EInputMode.FREEMOVEMENT) 
		{
			print("Saltar");
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15, 20), ForceMode2D.Impulse);
		}

		/* 
		else if (other.CompareTag("Ladder") && cPlayerController.eInputMode == EInputMode.FREEMOVEMENT) 
		{
			this.GetComponent<Rigidbody2D>().velocity += new Vector2(this.GetComponent<Rigidbody2D> ().velocity.x, -.5f);
		}*/
	}

	public void ResetVelocity()
	{
		rb.velocity = Vector2.zero;
		runSpeed = 1.5f;
	}

	
}
