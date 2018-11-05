using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPlayer : MonoBehaviour, ICharacter {
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//movement();	
	}

	public void movement()
    {
		//this.transform.position = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}
