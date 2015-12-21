using UnityEngine;
using System.Collections;

/// <summary>
/// 	Component to move the player
/// </summary>
public class Movement : MonoBehaviour 
{
	[SerializeField]
	[Range(0f,30f)] float maxSpeed = 10f;				// The fastest the game object can travel in the x axis.
	[SerializeField] bool airControl = true;//whether the player can be controlled while in the air
	[SerializeField] float accelerationSpeed = 100;//speed of acceleration
	[SerializeField] float knockBackInactionTime = 0.2f;//time while we cannot move after a knockback
	bool isKnocked = false;

	private BaseEntity baseEntity;

	void Awake()
	{
		baseEntity = GetComponent<BaseEntity> ();
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
		if (!isKnocked) {//we can only move if we are not knocked back
			if (baseEntity.isGrounded) {
				move *= maxSpeed;

			} else {
				move = rigidbody2D.velocity.x + Time.deltaTime * maxSpeed * (airControl ? move : 1);
				if (Mathf.Abs (move) > maxSpeed)
					move = maxSpeed * Mathf.Sign (move);
			}
			float speedX = rigidbody2D.velocity.x + move * accelerationSpeed * Time.deltaTime;
			if (Mathf.Abs (speedX) > maxSpeed)
				speedX = maxSpeed * Mathf.Sign (speedX);
			rigidbody2D.velocity = new Vector2 (speedX, rigidbody2D.velocity.y);
		}
	}

	public void KnockBack() {
		StartCoroutine (KnockBackRoutine ());

	}

	private IEnumerator KnockBackRoutine() {
		
		isKnocked = true;

		yield return new WaitForSeconds (knockBackInactionTime);
		
		isKnocked = false;

	}
}
