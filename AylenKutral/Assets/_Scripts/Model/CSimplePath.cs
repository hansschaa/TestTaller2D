using DragonBones;
using System.Collections;
using UnityEngine;

public class CSimplePath : MonoBehaviour
{
    // Array of waypoints to walk from one to the next one
    [SerializeField]
    UnityEngine.Transform[] waypoints;

                UnityEngine.Transform[] aux;

    // Walk speed that can be set in Inspector
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float delayMovement;

    // Index of current waypoint from which Enemy walks
    // to the next one
    private int waypointIndex = 0;

    //true is right
    //left is left
    [SerializeField]
    private bool dir;

    public UnityArmatureComponent unityArmatureComponent;

    // Use this for initialization
    private void Start()
    {
        // Set position of Enemy as position of the first waypoint
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // Move Enemy
        Move();
    }

    // Method that actually make Enemy walk
    private void Move()
    {
        //print("0");
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               moveSpeed * Time.deltaTime);

            //print("1");

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (Vector3.Distance(transform.position , waypoints[waypointIndex].transform.position) <= .5f)
            {
                //print("2");

                StartCoroutine(ToMove());
                print(waypointIndex);
                //print(waypointIndex + " Actual " + (waypoints[waypointIndex+1].transform.position.x - waypoints[waypointIndex].transform.position.x) + " xNext ");

                if (waypointIndex == waypoints.Length - 1) waypointIndex = 0;
                else waypointIndex++;

                //if(dir) StartCoroutine(ToLeft());
                //if(!dir) StartCoroutine(ToRight());
                /*
                if (waypointIndex == 0)
                {
                    if (waypoints[0].transform.position.x > waypoints[waypoints.Length - 1].transform.position.x)
                    {
                        StartCoroutine(ToRight());
                    } else if (waypoints[0].transform.position.x < waypoints[waypoints.Length - 1].transform.position.x)
                    {
                        StartCoroutine(ToLeft());
                    }
                }
                else if (waypointIndex == waypoints.Length - 1)
                {
                    if (waypoints[waypoints.Length - 1].transform.position.x > waypoints[waypoints.Length - 2].transform.position.x)
                    {
                        StartCoroutine(ToRight());
                    }
                    else if (waypoints[waypoints.Length - 1].transform.position.x < waypoints[waypoints.Length - 2].transform.position.x)
                    {
                        StartCoroutine(ToLeft());
                    }
                }
                else if(waypoints[waypointIndex+1].transform.position.x > waypoints[waypointIndex].transform.position.x)
                {
                    StartCoroutine(ToLeft());
                }
                else if (waypoints[waypointIndex + 1].transform.position.x < waypoints[waypointIndex].transform.position.x)
                {
                    StartCoroutine(ToRight());
                }
                */

                /*if (dir = true && waypoints[waypointIndex].transform.position.x > waypoints[waypointIndex-1].transform.position.x)
                {
                    StartCoroutine (DelayMovement());
                    dir = false;
                }

                if (dir = false && waypoints[waypointIndex].transform.position.x > waypoints[waypointIndex - 1].transform.position.x)
                {
                    StartCoroutine(DelayMovement());
                    dir = true;
                }*/

                /*if(waypointIndex == waypoints.Length)
                {
                    if (dir = true && waypoints[0].transform.position.x > waypoints[waypointIndex].transform.position.x)
                    {
                        //StartCoroutine (DelayMovement());
                        dir = false;
                    }else
                    if (dir = false && waypoints[0].transform.position.x < waypoints[waypointIndex].transform.position.x)
                    {
                        //StartCoroutine(DelayMovement());
                        dir = true;
                    }
                    waypointIndex = 0;
                }
                else 
                if (dir = true && waypoints[waypointIndex+1].transform.position.x > waypoints[waypointIndex].transform.position.x)
                {
                    //StartCoroutine (DelayMovement());
                    dir = false;
                }else 
                if (dir = false && waypoints[waypointIndex+1].transform.position.x < waypoints[waypointIndex].transform.position.x)
                {
                    //StartCoroutine(DelayMovement());
                    dir = true;
                }*/

                //if ()StartCoroutine (DelayMovement());
                //if(waypointIndex == 2) waypointIndex = 0;

            }
        }
    }
    /*
    IEnumerator ToLeft()
    {
        print("starting");
        moveSpeed = 0;
        yield return new WaitForSeconds(delayMovement);
        //if (waypoints[waypointIndex].transform.position.x > waypoints[waypointIndex - 1].transform.position.x)
        //{
        unityArmatureComponent.armature.flipX = false;
        //if(!dir) unityArmatureComponent.armature.flipX = false;
        //unityArmatureComponent.armature.flipX = !unityArmatureComponent.armature.flipX;
        
        //}
        moveSpeed = 2f;
        print("in delay");
    }
    IEnumerator ToRight()
    {
        print("starting");
        moveSpeed = 0;
        yield return new WaitForSeconds(delayMovement);
        //if (waypoints[waypointIndex].transform.position.x > waypoints[waypointIndex - 1].transform.position.x)
        //{
        unityArmatureComponent.armature.flipX = true;
        //if(!dir) unityArmatureComponent.armature.flipX = false;
        //unityArmatureComponent.armature.flipX = !unityArmatureComponent.armature.flipX;

        //}
        moveSpeed = 2f;
        print("in delay");
    }*/

    IEnumerator ToMove()
    {
        moveSpeed = 0;
        yield return new WaitForSeconds(delayMovement);
        if (waypointIndex == waypoints.Length-1)
        {
            if (waypoints[0].transform.position.x > waypoints[waypoints.Length - 1].transform.position.x)
            {
                unityArmatureComponent.armature.flipX = true;
            }
            else if (waypoints[0].transform.position.x < waypoints[waypoints.Length - 1].transform.position.x)
            {
                unityArmatureComponent.armature.flipX = false;
            }
        }
        else if (waypoints[waypointIndex + 1].transform.position.x > waypoints[waypointIndex].transform.position.x)
        {
            unityArmatureComponent.armature.flipX = true;
        }
        else if (waypoints[waypointIndex + 1].transform.position.x < waypoints[waypointIndex].transform.position.x)
        {
            unityArmatureComponent.armature.flipX = false;
        }
        moveSpeed = 2f;
    }


}
