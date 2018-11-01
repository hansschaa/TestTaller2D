using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CPlayerTrap : MonoBehaviour, ITrap {

    [SerializeField]
    int duration;
    [SerializeField]
    Image Image;
    [SerializeField]
    string Name;

    CInventario inventory;
    CPlayerInput input;
    Transform playerTransform;
    private ETrapType type;

    internal ETrapType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    private void Awake()
    {
        inventory = CPlayerInput.cInventario;
        Type = ETrapType.PARALYZING;
    }

    public void OnUse(List<EItem> items)
    {
        //List<EItem> aux = new List<EItem>();
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.getItemAmount(items[i]) > 0)
            //inventory.removeItem(items[i]);
            {
                inventory.removeItem(items[i]);
            }
            GameObject trap = Instantiate(input.objects[1], new Vector3(playerTransform.position.x,playerTransform.position.y,playerTransform.position.z) , Quaternion.identity);

        }
 /*       for(int i = 0; i < aux.Count; i++)
        {
            inventory.removeItem(aux[i]);
        }
        //necesito guardarla en lista auxiliar y borrar esa lista
  */}



}
