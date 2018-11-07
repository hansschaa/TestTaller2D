using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CCollisionZoneController : MonoBehaviour
{
    public GameObject agua;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CollisionRock"))
        {
            CrecerAgua();
            print("pls");
        }
    }

    private void CrecerAgua()
    {
        this.transform.position += new Vector3(0, 1000);
        agua.transform.DOMoveY(agua.transform.position.y + 37, 5);
        
        
    }
}
