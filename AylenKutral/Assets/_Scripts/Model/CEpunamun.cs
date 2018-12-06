using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEpunamun : MonoBehaviour {

    float lastTime;

    //time that enemy use to rotate in 180 degrees
    public float rotateDelay;
    //time that enemy is stunned
    public float stunnedDelay;
    //max distance that enemy can move since original point
    public float maxDist;
    //enemy velocity
    public float speedMovement;

    //bool to recognize if linecast is collision with player
    public bool target = false;
    //direction of Epunamun
    public bool dir = false;
    //bool to recognize if Epunamun is retorning to initial position
    bool returning = false;
    //bool to recognize stunned state
    bool stunned = false;

    //init and final points of LineCast
    public Transform start;
    public Transform end;

    //player Transform
    public Transform player;

    //enemy expressions
    public SpriteRenderer warning;
    public SpriteRenderer lost;
    public SpriteRenderer blind;

    //Components
    private Rigidbody2D rb;
    private Collider2D c2D;

    //aux transform to save initial enemy position
    private Transform aux;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        c2D = this.gameObject.GetComponent<Collider2D>();
        warning.enabled = false;
        lost.enabled = false;
        blind.enabled = false;
    }

    // Use this for initialization
    void Start() {
        aux = new GameObject().transform;
        lastTime = Time.time;
    }

    // Update is called once per frame
    void Update() {
        Raycasting();
        Behaviours();
    }

    public void Raycasting()
    {
        Debug.DrawLine(start.position, end.position, Color.red);
        target = Physics2D.Linecast(start.position, end.position, 1 << LayerMask.NameToLayer("Player"));
    }

    public void Behaviours()
    {
        if (stunned)
        {
            rb.velocity = Vector3.zero;
            lost.enabled = false;
            warning.enabled = false;
        }
        else
        if (!target)
        {
            lost.enabled = true;
            warning.enabled = false;
            if (returning)
            {
                if (Vector3.Distance(new Vector3(transform.position.x, 0f), new Vector3(aux.position.x, 0f)) <= 0.5f) returning = false;
                if (dir)
                {
                    rb.velocity = new Vector3(-speedMovement, 0, 0);
                }
                else
                {
                    rb.velocity = new Vector3(speedMovement, 0, 0);
                }
            }
            else
            if (Time.time - lastTime >= rotateDelay)
            {
                dir = !dir;
                lastTime = Time.time;
                if (dir)
                    transform.eulerAngles = new Vector2(0f, 0f);
                else transform.eulerAngles = new Vector2(0f, 180f);
            }
        }
        else
        {
            //move to left
            if (Vector3.Distance(transform.position, aux.position) <= maxDist && dir)
            {
                warning.enabled = true;
                lost.enabled = false;

                rb.velocity = new Vector3(-speedMovement, 0, 0);
            }
            //move to right
            else if (Vector3.Distance(new Vector3(transform.position.x, 0f), new Vector3(aux.position.x, 0f)) >= maxDist && !dir)
            {
                warning.enabled = true;
                lost.enabled = false;
                rb.velocity = new Vector3(speedMovement, 0, 0);
            }
            //return initial pos
            else if (Vector3.Distance(new Vector3(transform.position.x, 0f), new Vector3(aux.position.x, 0f)) >= maxDist)
            {
                dir = !dir;
                returning = true;
                if (dir)
                    transform.eulerAngles = new Vector2(0f, 0f);
                else transform.eulerAngles = new Vector2(0f, 180f);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //cambia a estado de stun y deja pasar al target
        //aqui debiera haber una animacion de stuneado
        if (collision.gameObject.tag == "MyPlayer")
        {
            StartCoroutine(DesactiveCollider());
        }else if(collision.gameObject.tag == "Rock")
        {
            //aca deberia ir la animacion de muerte
            //y devolver al last check point
            Destroy(player.gameObject);
        }
    }


    IEnumerator DesactiveCollider()
    {
        stunned = true;
        rb.isKinematic = true;
        c2D.enabled = false;
        blind.enabled = true;
        yield return new WaitForSeconds(stunnedDelay);
        rb.isKinematic = false;
        c2D.enabled = true;
        stunned = false;
        blind.enabled = false;
        StopCoroutine(DesactiveCollider());
    }

}
