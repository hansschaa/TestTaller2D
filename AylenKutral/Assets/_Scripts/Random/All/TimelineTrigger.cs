using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineTrigger : MonoBehaviour 
{

	#region  "Events"
	public delegate void CutSceneDelegate();

	public static event CutSceneDelegate OnCutScene;
	

	#endregion


	public bool sendEvent;

	[Header("PlayableDirector")]
	public PlayableDirector timeLineToTrigger;


	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("MyPlayer"))
		{
			if(sendEvent)
				if(OnCutScene != null)
				{
					GameStateManager.eGameState = EGameState.CUTSCENE;
					other.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
					OnCutScene();
				}
				

			timeLineToTrigger.Play();
		}
	}
}
