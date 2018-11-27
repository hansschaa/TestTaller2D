using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class DisableNigthObjects : MonoBehaviour 
{
	public GameObject[] disableNigthObjects;
	public GameObject[] enableDayObjects;

	public GameObject player;
	private CinemachineVirtualCamera cinemachineVirtualCamera;
	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
	}

	// Use this for initialization
	/// <summary>
	/// This function is called when the object becomes enabled and active.
	/// </summary>
	void OnEnable()
	{
		foreach(GameObject go in disableNigthObjects)
			go.SetActive(false);

		foreach(GameObject go in enableDayObjects)
			go.SetActive(true);

		cinemachineVirtualCamera.Follow = player.transform;

	
	}
}
