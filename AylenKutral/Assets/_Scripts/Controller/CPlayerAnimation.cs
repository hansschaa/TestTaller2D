using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class CPlayerAnimation : MonoBehaviour 
{

	private UnityArmatureComponent _unityArmatureComponent;
	[HideInInspector] public EPlayerAnimationState ePlayerAnimation;

	void Awake()
	{
		_unityArmatureComponent = transform.GetChild(0).gameObject.GetComponent<UnityArmatureComponent>();
		this.ePlayerAnimation = EPlayerAnimationState.IDLE;
	}

	public void ChangeAnimation(EPlayerAnimationState ePlayerAnimation,string animationName, float timeScale, int looped,float fadeTime)
	{
		//Si es distinta cambio la animacion
		if(ePlayerAnimation == this.ePlayerAnimation)
			return;

		this.ePlayerAnimation = ePlayerAnimation;
		_unityArmatureComponent.animation.timeScale = timeScale;
		_unityArmatureComponent.animation.FadeIn(animationName, fadeTime, looped); // a shooting animation that plays in loop 
		/* 
		_unityArmatureComponent.animation.timeScale = timeScale;
		_unityArmatureComponent.animation.p
		_unityArmatureComponent.animation.Play(animationName);*/
		
	}
}
