using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class TestMouse : MonoBehaviour 
{
	private int playerId;
	[HideInInspector] public Player player;
	public float movementVelocity;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		this.player = ReInput.players.GetPlayer(this.playerId);
	}
	// Update is called once per frame
	void Update () 
	{
		this.transform.position += new Vector3(this.player.GetAxisRaw("Move Horizontal") * movementVelocity,this.player.GetAxisRaw("Move Vertical") *movementVelocity,0);
		
	}
}
