using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CPlayerMovement : MonoBehaviour 
{
	public CPlayerController cPlayerController;
	public float climbVelocity = 5f;
	public float walkSpeed = 40f;
	public float runSpeed = 1f;
	private float horizontalMove = 0f;
	private bool jump = false;
	private bool crouch = false;
	public bool canHiding;				                       


	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Hide") && this.canHiding) 
		{
			print("Esconder");
		} 

		if(Input.GetButtonDown("Run") && !this.crouch && this.GetComponent<CPlayerController>().m_Grounded) 
		{
			runSpeed = 2;
		} 

		else if(Input.GetButtonUp("Run"))
		{
			runSpeed = 1;
		}

	
		horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed * runSpeed;

	
		if(Input.GetButtonDown("Jump"))
		{
			jump = true;
		}


		if(Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		} 
		
		else if(Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}
	}

	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		cPlayerController.Move(horizontalMove * Time.deltaTime,crouch,jump);
		jump = false;
	}
}
