using UnityEngine;
using System.Collections;

public class BaseEnemy : AbstractDamageDealer {
	[SerializeField] float force = 10000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override float getDamage(Collision2D col){
		return 100f;
	}


	public override Vector2 getForce(Collision2D col){
		return new Vector2(force,force);
	}
}
