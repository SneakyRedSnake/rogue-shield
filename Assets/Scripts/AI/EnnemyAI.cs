using UnityEngine;

/// <summary>
/// 	Component to control a gameobject
/// </summary>
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Jump))]
public class EnnemyAI : MonoBehaviour 
{
	GameObject Player;
	[SerializeField] float distanceToJump = 10;
	
	[SerializeField] float jumpOverDist = 1;
	[SerializeField] float backwardTime= 0.3f;
	[SerializeField] float distanceThreshold = 2;
	private Movement moveEnnemy;		//The Move component we use for the player
	private Jump jumpEnnemy;		//The jump component we use for the player
	private Rigidbody2D toHunt;
	private Rigidbody2D myself;
	private BaseEntity baseEntity;
	private bool backward = false;
	private float backwardTimer = 0;
	private float h;
	private bool facingRight = false;
	/// <summary>
	/// 	Get the basics components the player need
	/// </summary>
	void Awake()
	{
		Player =  GameObject.Find ("Player");
		moveEnnemy = GetComponent<Movement>();
		jumpEnnemy = GetComponent<Jump>();
		myself = GetComponent<Rigidbody2D> ();
		baseEntity = GetComponent<BaseEntity> ();
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

		if (!backward) {
			if (Player.transform.position.x > myself.position.x) {
				h = 1;
			} else {
				h = -1;
			}
			if (Mathf.Abs (Player.transform.position.x - myself.position.x) < distanceThreshold) {
				h = -Mathf.Sign (Player.transform.position.x - myself.position.x);
				backward = true;
				backwardTimer = 0f;
			}
		} else if (backwardTimer < backwardTime) {
			backwardTimer += Time.deltaTime;
		} else {
			backward = false;
		}
		
		bool beginJ = false;
		
		// Pass the parameter to the Move script.
		moveEnnemy.Move (h);
		Collider2D col = Player.GetComponent<Collider2D> ();
		if ((col.bounds.max.y+jumpOverDist) < transform.position.y){
			jumpEnnemy.endJump();
		}
		if (baseEntity.IsGrounded()) {
			jumpEnnemy.triggerJump();
		}
		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();
	}
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
