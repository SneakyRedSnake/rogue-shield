using UnityEngine;
using System.Collections;

public class Blob: AbstractKillable {
	[SerializeField] GameObject blobPrefab;
	[SerializeField] float healthCuttoff = 50;
	HealthBehavior health;
	float maxHealth;
	float damages;
	// Use this for initialization
	void Start () {
		health = GetComponent<HealthBehavior>();
		maxHealth = health.GetMaxHealth ();
		damages = GetComponent<BaseDamageDealer>().damages;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void Kill() {
		if (maxHealth >= healthCuttoff) {
			GameObject blob = (GameObject)Instantiate (blobPrefab, transform.position + transform.right, transform.rotation);
			GameObject blob2 = (GameObject)Instantiate (blobPrefab, transform.position - transform.right, transform.rotation);
			blob.transform.localScale /= 2;
			blob2.transform.localScale /= 2;
			blob.GetComponent<HealthBehavior> ().setMaxHealth (maxHealth / 2);
			blob2.GetComponent<HealthBehavior> ().setMaxHealth (maxHealth / 2);
			blob.GetComponent<BaseDamageDealer>().damages = damages/2;
			blob2.GetComponent<BaseDamageDealer>().damages = damages/2;
		}
		Destroy (gameObject);
	}
}
