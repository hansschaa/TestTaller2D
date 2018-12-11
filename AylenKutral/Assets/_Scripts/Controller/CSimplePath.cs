using DragonBones;
using System.Collections;
using UnityEngine;

public class CSimplePath : MonoBehaviour
{
   
    public Vector3[] waypoints;
    public float moveSpeed;
    public float delayMovement;


    private int waypointIndex = 0;


    public UnityArmatureComponent unityArmatureComponent;

    public bool m_FacingLeft;
    public bool inDelay;


    private void Start()
    {
        transform.position = waypoints[waypointIndex];
        waypointIndex++;
        inDelay = false;
    }

    private void Update()
    {
        if(!inDelay)
            Move();
    }

    private void Move()
    {

        if (Mathf.Abs(Vector3.Distance(transform.position , waypoints[waypointIndex])) > .5f)
            transform.position = Vector2.MoveTowards(transform.position,waypoints[waypointIndex],moveSpeed * Time.deltaTime); 
        

        //De lo contrario ... llegó
        else
        {
            if(waypointIndex+1 == waypoints.Length)
            {
                if(waypoints[waypointIndex].x < waypoints[0].x )
                {
                    m_FacingLeft = false;
                }

                else
                {
                    m_FacingLeft = true;
                }
            }

            else
            {
                if(waypoints[waypointIndex].x < waypoints[waypointIndex+1].x )
                {
                    m_FacingLeft = false;
                }

                else
                {
                    m_FacingLeft = true;
                }

            }

            

            

            StartCoroutine(Wait(delayMovement));
            unityArmatureComponent.armature.flipX = !m_FacingLeft;

            if (waypointIndex == waypoints.Length - 1) 
                waypointIndex = 0;

            else 
                waypointIndex++;

            
        }

    }

    IEnumerator Wait(float seconds)
    {
        inDelay = true;
        yield return new WaitForSeconds(seconds);
        inDelay = false;
    }
}
