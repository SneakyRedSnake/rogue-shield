using UnityEngine;
using System.Collections;

public class Blob: AbstractKillable {
	[SerializeField] GameObject blobPrefab;
	[SerializeField] float healthCuttoff = 50;
	HealthBehavior health;
	float maxHealth;
	float damages;
	[SerializeField]float power = 300f;	//the power of ejection of the inventory items
	Inventory inventory; 				//inventory of the entity
	// Use this for initialization
	void Start () {
		inventory = GetComponent<Inventory>();
		health = GetComponent<HealthBehavior>();
		maxHealth = health.GetMaxHealth ();
		damages = GetComponent<BaseDamageDealer>().damages;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public override void Kill() {
		if (maxHealth >= healthCuttoff) {
			spawnBlob(transform.right);
			spawnBlob(-transform.right);
		} else {
			dropInventory ();
		}
		Destroy (gameObject);
	}

	public void spawnBlob(Vector3 dir){
		GameObject blob = (GameObject)Instantiate (blobPrefab, transform.position + dir, transform.rotation);
		blob.transform.localScale /= 2;
		blob.GetComponent<HealthBehavior> ().setMaxHealth (maxHealth / 2);
		blob.GetComponent<BaseDamageDealer> ().damages = damages / 2;
	}

	public void dropInventory() {
		GameObject[] items = inventory.inventory;
		GameObject inv = GameObject.Find ("inventory");
		//for each item in the inventory
		for (int i = 0; i<inventory.CurrentSize(); i++) {
			//we simulate an ejection from the enemy's body
			items [i].transform.parent = inv.transform;
			items [i].transform.position = this.gameObject.transform.position;
			Vector2 randomVector = new Vector2 (Random.Range (-1, 1), Random.value);
			randomVector.Normalize ();
			randomVector *= power;
			items [i].rigidbody2D.AddForce (randomVector);
			items [i].SetActive (true);
			
		}
	}
}
