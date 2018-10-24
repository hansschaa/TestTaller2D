using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProyectileProyectionController : MonoBehaviour 
{

	public float maxAngle;
	public float minAngle;
	private int direction = 1;
	private int currentAngle;
	[HideInInspector] public bool stopRotation = false;

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		
		currentAngle = 0;
		direction = 1;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(!stopRotation)
		{
			if(currentAngle < minAngle || currentAngle > maxAngle )
				direction *=-1;

			currentAngle+=(direction);
			transform.localRotation = Quaternion.AngleAxis(currentAngle, Vector3.forward);
		}
		
	}
}
