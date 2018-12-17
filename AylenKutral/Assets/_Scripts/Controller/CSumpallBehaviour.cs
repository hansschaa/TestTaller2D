using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSumpallBehaviour : CSimplePath 
{

	public bool collidedWPlayer;
	public Transform player;

	public override void Update()
	{
		if(collidedWPlayer)
		{
			FollowPlayer();
		}

		else if(!collidedWPlayer && !inDelay)
		{
			base.Move();
		}

		
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("MyPlayer"))
		{
			collidedWPlayer = true;
			player = other.gameObject.transform;
		}
	}

	/// <summary>
	/// Sent when another object leaves a trigger collider attached to
	/// this object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("MyPlayer"))
		{
			collidedWPlayer = false;
			player = null;
		}
	}

	public void FollowPlayer()
	{
		

	}
	
}
