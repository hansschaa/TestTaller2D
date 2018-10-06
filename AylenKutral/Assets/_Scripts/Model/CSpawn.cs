using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSpawn : MonoBehaviour {

    public GameObject item;
    private Transform player;

	// Use this for initialization
	private void Start () {
        //Find player´s transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnDroppedItem()
    {
        Vector2 playerPos = new Vector2(player.position.x + 5, player.position.y);
        Instantiate(item, player.position, Quaternion.identity);
    }

}
