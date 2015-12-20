using UnityEngine;
using System.Collections;
/**
 * 
 * Script to use on a entity with health
 * 
 * */

public class HealthBehavior : MonoBehaviour {
	[SerializeField] protected float maxHealth;
	protected float health;
	protected AbstractKillable killable;
	[SerializeField] private float recoveryTime = 0.5f;
	protected bool recovery = false;

	public void Start(){
		health = maxHealth;
	}

	void OnCollisionStay2D(Collision2D coll) {
		AbstractDamageDealer dealer = coll.gameObject.GetComponentInParent<AbstractDamageDealer> ();

	
		if (dealer && !recovery) {
			Debug.Log ("recovery "+recovery);
			OnDamageTaken();
			health -= dealer.getDamage(coll);
			Debug.Log("health :"+health);
			Vector3 pos = new Vector2(transform.position.x,transform.position.y);
			pos = coll.gameObject.transform.position - gameObject.transform.position;
			pos.Normalize();
			Vector2 forcePos = new Vector2(pos.x,pos.y);
			Vector2 force = Vector2.Scale(dealer.getForce(coll),-forcePos);
			rigidbody2D.AddForce(force,ForceMode2D.Impulse);
			Debug.Log("force:"+force+" "+pos);
			if(recoveryTime > 0){
				StartCoroutine(Recovery());
			}
		}
		if (health <= 0 && killable) {
			killable.kill();
		}

	}

	IEnumerator Recovery(){
		recovery = true;
		
		Debug.Log ("recovery numerator");
		yield return new WaitForSeconds(recoveryTime);
		recovery = false;
	}

	public	virtual void OnDamageTaken() {

	}


}
