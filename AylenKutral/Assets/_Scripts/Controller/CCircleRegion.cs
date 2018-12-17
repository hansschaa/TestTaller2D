using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CCircleRegion : MonoBehaviour 
{


	// Use this for initialization
	void Start () 
	{
		transform.DOScale(12,1).SetEase(Ease.OutBack).OnComplete(()=> Destroy(this.gameObject));
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Enemy/Movil"))
		{
			//other.GetComponent<SpriteRenderer>().color = Color.red;
			other.GetComponent<CEpunamunBehaviour>().Stunned();
		}
	}
}
