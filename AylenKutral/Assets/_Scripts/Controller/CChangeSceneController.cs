using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class CChangeSceneController : MonoBehaviour 
{
	Sequence sequence;
	public Image fade;
	public int scene;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		sequence = DOTween.Sequence();
	}

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("MyPlayer"))
		{
			sequence.Append(fade.DOFade(1,.5f)).Join(other.gameObject.transform.DOMoveX(other.gameObject.transform.position.x + 2,1).OnComplete( ()=> SceneManager.LoadScene(scene)));
		}
	}

    private TweenCallback Hacer()
    {
        throw new NotImplementedException();
    }
}
