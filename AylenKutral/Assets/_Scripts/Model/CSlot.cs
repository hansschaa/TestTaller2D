using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSlot : MonoBehaviour {

    private CInventario inventory;
    public int i;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CInventario>();
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }

    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            child.GetComponent<CSpawn>().SpawnDroppedItem();
            GameObject.Destroy(child.gameObject);
        }
    }

}
