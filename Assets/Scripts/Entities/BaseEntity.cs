using UnityEngine;
using System.Collections;

/// <summary>
/// 	The base of any entity whose gonna act (monsters, hero, etc...)
/// 	We have to know if they are on the ground, if they hit the ceiling
/// 	or a wall, etc...
/// </summary>
public class BaseEntity : MonoBehaviour {

	//the Collider2D of the entity
	private Collider2D collids;

	//What is ground for the entity
	[SerializeField]LayerMask whatIsGround;
	//the radius of the circle where we will check if we have the ground
	[SerializeField]float isGroundedCircleRadius = 0.1f;
	//the bool to know if we are on the ground or not
	private bool grounded;
	// Use this for initialization
	void Start () {
		collids = GetComponent<Collider2D> ();
		this.grounded = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position = transform.position;
		position.y = collids.bounds.min.y + 0.1f;
		Debug.DrawRay (position, -Vector2.up * isGroundedCircleRadius);
		this.grounded = Physics2D.CircleCast (position, isGroundedCircleRadius, -Vector2.up, isGroundedCircleRadius, whatIsGround.value);
	}

	public bool IsGrounded() {
		return this.grounded;
	}
}
