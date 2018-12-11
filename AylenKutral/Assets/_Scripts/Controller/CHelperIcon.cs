using UnityEngine;

public class CHelperIcon : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MyPlayer") && GameStateManager.eGameState == EGameState.NORMAL)
            this.transform.GetChild(0).gameObject.SetActive(true);
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("MyPlayer") && GameStateManager.eGameState == EGameState.NORMAL)
            this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
