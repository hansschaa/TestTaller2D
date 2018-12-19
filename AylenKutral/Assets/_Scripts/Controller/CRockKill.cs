using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CRockKill : MonoBehaviour {

    public float spawnDelay;

    public Transform player;
    public Transform model;
    public Transform parent;

    // Use this for initialization

    void Start () {
        StartCoroutine(Spawn());
    }
	
	// Update is called once per frame
	void Update () 
    {

        if (this.gameObject.transform.position.y <= -1000) Destroy(this.gameObject);
        //print(parent.position);
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        Instantiate(model, parent.position, Quaternion.identity).localScale = parent.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MyPlayer")
        {
            Destroy(player.gameObject);
        }
    }

}
