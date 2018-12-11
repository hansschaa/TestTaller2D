using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CFadeInOut : MonoBehaviour 
{
	Tween b;
	void OnEnable()
	{
		
		print("Enable");
		b = GetComponent<Image>().DOFade(1,.5f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
		b.Play();
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		print("On Disable");
		b.Kill();
		this.GetComponent<Image>().color = new Color(255,255,255,0);
	}

	
}
