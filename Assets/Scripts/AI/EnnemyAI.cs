using UnityEngine;

/// <summary>
/// 	Component to control a gameobject
/// </summary>
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Jump))]
public class EnnemyAI : MonoBehaviour 
{
	[SerializeField]
	GameObject Player;

	private Movement moveEnnemy;		//The Move component we use for the player
	private Jump jumpEnnemy;		//The jump component we use for the player
	private Rigidbody2D toHunt;
	private Rigidbody2D myself;

	/// <summary>
	/// 	Get the basics components the player need
	/// </summary>
	void Awake()
	{
		moveEnnemy = GetComponent<Movement>();
		jumpEnnemy = GetComponent<Jump>();
		toHunt = Player.GetComponent<Rigidbody2D> ();
		myself = GetComponent<Rigidbody2D> ();
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
		// Get the inputs.
		float h;
		if (toHunt.position.x > myself.position.x) {
			h = 1;
		} else {
			h = -1;
		}

		bool beginJ = false;
		bool releaseJ = false;

		// Pass the parameter to the Move script.
		moveEnnemy.Move(h);
		
		if (beginJ) {
			jumpEnnemy.triggerJump();
		}
		if (releaseJ) {
			jumpEnnemy.endJump();
		}
	}
}
