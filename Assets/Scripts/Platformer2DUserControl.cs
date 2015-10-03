using UnityEngine;

[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour 
{
	private PlatformerCharacter2D character;
	
	void Awake()
	{
		character = GetComponent<PlatformerCharacter2D>();
	}
	
	void Update ()
	{

	}
	
	void FixedUpdate()
	{
		// Read the inputs.
		float h = Input.GetAxis("Horizontal");
		
		// Pass all parameters to the character control script.
		character.Move(h);
		
	}
}
