using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Yarn.Unity;

public class DialogueControlAsset : PlayableAsset 
{
	public ExposedReference<DialogueRunner> dialogueRunner;
	public TextAsset textAsset;
	public Vector3 otherPersonPosition;
    public bool isFinal;

   public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
   {
        var playable = ScriptPlayable<DialogueControlBehaviour>.Create(graph);
      
		var dialogueControlBehaviour = playable.GetBehaviour();

		dialogueControlBehaviour.dialogueRunner = dialogueRunner.Resolve(graph.GetResolver());

		dialogueControlBehaviour.textAsset = textAsset;
        dialogueControlBehaviour.otherPersonPosition = otherPersonPosition;
        dialogueControlBehaviour.isFinal = isFinal;
       //dr1 = dialogueRunner.Resolve(graph.GetResolver());

       return playable;   
   }
}