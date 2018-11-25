using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMoveRock : CInteractiveObject 
{
	public bool onWater;
	public PhysicsMaterial2D slipperyPhysicMaterial2D;
	public PhysicsMaterial2D rockPhysicMaterial2D;

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Water") && !onWater)
        {
            //this.GetComponent<CapsuleCollider2D>().enabled = false;
            //this.GetComponent<BoxCollider2D>().enabled = true;
			this.GetComponent<Rigidbody2D>().mass = 2;
            onWater = true;
        }
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Water") && onWater)
        {
            //this.GetComponent<CapsuleCollider2D>().enabled = false;
            //this.GetComponent<BoxCollider2D>().enabled = true;
			this.GetComponent<Rigidbody2D>().mass = 12;
            onWater = false;
        }
	}


	
}
