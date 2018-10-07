using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLadderController : MonoBehaviour 
{
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player") && Input.GetKey (KeyCode.Y)) 
		{
				other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, other.GetComponent<CPlayerMovement> ().climbVelocity);
		} 
		
		else if (other.CompareTag("Player") && Input.GetKey (KeyCode.H)) 
		{
				other.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -other.GetComponent<CPlayerMovement> ().climbVelocity);
		} 

	}
}
