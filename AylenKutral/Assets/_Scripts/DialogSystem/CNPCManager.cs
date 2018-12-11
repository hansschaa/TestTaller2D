using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

public class CNPCManager : MonoBehaviour 
{
	public Transform[] NPC;
	public ExampleDialogueUI exampleDialogueUI;

	[YarnCommand("ChangeCurrentTalker")]
	public void ChangeCurrentTalker(string npcId) 
	{
		/*if(exampleDialogueUI.currentTransform!= null)
			exampleDialogueUI.currentTransform.gameObject.GetComponent<Animator>().SetTrigger("Idle");*/

		//print("changeCuurentPosition: " + npcId);
		//Vector3 npcPosition = NPC[int.Parse(npcId)].position;
		//Vector3 newVector = new Vector3(npcPosition.x, npcPosition.y + 4f, npcPosition.z);
		//exampleDialogueUI.currentNpcPosition = new Vector3(npcPosition.x, npcPosition.y + 4f, npcPosition.z);
		exampleDialogueUI.ChangeTextBoxPosition(NPC[int.Parse(npcId)]);
	}

	
}
