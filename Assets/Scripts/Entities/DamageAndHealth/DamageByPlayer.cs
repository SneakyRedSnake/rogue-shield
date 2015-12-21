using UnityEngine;
using System.Collections;

public class DamageByPlayer : AbstractDamageDealer {
	[SerializeField] float force = 10000;
	public override float getDamage(Collision2D col){
		return 100f;
	}

	public override Vector2 getForce(Collision2D col){
		return new Vector2(force,force);
	}
}
