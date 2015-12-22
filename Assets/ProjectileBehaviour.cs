using UnityEngine;
using System.Collections;

public class ProjectileBehaviour : MonoBehaviour {
	[SerializeField]float maxVelocity = 30f;
	[SerializeField]LayerMask onWhatDestroy;

	private bool toDestroy = false;

	void Start(){
		Destroy (gameObject, 10);
	}

	// Update is called once per frame
	void Update () {
		if (toDestroy) {
			Destroy(this.gameObject, Time.deltaTime);
		}
		if (gameObject.rigidbody2D.velocity.magnitude > maxVelocity) {
			Debug.Log ("too much speed");
			Vector2 newVelocity = new Vector2(gameObject.rigidbody2D.velocity.x, gameObject.rigidbody2D.velocity.y);
			newVelocity.Normalize();
			gameObject.rigidbody2D.velocity = newVelocity * maxVelocity;
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if ((collision.gameObject.layer & ~onWhatDestroy) > 0) {
			toDestroy = true;
		}
	}
}
