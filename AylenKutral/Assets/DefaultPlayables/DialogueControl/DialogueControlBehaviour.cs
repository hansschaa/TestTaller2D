using UnityEngine;
using UnityEngine.Playables;
using Yarn.Unity;

public class DialogueControlBehaviour : PlayableBehaviour 
{
	public DialogueRunner dialogueRunner = null;
	public TextAsset textAsset;
	public Vector3 otherPersonPosition;
	public bool isFinal;

	public override void OnBehaviourPlay(Playable playable , FrameData info)
	{
		if(dialogueRunner != null)
		{
			dialogueRunner.ShowDialog(otherPersonPosition,textAsset,isFinal);
		}
	}


}