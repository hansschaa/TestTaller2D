using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CAnchimallenController : CSpirit ,ISpirit
{
 
    [Header ("Ability")]
    public GameObject circleRegion;


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
        if(spiritUIImage.fillAmount == 1)
        {
            print("usar habilidad");
            Instantiate(circleRegion, transform.position, Quaternion.identity);
            spiritUIImage.transform.parent.transform.localScale = Vector3.one;
            spiritUIImage.fillAmount = 0;
        }
        
    }
}
