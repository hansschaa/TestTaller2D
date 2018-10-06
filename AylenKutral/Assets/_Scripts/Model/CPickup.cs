using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPickup : MonoBehaviour {

	private CInventario inventory;
    public GameObject itemButtom;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CInventario>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //si el objeto colisiona con el player
        //y apreto la tecla
        if(other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            for(int i = 0; i < inventory.slots.Length; i++)
            {
                if(inventory.isFull[i] == false)
                {
                    //Can add item to inventory
                    inventory.isFull[i] = true;
                    Instantiate(itemButtom, inventory.slots[i].transform);
                    break;
                }
            }
        }
    }

}
