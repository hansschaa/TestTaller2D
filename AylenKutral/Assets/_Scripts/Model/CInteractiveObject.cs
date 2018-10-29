using System;
using UnityEngine;

public class CInteractiveObject : MonoBehaviour 
{
	public GameObject buttonToShow;
	public bool inInteraction;


	void Start()
	{
		inInteraction = false;
	}

    public void ShowButton(bool showButton)
    {
        buttonToShow.SetActive(showButton);
    }
	
}
