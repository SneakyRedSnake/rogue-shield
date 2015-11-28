using UnityEngine;
interface IDamageDealer {
	
	float getDamage(Collision2D col);
	Vector2 getForce(Collision2D col);
}
