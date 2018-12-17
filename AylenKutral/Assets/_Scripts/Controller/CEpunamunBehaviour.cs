using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CEpunamunBehaviour : MonoBehaviour 
{
	private EEpunamunState _eEpunamunState;
	private EEpunamunState _lastEpunamunState;
	private float _lastTime;


	[Header("Effects")]
	private Coroutine stunnedCoroutine;
	public float stunnedTime;
	private _2dxFX_Lightning stunnedVisualEffect;


	[Header ("Movement parameters")]
	public float walkVelocity;
	public float runVelocity;
	private int _direction; 
	public float timeToPatrol;
	public float timeToIdle;
	private Rigidbody2D _rb;
	private SpriteRenderer _childSpriteRenderer;
	private Animator _childAnimator;
	


	[Header ("Follow Player Parameters")]
	public bool _target;
	public Transform playerTransform;
	public float rayLength;
	public float xInitialBeforeFollow;


	// Use this for initialization
	void Start () 
	{
		_target = false;
		_childSpriteRenderer = this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
		_childAnimator = this.transform.GetChild(0).gameObject.GetComponent<Animator>();
		stunnedVisualEffect = this.transform.GetChild(0).gameObject.GetComponent<_2dxFX_Lightning>();
		_rb = this.GetComponent<Rigidbody2D>();
		xInitialBeforeFollow = transform.position.x;

		_eEpunamunState = EEpunamunState.PATROL;
	

		// 1 Para mirar a la derecha
		// -1 Para mirar a la izquierda
		_direction = 1;
		_childAnimator.SetTrigger("Walk");
		
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(_eEpunamunState != EEpunamunState.STUNED)
		{
			_target = Physics2D.Linecast(this.transform.position, 
            new Vector3(transform.position.x  + (rayLength * _direction), transform.position.y, transform.position.z), 
        1 << LayerMask.NameToLayer("Player"));

			if(_target && _eEpunamunState != EEpunamunState.FOLLOWPLAYER)
			{
				//print("Cambiando a Run");
				_childAnimator.SetTrigger("Run");
				
				_eEpunamunState = EEpunamunState.FOLLOWPLAYER;
			}

			//Volver a mi x inicial
			else if(!_target && _eEpunamunState == EEpunamunState.FOLLOWPLAYER)
			{
				//print("Cambiando a Return");
				_childAnimator.SetTrigger("Walk");
				ChangeDirection();
				_eEpunamunState = EEpunamunState.RETURN;
			}

			#region "States"
			if(_eEpunamunState== EEpunamunState.PATROL)
			{
				
				_rb.velocity = new Vector2(walkVelocity * _direction,0);


				if (_lastTime > timeToPatrol)
				{
					//print("Cambiando a Idle");
					_childAnimator.SetTrigger("Idle");
					_lastTime = 0;
					_eEpunamunState = EEpunamunState.IDLE;
				}
			}

			else if(_eEpunamunState== EEpunamunState.IDLE)
			{
				_rb.velocity = new Vector2(0 ,0);

				if (_lastTime > timeToIdle)
				{
					//print("Cambiando a Walk");
					_childAnimator.SetTrigger("Walk");
					_lastTime = 0;
					_eEpunamunState = EEpunamunState.PATROL;

					ChangeDirection();
				}
				
			}
			

			//Folloy Player
			else if(_eEpunamunState == EEpunamunState.FOLLOWPLAYER)
			{
				
				
				if(playerTransform.transform.position.x < this.transform.position.x)
				{
					_rb.velocity = new Vector2(-runVelocity,0);
					_direction = -1;
				}

				else
				{
					_rb.velocity = new Vector2(runVelocity,0);
					_direction = 1;
				}
			}

			//Return to initial point
			else if(_eEpunamunState == EEpunamunState.RETURN)
			{
				//RETURN
				if(this.transform.position.x < xInitialBeforeFollow)
				{
					_rb.velocity = new Vector2(walkVelocity,0);
				}

				else if(this.transform.position.x > xInitialBeforeFollow)
				{
					_rb.velocity = new Vector2(-walkVelocity,0);
				}

				if(Mathf.Abs(this.transform.position.x - xInitialBeforeFollow) < 0.1f)
				{
					//print("He retornado a mi posicion initial");
					_lastTime = 0;
					_eEpunamunState = EEpunamunState.PATROL;
				}
			}

			#endregion


			
			_lastTime += UnityEngine.Time.deltaTime;

		}
		

		//Debug.DrawLine(this.transform.position, new Vector3(transform.position.x + (rayLength * _direction), transform.position.y, transform.position.z), Color.red);

		//Target por primera vez
		
	}



	public void ChangeDirection()
	{
		//Está mirando para la izquierda
		if(_direction == -1)
		{
			_direction = 1;
			_childSpriteRenderer.flipX = false;
		}

		//Está mirando para la Derecha
		else if(_direction == 1)
		{
			_direction = -1;
			_childSpriteRenderer.flipX = true;
		}

	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			//print("Cambiando a Return");
			_childAnimator.SetTrigger("Walk");
			ChangeDirection();
			_eEpunamunState = EEpunamunState.RETURN;
			transform.position = new Vector3(xInitialBeforeFollow,-51,-1);
		}	
	}

	public void Stunned()
	{
		if(_eEpunamunState != EEpunamunState.STUNED)
		{
			_lastEpunamunState = _eEpunamunState;
			_eEpunamunState = EEpunamunState.STUNED;
			_rb.velocity = Vector2.zero;
			stunnedCoroutine = StartCoroutine(StunnedEffect());
			print("Coroutina inicializada");
		}
		

	}

    IEnumerator StunnedEffect()
    {
		
		_childAnimator.speed = 0;
		stunnedVisualEffect.enabled = true;
		_rb.constraints = RigidbodyConstraints2D.FreezePositionY;
		this.GetComponent<Collider2D>().enabled = false;

		yield return new WaitForSeconds(stunnedTime);

		_rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		this.GetComponent<Collider2D>().enabled = true;
		stunnedVisualEffect.enabled = false;
		_eEpunamunState = _lastEpunamunState;
		_childAnimator.speed = 1;
		
        
    }
}
