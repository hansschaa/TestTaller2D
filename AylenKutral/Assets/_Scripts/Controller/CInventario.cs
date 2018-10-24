using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CInventario : MonoBehaviour 
{
    public bool[] isFull;
    public GameObject[] slots;
    public GameObject[] objects;

    void OnEnable()
    {
        CPlayerInput.OnPickUp += AddItem;
    }
    
    void OnDisable()
    {
        CPlayerInput.OnPickUp -= AddItem;
    }

    public void AddItem(EItem ei, int i)
    {
        slots[i] = Instantiate(this.objects[(int)ei],transform.GetChild(i).transform.position, Quaternion.identity, transform.GetChild(i).transform);
        slots[i].name = ei.ToString();
    }

    
    public int getItemAmount(EItem eItem)
    {
        int amount =0;
        //print("Entrante: " + eItem.ToString());
        foreach(GameObject go in slots)
        {
            //print(go.name);
            if(go != null && go.name.Equals(eItem.ToString()))
                amount++;
        }

        return amount;
    }

    public void removeItem(EItem eItem)
    {
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            if(transform.GetChild(i).childCount>0)
            {
                if(transform.GetChild(i).GetChild(0).gameObject.name.Equals(eItem.ToString()))
                    isFull[i] = false;
                    Destroy(transform.GetChild(i).GetChild(0).gameObject);
                    return;
            }
                
        }
    }
 
}
