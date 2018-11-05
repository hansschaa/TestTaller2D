using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTransparentObject : CHelperIcon 
{

	private Color myColor;
	private Color alphaColor;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		myColor = this.GetComponent<SpriteRenderer>().color;
		alphaColor = new Color (myColor.r,myColor.g,myColor.b,0.75f);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player")) 
		{
			this.GetComponent<SpriteRenderer>().color = alphaColor;
		} 
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player")) 
		{
			this.GetComponent<SpriteRenderer>().color = myColor;
		} 
	}
}
