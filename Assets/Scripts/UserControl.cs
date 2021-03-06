﻿using UnityEngine;

/// <summary>
/// 	Component to control a gameobject
/// </summary>
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Jump))]
public class UserControl : MonoBehaviour 
{
	private Movement movePlayer;		//The Move component we use for the player
	private Jump jumpPlayer;		//The jump component we use for the player

	/// <summary>
	/// 	Get the basics components the player need
	/// </summary>
	void Awake()
	{
		movePlayer = GetComponent<Movement>();
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
		bool beginJ = Input.GetButtonDown("Jump");
		bool releaseJ = Input.GetButtonUp("Jump");
		// Pass the parameter to the Move script.
		movePlayer.Move(h);

		if (beginJ) {
			jumpPlayer.triggerJump();
		}
		if (releaseJ) {
			jumpPlayer.endJump();
		}
	}
}
