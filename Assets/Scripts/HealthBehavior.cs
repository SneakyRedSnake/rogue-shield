using UnityEngine;
using System.Collections;
/**
 * 
 * Script to use on a entity with health
 * 
 * */

public class HealthBehavior : MonoBehaviour {
	[SerializeField] protected float maxHealth;// maximum health of the entity
	protected float health;// current health
	protected AbstractKillable killable;// the killable component to send it the death signal
	[SerializeField] private float recoveryTime = 0.5f;//time of recovery between each damage taken
	protected bool recovery = false;// recovery mode boolean

	public void Start(){
		health = maxHealth;
		killable = GetComponent<AbstractKillable> ();

	}

	void OnCollisionStay2D(Collision2D coll) {
		//we get the damage dealer of the collider
		AbstractDamageDealer dealer = coll.gameObject.GetComponentInParent<AbstractDamageDealer> ();
	
		if (dealer && !recovery) {//if the collider is a damage dealer we act
			OnDamageTaken();//call abstract signal for damage taken
			health -= dealer.getDamage(coll);//we take the damage
			// we calculate the vector between our center and the damage dealer's center
			Vector3 pos = new Vector2(transform.position.x,transform.position.y);
			pos = coll.gameObject.transform.position - gameObject.transform.position;
			Vector2 forcePos = new Vector2(-pos.x,-pos.y);
			forcePos.Normalize();
			Vector2 force;
			KnockBack();// signal to the health behaviour we receive a knockback effect
			//we decide the direction of the force to apply
			if(Mathf.Abs(forcePos.x)  > Mathf.Abs(forcePos.y) || forcePos.y < 0)
				force = new Vector2(dealer.getForce(coll).x * Mathf.Sign(forcePos.x),0f);
			else
				force = new Vector2(0f,dealer.getForce(coll).y * Mathf.Sign(forcePos.y));
			rigidbody2D.AddForce(force);
			if(recoveryTime > 0){//we start the recovery coroutine
				StartCoroutine(Recovery());
			}
		}
		if (health <= 0 && killable) {
			killable.kill();
		}

	}

	IEnumerator Recovery(){
		recovery = true;
		yield return new WaitForSeconds(recoveryTime);
		recovery = false;
	}

	public	virtual void OnDamageTaken() {

	}

	public virtual void KnockBack() {

	}


}
