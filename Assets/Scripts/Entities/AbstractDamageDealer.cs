using UnityEngine;


public abstract class AbstractDamageDealer : MonoBehaviour, IDamageDealer {
	
	public abstract float getDamage(Collision2D col);
	public abstract Vector2 getForce(Collision2D col);
}
