using UnityEngine;
using System.Collections;

/// <summary>
/// 	The entity with this script release all the items
/// 	of its inventory before dying.
/// </summary>
[RequireComponent(typeof(Inventory))]
public class KillableReleaseInventory : AbstractKillable {

	[SerializeField]float power = 300f;	//the power of ejection of the inventory items
	Inventory inventory; 				//inventory of the entity
	// Use this for initialization
	void Start () {
		inventory = GetComponent<Inventory>();
	}

	/// <summary>
	/// 	Kill the entity and release all his items
	/// </summary>
	public override void Kill(){
		GameObject[] items = inventory.inventory;
		//for each item in the inventory
		for(int i = 0; i<inventory.CurrentSize(); i++){
			//we simulate an ejection from the enemy's body
			items[i].transform.position = this.gameObject.transform.position;
			Vector2 randomVector = new Vector2(Random.Range (-1,1), Random.value);
          	randomVector.Normalize();
			randomVector *= power;
			items[i].rigidbody2D.AddForce(randomVector);
			items[i].SetActive(true);
		}
		Destroy (gameObject);
	}
}
