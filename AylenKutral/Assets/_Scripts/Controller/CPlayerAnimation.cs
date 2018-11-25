using System;
using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class CPlayerAnimation : MonoBehaviour 
{

	private UnityArmatureComponent _unityArmatureComponent;
	[HideInInspector] public EPlayerAnimationState ePlayerAnimation;
	private Animator _playerAnimator;
	

	void Awake()
	{
		this._playerAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
		_unityArmatureComponent = transform.GetChild(0).gameObject.GetComponent<UnityArmatureComponent>();
		this.ePlayerAnimation = EPlayerAnimationState.IDLE;
	}

	public void ChangeAnimation(EPlayerAnimationState ePlayerAnimation,string animationName)
	{
	
		if(this.ePlayerAnimation == ePlayerAnimation)
		{
			
			if(_playerAnimator.speed == 0)
				_playerAnimator.speed = 1;

			return;
		
		}

		this.ePlayerAnimation = ePlayerAnimation;
		_playerAnimator.speed = 1;
		_playerAnimator.SetTrigger(animationName);

			/* 
		_unityArmatureComponent.animation.Play();
		this.ePlayerAnimation = ePlayerAnimation;
		_unityArmatureComponent.animation.timeScale = timeScale;
		_unityArmatureComponent.animation.FadeIn(animationName, fadeTime, looped);  */
	}

    internal void StopCurrentAnimation()
    {
		_playerAnimator.speed = 0;
		
        //_unityArmatureComponent.animation.Stop();
    }
}
