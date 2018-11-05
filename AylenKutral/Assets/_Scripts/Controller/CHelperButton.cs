using UnityEngine;
using DG.Tweening;

public class CHelperButton : MonoBehaviour 
{
	Sequence _onEnableAnimation;
	SpriteRenderer _spriteRenderer; 

	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		_onEnableAnimation = DOTween.Sequence();
		_onEnableAnimation.Append(_spriteRenderer.DOFade(0,1)).SetEase(Ease.InCubic).SetLoops(-1);
	}

	void OnEnable()
	{
		_onEnableAnimation.Play();
	}
}
