﻿using UnityEngine;

/// <summary>
/// 	Component to control a gameobject
/// </summary>
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Jump))]
[RequireComponent(typeof(BaseEntity))]
public class UserControl : MonoBehaviour 
{
	private Movement movePlayer;							//The Move component we use for the player
	private Jump jumpPlayer;								//The Jump component we use for the player
	private BaseEntity basePlayer;							//The BaseEntity component we use for the player
	private StatePlayer statePlayer;						//The datas about the state if the player

	[SerializeField]
	[Range(0f,1f)]private float movementReduction = 0.5f;	//the reducation of movement when the shield is activated

	/// <summary>
	/// 	Get the basics components the player need
	/// </summary>
	void Awake()
	{
		movePlayer = GetComponent<Movement>();
		jumpPlayer = GetComponent<Jump>();
		basePlayer = GetComponent<BaseEntity>();
		statePlayer = transform.root.GetComponent<StatePlayer> ();
	}

	/// <summary>
	/// 	Get the inputs of the controller and give the values
	/// 	to the corrects actions components
	/// </summary>
	void Update ()
	{
		
		bool beginJ = Input.GetButtonDown("Jump");
		bool releaseJ = Input.GetButtonUp("Jump");
		if (beginJ) {
			jumpPlayer.triggerJump();
		}
		if (releaseJ) {
			jumpPlayer.endJump();
		}
	}

	/// <summary>
	/// 	Get the inputs of the controller and give the values
	/// 	to the corrects actions components
	/// </summary>
	void FixedUpdate()
	{
		// Read the inputs.
		float h = Input.GetAxis("Horizontal");

		if (statePlayer.shielded == StatePlayer.StateShield.Shield && basePlayer.isGrounded) {
			h *= movementReduction;
		}
		// Pass the parameter to the Move script.
		movePlayer.Move(h);
	
	}
}
