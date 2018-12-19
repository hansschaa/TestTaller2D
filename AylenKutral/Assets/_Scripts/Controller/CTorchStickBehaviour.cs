using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTorchStickBehaviour : CAction
{
	public int orderNumber;
    public override void OnUse(CPlayerInput cPlayerInput)
    {
        if(!base.used)
		{
			if(cPlayerInput.cInventario.getItemAmount(EItem.BRANCH) > 0)
			{
				base.used = true;
				print("mayor a 0");
				cPlayerInput.cInventario.removeItem(EItem.BRANCH);
				Encender();
			}
		}
    }

    private void Encender()
    {
		print("Encender la antorcha");
		this.GetComponent<AudioSource>().Play();
		this.transform.GetChild(2).gameObject.SetActive(true);
		this.transform.parent.GetComponent<CStickPuzzle>().AddAnswer(orderNumber);
    }


	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MyPlayer") && GameStateManager.eGameState == EGameState.NORMAL && base.used)
            this.transform.GetChild(0).gameObject.SetActive(true);
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("MyPlayer") && GameStateManager.eGameState == EGameState.NORMAL && base.used)
            this.transform.GetChild(0).gameObject.SetActive(false);
    }


}
