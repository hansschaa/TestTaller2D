using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Yarn.Unity;

public class DialogueControlAsset : PlayableAsset 
{
	public ExposedReference<DialogueRunner> dialogueRunner;
	public string nodeStart;

   public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
   {
        var playable = ScriptPlayable<DialogueControlBehaviour>.Create(graph);
      
		var dialogueControlBehaviour = playable.GetBehaviour();

		dialogueControlBehaviour.dialogueRunner = dialogueRunner.Resolve(graph.GetResolver());

        dialogueControlBehaviour.nodeStart = nodeStart;

       //dr1 = dialogueRunner.Resolve(graph.GetResolver());

       return playable;   
   }
}