using UnityEngine;
using System.Collections;

/// <summary>
/// 	Component to move the player
/// </summary>
public class Movement : MonoBehaviour 
{
	[SerializeField]
	[Range(0f,30f)] float maxSpeed = 10f;				// The fastest the game object can travel in the x axis.

	
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
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
	}
}
