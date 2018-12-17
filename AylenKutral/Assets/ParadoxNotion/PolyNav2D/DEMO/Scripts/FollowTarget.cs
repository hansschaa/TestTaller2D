using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(PolyNavAgent))]
public class FollowTarget : MonoBehaviour
{

	#region Reset
	public float resetVelocity; 
	public bool resetPosition;
	#endregion
	

	#region "Store Initial parameters"
	private Vector3 initialPosition;
	private float initialRadiusCollider;
	#endregion

	#region  "PathFinging Variables"
	private Transform target; 
	private PolyNavAgent _agent;
	private PolyNavAgent agent{
		get {return _agent != null? _agent : _agent = GetComponent<PolyNavAgent>();}
	}
	#endregion

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		resetPosition = false;
		initialPosition = transform.position;
		initialRadiusCollider = GetComponent<CircleCollider2D>().radius;
		
	}

	void Update() 
	{
		if (target != null)
		{
			agent.SetDestination( target.position );
		}

		
		else if(resetPosition)
		{
			float step = resetVelocity * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, initialPosition, step);

			if(Mathf.Abs(Vector2.Distance(transform.position, initialPosition)) < 1.0f)
			{
				resetPosition = false;
			}
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
			target = other.transform;
			this.GetComponent<CircleCollider2D>().radius = 10;
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
			//agent.SetDestination(null);
			agent.Stop();
			target = null;
			this.GetComponent<CircleCollider2D>().radius = initialRadiusCollider;
			resetPosition = true;
		}
	}

   
}