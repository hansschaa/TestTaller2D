using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHidingPlaceController : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) 
		{
			this.GetComponent<CPlayerMovement>().canHiding = true;
		} 
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")) 
		{
			this.GetComponent<CPlayerMovement>().canHiding = false;
		} 
	}
	
}
