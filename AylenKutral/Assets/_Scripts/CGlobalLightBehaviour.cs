using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CGlobalLightBehaviour : MonoBehaviour 
{
	public Light globalLight;
	public SpriteRenderer sky;

	#region "Amanecer"
	public Color amanecerGlobalLight;
	public Color amanecerSkyColor;
	#endregion

	#region "Atardecer"
	public Color atardecerGlobalLight;
	public Color atardecerSkyColor;
	#endregion

	#region "Anochecer"
	public Color anochecerGlobalLight;
	public Color anochecerSkyColor;
	#endregion

	public int idChange;


	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("MyPlayer"))
		{
			switch(idChange)
			{
				//Mañana
				case 0:
					globalLight.DOColor(amanecerGlobalLight,4f);
					sky.DOColor(amanecerSkyColor,4f);
					break;

				//Atardecer
				case 1:
					globalLight.DOColor(atardecerGlobalLight,4f);
					sky.DOColor(atardecerSkyColor,4f);
					break;
				
				//Noche
				case 2:
					globalLight.DOColor(anochecerGlobalLight,4);
					sky.DOColor(anochecerSkyColor,4f);
					break;
			}
		}
	}

	
}
