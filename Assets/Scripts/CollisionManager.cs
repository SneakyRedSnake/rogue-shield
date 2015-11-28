using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {
	public delegate void CollideAction(GameObject collider,Collision2D collision);
	public static event CollideAction onCollided;


	void OnCollisionEnter2D(Collision2D collision){
		Debug.Log ("Event manager detects collision");
		if (onCollided != null) {
			onCollided(collision.gameObject,collision);
		}
	}
}
