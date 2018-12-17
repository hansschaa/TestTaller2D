using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	private PolyNavAgent _agent;
	private PolyNavAgent agent{
		get {return _agent != null? _agent : _agent = GetComponent<PolyNavAgent>();}
	}

	public Transform playerTransform;

/* 
	void OnEnable(){
		agent.OnDestinationReached += MoveRandom;
		agent.OnDestinationInvalid += MoveRandom;
	}

	void OnDisable(){
		agent.OnDestinationReached -= MoveRandom;
		agent.OnDestinationInvalid -= MoveRandom;
	}*/

	// Use this for initialization
	void Start () {
		//agent.SetDestination(playerTransform.position);
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		agent.SetDestination(playerTransform.position);
	}
}
