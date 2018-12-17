using UnityEngine;

public class CGameOverController : MonoBehaviour 
{

	public delegate void GameOverDelegate();
	public static event GameOverDelegate OnGameOver;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("MyPlayer") && OnGameOver != null)
                OnGameOver();
	}

}
