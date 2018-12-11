using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAnchimallenController : MonoBehaviour ,ISpirit
{
    [Header ("Spirit Movement")]
    public float velocityMovement;
	public Transform playerAnchimallenPosition;
    public CPlayerController cPlayerController;
    public bool isFliped;


    [Header ("Ability")]
    public GameObject circleRegion;

    void OnDrawGizmos()
    {
        
       
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        CPlayerInput.OnSpiritAbility += OnActiveAbility;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        CPlayerInput.OnSpiritAbility -= OnActiveAbility;
    }


    public void OnActiveAbility()
    {
        print("usar habilidad");
        Instantiate(circleRegion, transform.position, Quaternion.identity);
    }



    void Update () 
    {
        
         //Animation when the distance is more than 1 and the Spiriit need getting closer to player
        if(Math.Abs(Vector2.Distance(playerAnchimallenPosition.position,this.transform.position)) > 1f )
        {
            print("Mover hasta la posición");
            transform.position = Vector3.Lerp(transform.position,new Vector3(playerAnchimallenPosition.position.x,playerAnchimallenPosition.position.y + Mathf.Sin(Time.time), 0.0f), velocityMovement*Time.deltaTime);
            
            if(isFliped)
            {
                isFliped=false;
            }
        }

        //Animation when the distance is less than 1
        else
        {
            transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x,playerAnchimallenPosition.position.y + Mathf.Sin(Time.time), 0.0f), velocityMovement*Time.deltaTime);
            
            if(!isFliped)
            {
                isFliped = true;
                if(cPlayerController.m_FacingRight)
                {
                    this.GetComponent<SpriteRenderer>().flipX = true;
                    //this.transform.GetChild(0).gameObject.transform.position -= new Vector3(.25f,0,0);

                }

                else
                {
                    this.GetComponent<SpriteRenderer>().flipX = false;
                    //this.transform.GetChild(0).gameObject.transform.position += new Vector3(.25f,0,0);
                    
                }
                
            }
        }


        
    }

   
}
