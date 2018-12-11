using UnityEngine;
using UnityEngine.Playables;
using Yarn.Unity;

public class DialogueControlBehaviour : PlayableBehaviour 
{
	public DialogueRunner dialogueRunner = null;
	public string nodeStart;

	public override void OnBehaviourPlay(Playable playable , FrameData info)
	{
		if(dialogueRunner != null)
		{
			dialogueRunner.StartDialogue(nodeStart);
		}
	}


}