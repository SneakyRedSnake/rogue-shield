using UnityEngine;

/// <summary>
/// 	Component to control a gameobject
/// </summary>
[RequireComponent(typeof(Move))]
[RequireComponent(typeof(Jump))]
public class UserControl : MonoBehaviour 
{
	private Move movePlayer;		//The Move component we use for the player
	private Jump jumpPlayer;		//The jump component we use for the player
	
	void Awake()
	{
		movePlayer = GetComponent<Move>();
		jumpPlayer = GetComponent<Jump>();
	}
	
	void Update ()
	{

	}

	/// <summary>
	/// 	Get the inputs of the controller and give the values
	/// 	to the corrects actions components
	/// </summary>
	void FixedUpdate()
	{
		// Read the inputs.
		float h = Input.GetAxis("Horizontal");
		bool j = Input.GetButton("Jump");

		// Pass the parameter to the Move script.
		movePlayer.setMove(h);
		jumpPlayer.triggerJump(j);
		
	}
}
