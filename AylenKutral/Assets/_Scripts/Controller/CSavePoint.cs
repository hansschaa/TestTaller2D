using UnityEngine;

public class CSavePoint : MonoBehaviour 
{
	public int id;
	[HideInInspector] public Vector3 position;

	void Awake()
	{
		this.position = this.transform.position;
	}
}
