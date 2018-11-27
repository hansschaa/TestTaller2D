using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameStateControlAsset : PlayableAsset 
{
	public ExposedReference<GameStateManager> gameStateManager;
	public EGameState eGameState;
    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<GameStateBehaviour>.Create(graph);
      
		var gameStateBehaviour = playable.GetBehaviour();


		gameStateBehaviour.gameStateManager = gameStateManager.Resolve(graph.GetResolver());
        gameStateBehaviour.eGameState = eGameState;
		
	
       //dr1 = dialogueRunner.Resolve(graph.GetResolver());

       return playable;   
    }
}