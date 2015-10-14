using UnityEngine;
using System.Collections;

/// <summary>
/// 	Component to move the player
/// </summary>
public class Move : MonoBehaviour 
{
	[SerializeField]
	[Range(0f,30f)] float maxSpeed = 10f;				// The fastest the game object can travel in the x axis.

	private float move;									// The speed we want
	
	void Awake()
	{
		move = 0f;
	}

	/// <summary>
	/// 	Update the rigidbody2D, giving a velocity to it
	/// </summary>
	void Update()
	{
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
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
	public void setMove(float move)
	{
		this.move = move;
	}
}
