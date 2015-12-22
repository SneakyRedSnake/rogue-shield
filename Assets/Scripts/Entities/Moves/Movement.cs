using UnityEngine;
using System.Collections;

/// <summary>
/// 	Component to move the player
/// </summary>
public class Movement : MonoBehaviour 
{
	[SerializeField]
	[Range(0f,30f)] float maxSpeed = 10f;				// The fastest the game object can travel in the x axis.
	[SerializeField] float airControl = 1f;//whether the player can be controlled while in the air
	[SerializeField] float accelerationSpeed = 100;//speed of acceleration
	[SerializeField] float knockBackInactionTime = 0.2f;//time while we cannot move after a knockback
	[SerializeField] float airControlAccelerationFactor = 10f;//speed of move changement while in the air
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
		//Debug.Log ("Moving : " + move + " while grounded = " + baseEntity.IsGrounded ());
		if (!isKnocked) {//we can only move if we are not knocked back
			if (!baseEntity.IsGrounded()) {

				//Debug.Log("Moving by : " + baseEntity.rigidbody2D.velocity.x + "  Adding : " + move * maxSpeed);
				move = move * airControl;
				baseEntity.rigidbody2D.velocity = new Vector2(move * maxSpeed, baseEntity.rigidbody2D.velocity.y);
			} else {
				baseEntity.rigidbody2D.velocity = new Vector2(move * maxSpeed, baseEntity.rigidbody2D.velocity.y);
			}

			if (Mathf.Abs(baseEntity.rigidbody2D.velocity.x) > maxSpeed) {
				Debug.Log("Floor !");
				baseEntity.rigidbody2D.velocity = new Vector2(move * maxSpeed, baseEntity.rigidbody2D.velocity.y);
			}
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
