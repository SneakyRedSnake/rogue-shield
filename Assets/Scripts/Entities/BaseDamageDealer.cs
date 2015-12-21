using UnityEngine;
using System.Collections;

public class BaseDamageDealer : AbstractDamageDealer {
	[SerializeField] float force = 10000;
	[SerializeField] float damages = 100;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override float getDamage(Collision2D col){
		return damages;
	}
	
	//Gives a vector representing the knockback force of the damage dealer
	//you can change that so the force is different depending on the axis of the force
	public override Vector2 getForce(Collision2D col){
		return new Vector2(force,force);
	}
}
