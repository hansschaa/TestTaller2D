using Rewired;
using UnityEngine;
using Yarn.Unity;

public class NPCDialog :MonoBehaviour
{
	private bool isCollided;


	#region  "Events"
	public delegate void DialogDelegate( Vector3 position, TextAsset[] sourceText);
	//public delegate void StrengthDelegate(float amount);
	public static event DialogDelegate OnDialog;
	//public static event StrengthDelegate OnStrength;
	#endregion


	/// The JSON files to load the conversation from
    public TextAsset[] sourceText;


	[Header("Rewired Variables")]
	private int playerId;
	[HideInInspector] public Player player;

	/// <summary>
	/// Awake is called when the script instance is being loaded.
	/// </summary>
	void Awake()
	{
		this.player = ReInput.players.GetPlayer(0);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("MyPlayer"))
		{
			isCollided = true;
		}
		
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("MyPlayer"))
		{
			isCollided = false;
		}
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if(player.GetButtonDown("Action") && isCollided)
		{
			//base.StartDialogue(this.transform.position);
			if(OnDialog != null)
				OnDialog(transform.position, this.sourceText);

		}
	}
}
