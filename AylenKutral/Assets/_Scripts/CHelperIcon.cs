using UnityEngine;

public class CHelperIcon : MonoBehaviour 
{
	void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("MyPlayer"))
            this.transform.GetChild(0).gameObject.SetActive(true);
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("MyPlayer"))
            this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
