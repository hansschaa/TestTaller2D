using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Yarn.Unity.Example;

public class CCutSceneManager : MonoBehaviour 
{


    [Header ("Virtual Cameras")]
    public GameObject normal_PlayerCamera;
    public GameObject tiny_PlayerCamera;
    public GameObject farLeft_PlayerCamera;


    [Header ("Others")]
    public Animator canvas;
    public GameObject gameCanvas;
    public Rigidbody2D m_rigidBody;



	 void OnEnable()
    {
        TitleAnimation.OnCutScene += OnCutScenePlay;
        TimelineTrigger.OnCutScene += OnCutScenePlay;
		ExampleDialogueUI.OnOutCutScene += OnCutSceneFinish;
    }

     void OnDisable()
    {
		TitleAnimation.OnCutScene -= OnCutScenePlay;
        TimelineTrigger.OnCutScene -= OnCutScenePlay;
        ExampleDialogueUI.OnOutCutScene -= OnCutSceneFinish;
    }

    public void OnCutScenePlay()
    {
        canvas.SetTrigger("ShowBars");
        gameCanvas.SetActive(false);
    }

    

    public void OnCutSceneFinish()
    {
        canvas.SetTrigger("HideBars");

        gameCanvas.SetActive(true);

        if(normal_PlayerCamera!= null)
        {
             normal_PlayerCamera.SetActive(true);
        }

        if(tiny_PlayerCamera != null)
            tiny_PlayerCamera.SetActive(false);

        if(farLeft_PlayerCamera != null)
            farLeft_PlayerCamera.SetActive(false);
    }

}
