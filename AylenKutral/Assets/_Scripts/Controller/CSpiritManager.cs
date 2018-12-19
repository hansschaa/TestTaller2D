using System;
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


	

	[Header ("SpiritsGroup")]
	public Transform spiritsGroup;
	public GameObject[] spiritsToInvoke;


	[Header ("Other")]
	public Transform playerTransform;
	public ESpirit currentSpirit; 
	public CNPCManager cNPCManager;

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
		
		//Recognize the spirit to delete
		switch(id)
		{
			case 0:
				Destroy(spiritsGroup.GetChild(0).transform.gameObject);
				break;
			case 1:
				break;
		}

		

		InvokeSpirit(id);
    }


	public void InvokeSpirit(int id)
	{
		//Put active the image that have the spiritImage
		spiritsSlots[id].transform.GetChild(0).gameObject.SetActive(true);

		//Change to the active spirit
		spiritsSlots[id].GetComponent<Image>().sprite = activeSpirit;
		spiritsSlots[id].transform.localScale *= 1.4f;

		switch((ESpirit) id)
		{
			case ESpirit.ANCHIMALLEN:
				cNPCManager.NPC[1] = (Instantiate(spiritsToInvoke[id], playerTransform.Find("AnchimallenPosition").transform.position, Quaternion.identity) as GameObject).transform;
				cNPCManager.NPC[1].GetComponent<CAnchimallenController>().enabled = true;

				cNPCManager.NPC[1].GetComponent<CAnchimallenController>().spiritUIImage = spiritsSlots[0].transform.GetChild(0).GetComponent<Image>();
				cNPCManager.NPC[1].GetComponent<CAnchimallenController>().playerSpiritPosition = playerTransform.Find("AnchimallenPosition").transform;
				cNPCManager.NPC[1].GetComponent<CAnchimallenController>().cPlayerController = playerTransform.gameObject.GetComponent<CPlayerController>();
				
				//spiritsGroup.GetChild(id).gameObject.GetComponent<CAnchimallenController>().enabled = true;

				
				break;
			
			case ESpirit.OTHER:
				break;
		}

		currentSpirit = (ESpirit) id;
		
	}


	public void TemporalyAnchimallenDissapear()
	{
		//Id in the npc manager gameobject of anchimallen
		cNPCManager.NPC[1].gameObject.SetActive(false);
	}

	public void ActivateAnchimallen()
	{
		if(currentSpirit == ESpirit.ANCHIMALLEN)
		{
			cNPCManager.NPC[1].gameObject.SetActive(true);
		}
		
	}
}
