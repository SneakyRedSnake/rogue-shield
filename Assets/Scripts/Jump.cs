using UnityEngine;
using System.Collections;

/// <summary>
/// 	The Jump component permits to the GameObject
/// 	to Jump
/// </summary>
public class Jump : MonoBehaviour
{
	private bool jump;											// A boolean to know if we want to jump
	private IEnumerator currentJump;							//the current jump coroutine
	[SerializeField] float jumpForce = 9f;						// Amount of force added when the gameobject jumps.	
	[SerializeField]
	[Range(0f,5f)]float maxJumpDuration = 2f;					// The max duration of a jump
	[SerializeField]
	[Range(0f,5f)]float minJumpDuration = 0.1f;					// The min duration of a jump
	
	[SerializeField] LayerMask whatIsGround;					// A mask determining what is ground to the game object
	private Transform groundCheck;								// A position marking where to check if the game object is grounded.
	private float groundedRadius = .000001f;						// Radius of the overlap circle to determine if grounded
	private bool grounded = false;								// Whether or not the game object is grounded.


	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("GroundCheck");
		//at the beginning we don't want to jump
		jump = false;
	}

	/// <summary>
	///		check if we are currently grounded
	/// </summary>
	void FixedUpdate()
	{
		//the game object is grounded if we have the ground in a circle of groundedRadius radius at the bottom of it
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
	}

	/// <summary>
	///		we jump if we triggered it and if we are grounded
	/// </summary>
	void Update()
	{
		if (grounded && jump) {
			//launch the jump
			if(currentJump != null){
				StopCoroutine(currentJump);
			}
			Debug.Log ("Launch jump");
			currentJump = JumpCoroutine ();
			StartCoroutine (currentJump);
		} else if (grounded && currentJump != null) {
			//StopCoroutine(currentJump);
		}
	}
	
	
	/// <summary>
	/// 	Trigger the jump
	/// </summary>
	/// <param name="jump">if we want to jump</param>
	public void triggerJump(bool jump)
	{
		this.jump = jump;
	}

	/// <summary>
	/// 	The Jump coroutine (so, the jump itself)
	/// </summary>
	/// <returns>The coroutine</returns>
	private IEnumerator JumpCoroutine() {
		float time = 0;
		//We jump while we want and while we can
		while( (time < minJumpDuration) || (jump && time < maxJumpDuration)) {
			float proportion = time/maxJumpDuration;
			Vector2 jumpStrength = new Vector2(0f,jumpForce);
			jumpStrength = Vector2.Lerp(jumpStrength,Vector2.zero,proportion);
			rigidbody2D.AddForce(jumpStrength);
			time += Time.deltaTime;
			yield return null;
		}
		Debug.Log ("FIN==========================");
		//we don't want to jump anymore
		jump = false;
		currentJump = null;
	}
}

