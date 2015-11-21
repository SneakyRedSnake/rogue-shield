using UnityEngine;
using System.Collections;

/// <summary>
/// 	Component to move the player
/// </summary>
public class Movement : MonoBehaviour 
{
	[SerializeField]
	[Range(0f,30f)] float maxSpeed = 10f;				// The fastest the game object can travel in the x axis.
	[SerializeField] float moveForce = 10f;
	
	void Awake()
	{

	}

	/// <summary>
	/// 	Update the rigidbody2D, giving a velocity to it
	/// </summary>
	void Update()
	{

	}

	/// <summary>
	/// 	do nothing
	/// </summary>
	void FixedUpdate()
	{
	}
	
	/// <summary>
	/// 	Set the move float
	/// </summary>
	/// <param name="move">the speed we want</param>
	public void Move(float move)
	{
		if (rigidbody2D.velocity.x < maxSpeed && rigidbody2D.velocity.x > -maxSpeed && move != 0) {
			rigidbody2D.AddForce (new Vector2 (move * moveForce, 0),ForceMode2D.Force);
		}
		//rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
	}
}
