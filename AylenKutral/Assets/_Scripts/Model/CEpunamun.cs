using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEpunamun : MonoBehaviour {

    float lastTime;

    public float delay;
    public float maxDist;
    public float speedMovement;

    public bool collision = false;
    public bool dir = false;

    public Transform start;
    public Transform end;
    public Transform player;
    public GameObject warning;
    private Rigidbody2D rb;

    private Transform aux;

    private void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        aux = new GameObject().transform;
        lastTime = Time.time;
        //StartCoroutine(TurnAround());
    }
	
	// Update is called once per frame
	void Update () {
        Raycasting();
        Behaviours();
	}

    
    public void Raycasting()
    {
        Debug.DrawLine(start.position, end.position,Color.red);
        collision = Physics2D.Linecast(start.position,end.position, 1 << LayerMask.NameToLayer("Player"));
    }

    public void Behaviours ()
    {
        print(collision);
        if (!collision)
        {
            if (Time.time - lastTime >= delay)
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
            //moverse a la izquierda
            if (Vector3.Distance(transform.position, aux.position) <= maxDist && dir)
            {
                rb.AddForce(new Vector3(-speedMovement,0,0));
                //espera,rota y vuelve al anterior
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 2, 0, 0), -speedMovement * Time.deltaTime);
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, 0), new Vector3(player.position.x, player.position.y, 0), speedMovement * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), speedMovement * Time.deltaTime);
            } else if(Vector3.Distance(transform.position, aux.position) <= maxDist && !dir)
            {
                rb.AddForce(new Vector3(speedMovement, 0, 0));
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 2, 0, 0), speedMovement * Time.deltaTime);
                transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, 0), player.position, speedMovement * Time.deltaTime);
                //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), speedMovement * Time.deltaTime);
            }
            else
            {
                print("retorno");
                //retorna
                //transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,0,0), new Vector3(aux.position.x,0,0), speedMovement * Time.deltaTime);
            }
        }

    }

    //gira sobre si mismo
    IEnumerator TurnAround()
    {
        dir = !dir;

        yield return new WaitForSeconds(delay);
        if (dir)
            transform.eulerAngles = new Vector2(0f, 0f);
        else transform.eulerAngles = new Vector2(0f, 180f);

    }

    IEnumerator Attack()
    {
        //moverse a la izquierda
        if (start.position.x - end.position.x >= 0f && Vector3.Distance(transform.position,aux.position) <= maxDist)
        {
            //espera,rota y vuelve al anterior
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else
        {
            //retorna
            transform.position = Vector3.MoveTowards(transform.position, aux.position, Time.deltaTime);
        }
        //moverse a la derecha
        if (start.position.x - end.position.x < 0f && Vector3.Distance(transform.position, aux.position) <= maxDist)
        {
            //espera,rota y vuelve al anterior
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else
        {
            //retorna
            transform.position = Vector3.MoveTowards(transform.position, aux.position, Time.deltaTime);
        }
        yield return new WaitForSeconds(delay);

    }

}
