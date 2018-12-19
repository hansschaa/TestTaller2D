using UnityEngine;
using UnityEngine.UI;
public class CSpirit : MonoBehaviour 
{
	public ESpirit eSpirit;


    [Header ("UI")]
    public Image spiritUIImage;
    public float ccAbility;


	[Header ("Spirit Movement")]
    public float velocityMovement;
	public Transform playerSpiritPosition;
    public CPlayerController cPlayerController;
    public bool isFliped;


    void Update()
    {
        if(spiritUIImage.fillAmount < 1)
        {
            spiritUIImage.fillAmount += ccAbility;

            if(spiritUIImage.fillAmount == 1)
                spiritUIImage.transform.parent.transform.localScale *= 1.4f;
            
        }
    }

	void FixedUpdate () 
    {
        
         //Animation when the distance is more than 1 and the Spiriit need getting closer to player
        if(Mathf.Abs(Vector2.Distance(playerSpiritPosition.position,this.transform.position)) > 1f )
        {
            transform.position = Vector3.Lerp(transform.position,new Vector3(playerSpiritPosition.position.x,playerSpiritPosition.position.y + Mathf.Sin(Time.time), 0.0f), velocityMovement*Time.deltaTime);
            
            if(isFliped)
                isFliped=false;
            
        }

        //Animation when the distance is less than 1
        else
        {
            transform.position = Vector3.Lerp(transform.position,new Vector3(transform.position.x,playerSpiritPosition.position.y + Mathf.Sin(Time.time), 0.0f), velocityMovement*Time.deltaTime);
            
            if(!isFliped)
            {
                isFliped = true;
                if(cPlayerController.m_FacingRight)
                    this.GetComponent<SpriteRenderer>().flipX = true;
         

                else
                    this.GetComponent<SpriteRenderer>().flipX = false;
  
            }
        } 
    }


	
}
