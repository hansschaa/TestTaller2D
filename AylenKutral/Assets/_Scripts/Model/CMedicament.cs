using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMedicament : MonoBehaviour, IInventoryIcon
{
    public void OnUse()
    {
		  print("Usar Medicamento");
    }
}
