using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class CSpiritManager : MonoBehaviour 
{
	[Header ("SpiritUi")]
	public GameObject[] spiritsSlots;
	public Sprite activeSpirit;


	ESpirit currentSpirit; 

	[Header ("SpiritsInWorld")]
	public GameObject[] spirits;


	[Header ("Other")]
	public Transform playerTransform;

	public void InvokeSpirit(ESpirit eSpirit)
	{
		switch(eSpirit)
		{
			case ESpirit.ANCHIMALLEN:
				break;
			case ESpirit.OTHER:
				break;

		}
	}

	/* 
	public void AddSpirit(ESpirit eSpirit)
	{
		switch(eSpirit)
		{
			case ESpirit.ANCHIMALLEN:
				break;
			case ESpirit.OTHER:
				break;
		}
	}*/

	[YarnCommand("AddSpirit")]
    public void AddSpirit(string spiritId) 
	{
		//Convert the text command of text asset file (dialogue) to int data
		int id = System.Int32.Parse(spiritId);
		
		UpdateCurrentSpirit(id);
    }

	public void UpdateCurrentSpirit(int id)
	{
		//Put active the image that have the spiritImage
		spiritsSlots[id].transform.GetChild(0).gameObject.SetActive(true);

		//Change to the active spirit
		spiritsSlots[id].GetComponent<Image>().sprite = activeSpirit;
		spiritsSlots[id].transform.localScale *= 1.4f;

		

		//Instantiate the spirit in player
		
		switch((ESpirit) id)
		{
			case ESpirit.ANCHIMALLEN:
				spirits[id].gameObject.GetComponent<CAnchimallenController>().enabled = true;
				break;
			
			case ESpirit.OTHER:
				break;
		}
	}


}
