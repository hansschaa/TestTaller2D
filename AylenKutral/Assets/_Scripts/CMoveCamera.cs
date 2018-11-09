using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CMoveCamera : MonoBehaviour
{
    bool alejado;
    public GameObject camera;

    private void Start()
    {
        alejado= false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            if (!alejado)
                camera.GetComponent<Animator>().SetTrigger("Alejar");

    }

}
