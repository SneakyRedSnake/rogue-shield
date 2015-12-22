using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collisionInfo){
		Debug.Log ("in");
		Debug.Log ("velocity : " + collisionInfo.relativeVelocity);
		Vector2 normal = collisionInfo.relativeVelocity;
		normal.Normalize ();
		Debug.Log ("normal : " + normal);
		Vector3 bounce = Vector3.Reflect (new Vector3(normal.x, normal.y), 
		                                  new Vector3(collisionInfo.contacts [0].normal.x,collisionInfo.contacts [0].normal.y )) * collisionInfo.relativeVelocity.magnitude;
		Debug.Log ("bounce : " + bounce);
		gameObject.rigidbody2D.AddForce(new Vector2(bounce.x, bounce.y)*10, ForceMode2D.Impulse);
	}

}
