using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawnRocks : MonoBehaviour 
{
	public float spawnDelay;
	public GameObject rockToSpawn;
	public Coroutine spawnCoroutine;


    // Use this for initialization

    void Start () 
	{
        spawnCoroutine = StartCoroutine(Spawn());
    }
	
    IEnumerator Spawn()
    {
        while(true)
        {
            Destroy(Instantiate(rockToSpawn, this.transform.position, Quaternion.identity),15f);
            yield return new WaitForSeconds(spawnDelay);
           

        }
        
    }

    
}
