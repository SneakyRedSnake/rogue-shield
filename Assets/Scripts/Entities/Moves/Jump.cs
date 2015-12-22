using UnityEngine;
using System.Collections;

/// <summary>
/// 	The Jump component permits to the GameObject
/// 	to Jump
/// </summary>
[RequireComponent(typeof(BaseEntity))]
public class Jump : MonoBehaviour
{
	private bool jump;											// A boolean to know if we want to jump
	private IEnumerator currentJump;							//the current jump coroutine
	[SerializeField] float jumpForce = 9f;						// Amount of force added when the gameobject jumps.	
	[SerializeField]
	[Range(0f,5f)]float maxJumpDuration = 2f;					// The max duration of a jump
	[SerializeField]
	[Range(0f,5f)]float minJumpDuration = 0.1f;					// The min duration of a jump

	private BaseEntity baseEntity;

	void Start()
	{
		baseEntity = GetComponent<BaseEntity> ();
	}

	void Awake()
	{
		//at the beginning we don't want to jump
		jump = false;
	}

	/// <summary>
	///		check if we are currently grounded
	/// </summary>
	void FixedUpdate()
	{

	}

	/// <summary>
	///		we jump if we triggered it and if we are grounded
	/// </summary>
	void Update()
	{

	}
	
	
	/// <summary>
	/// 	Trigger the jump
	/// </summary>
	/// 
	public void triggerJump()
	{
		if (baseEntity.IsGrounded()) {
			currentJump = JumpCoroutine();
			StartCoroutine (currentJump);
		}
	}

	public void endJump(){
		jump = false;
	}

	/// <summary>
	/// 	The Jump coroutine (so, the jump itself)
	/// </summary>
	/// <returns>The coroutine</returns>
	private IEnumerator JumpCoroutine() {
		jump = true;
		float time = 0;

		// First strong impulsion
		rigidbody2D.AddForce (new Vector2 (0f, jumpForce), ForceMode2D.Impulse);

		// Slow progression while pressing jump
		while( (time < minJumpDuration) || (jump && time < maxJumpDuration)) {
			float proportion = time/maxJumpDuration;
			Vector2 jumpStrength = new Vector2(0f,jumpForce);
			jumpStrength = Vector2.Lerp(jumpStrength,Vector2.zero,proportion);
			rigidbody2D.AddForce(jumpStrength, ForceMode2D.Impulse);
			time += Time.deltaTime;
			yield return null;
		}
		//we don't want to jump anymore
		jump = false;
		currentJump = null;
	}
}

