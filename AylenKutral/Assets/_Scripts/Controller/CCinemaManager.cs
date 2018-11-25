using UnityEngine;
using DG.Tweening;
using System;
using Cinemachine;

public class CCinemaManager : MonoBehaviour 
{
	public CinemachineVirtualCamera _cinemachineVirtualCamera;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		_cinemachineVirtualCamera = this.gameObject.GetComponent<CinemachineVirtualCamera>();
	}

	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		CCollisionZoneController.OnCameraTransition += DoCameraTransition;
	}

	/// <summary>
	/// This function is called when the behaviour becomes disabled or inactive.
	/// </summary>
	void OnDisable()
	{
		CCollisionZoneController.OnCameraTransition -= DoCameraTransition;
	}

    private void DoCameraTransition(float y)
    {	
    	/*Tween w= DOTween.To(()=> _cinemachineVirtualCamera.m_Lens.OrthographicSize, 
		x => _cinemachineVirtualCamera.m_Lens.OrthographicSize = x, 30, 1.5f);

		//Tween q = DOTween.To(()=> _cinemachineVirtualCamera.m_Lens.OrthographicSize, x=> _cinemachineVirtualCamera.m_Lens.OrthographicSize = x, 45, 1).SetOptions(true).Play();
		//DOTween.To(()=> _cinemachineVirtualCamera.m_Lens.OrthographicSize, x=> _cinemachineVirtualCamera.m_Lens.OrthographicSize = x, 100, 1).ForceInit();
		Sequence mySequence = DOTween.Sequence();
		mySequence.Append(w);
		mySequence.Play();*/

		this.GetComponent<Animator>().SetTrigger("ZoomIn");
    }
}
