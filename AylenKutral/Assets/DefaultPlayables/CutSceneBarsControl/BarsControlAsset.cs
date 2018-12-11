using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BarsControlAsset : PlayableAsset 
{
	public ExposedReference<CCutSceneManager> cCutSceneManager;
	public bool showBars;

	public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
   {
        var playable = ScriptPlayable<BarsControlBehaviour>.Create(graph);
      
		var barsControlBehaviour = playable.GetBehaviour();

		barsControlBehaviour.cCutSceneManager = cCutSceneManager.Resolve(graph.GetResolver());

        barsControlBehaviour.showBars = showBars;

       return playable;   
   }

	
}
