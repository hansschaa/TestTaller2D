using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class CCollisionZoneController : MonoBehaviour
{

    #region  "Events"
	public delegate void CameraTransition(float zoomAmount);
	public static event CameraTransition OnCameraTransition;
	#endregion

    public GameObject agua;
    public UnityEvent m_OnCollisionEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionRock"))
        {
            if(OnCameraTransition != null)
                OnCameraTransition(10);

            CrecerAgua();
            
        }
    }

    private void CrecerAgua()
    {

        this.transform.position += new Vector3(0, 1000);
        agua.transform.DOMoveY(agua.transform.position.y + 37, 5);
        
        
    }
}
