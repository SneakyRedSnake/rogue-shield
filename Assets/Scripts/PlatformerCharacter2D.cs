using UnityEngine;
using System.Collections;

public class PlatformerCharacter2D : MonoBehaviour 
{
	[SerializeField] float maxSpeed = 10f;				// The fastest the player can travel in the x axis.
	
	void Awake()
	{

	}
	
	void Update()
	{

	}
	
	void FixedUpdate()
	{

	}
	
	
	public void Move(float move)
	{
		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
	}
}
