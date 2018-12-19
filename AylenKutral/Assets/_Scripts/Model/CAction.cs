using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CAction : MonoBehaviour ,IActionObject
{
	public bool used;

    public abstract void OnUse(CPlayerInput cPlayerInput);
   
}
