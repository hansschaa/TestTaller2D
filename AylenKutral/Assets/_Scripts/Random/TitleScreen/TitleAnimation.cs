using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using Rewired;
using System;


public class TitleAnimation : MonoBehaviour 
{
	public TextMeshProUGUI title;
	public TextMeshProUGUI buttonForPlay;

	#region  "Events"
	public delegate void CutSceneDelegate();

	public static event CutSceneDelegate OnCutScene;
	

	#endregion


	[Header("PlayableDirector")]
	public UnityEngine.Playables.PlayableDirector earthquakePlayable;


	Sequence _inSceneSequence;
	Sequence _inPressForPlaySequence;
	Sequence _inStartSequence;
	private bool canPressStart;


	[Header("Music Variables")]
	public AudioClip natureAmbience;
	public AudioClip partyAmbiance;
	private AudioSource _audioSource;


	[Header("Rewired Variables")]
	private int playerId;
	[HideInInspector] public Player player;



	Tween b;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		title = title.GetComponent<TextMeshProUGUI>();
		_audioSource = this.GetComponent<AudioSource>();
		canPressStart = false;
	}



	

	// Use this for initialization
	void Start () 
	{
		_audioSource.PlayOneShot(natureAmbience,1f);
		_audioSource.PlayOneShot(partyAmbiance,.1f);

		this.player = ReInput.players.GetPlayer(0);


		
		_inSceneSequence = DOTween.Sequence();
		_inStartSequence = DOTween.Sequence();
	
		Tween a = DOTween.To(()=> title.outlineWidth, x => title.outlineWidth = x, 0, 2.5f).SetEase(Ease.Linear);
		_inSceneSequence.Append(a).AppendCallback(()=> 
		{
            b = buttonForPlay.DOFade(1,1.5f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
			canPressStart = true;
        });
		
	}


	void Update()
	{
		if(this.player.GetButtonDown("Start") && GameStateManager.eGameState != EGameState.CUTSCENE && canPressStart)
		{
			GameStateManager.eGameState = EGameState.CUTSCENE;

			b.Kill();
			DOTween.Kill(b);
			
			_inStartSequence.Append(buttonForPlay.DOFade(0,1f).SetEase(Ease.Linear)).
			Join(title.DOFade(0,1f).SetEase(Ease.Linear).OnComplete(()=> {
               InitializeCameraTransition();
            }));

		}
	}

    private void InitializeCameraTransition()
    {
		if(OnCutScene!=null)
		{
			print("Inicializando barras");
			OnCutScene();
		}
			


        earthquakePlayable.Play();
		
    }
}
