using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStateBehaviour : PlayableBehaviour  
{
	public GameStateManager gameStateManager = null;
	public EGameState eGameState; 

	public override void OnBehaviourPlay(Playable playable , FrameData info)
	{
		if(gameStateManager != null)
			GameStateManager.eGameState = this.eGameState;	
	}
}
