using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BarsControlBehaviour : PlayableBehaviour 
{
	public CCutSceneManager cCutSceneManager = null;

	public bool showBars;

	public override void OnBehaviourPlay(Playable playable , FrameData info)
	{
		if(cCutSceneManager != null)
		{
			if(showBars)
				cCutSceneManager.OnCutScenePlay();

			else
				cCutSceneManager.OnCutSceneFinish();
		}
	}
	
}
