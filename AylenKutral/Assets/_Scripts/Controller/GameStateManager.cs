using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
	public static EGameState eGameState;

	
	void Start()
	{
		eGameState = EGameState.NORMAL;
	}
}
