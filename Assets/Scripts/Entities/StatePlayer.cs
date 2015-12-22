using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// 	State of the current player
/// </summary>

public class StatePlayer : MonoBehaviour {
	
	public enum StateShield{Shield, NoShield};		
	private IList<GameObject> itemsInRange;

	public StateShield shielded{get; private set;}	//to know if the player has is shield activated

	/// <summary>
	/// 	Initialize the important states possible for the player
	/// </summary>
	void Start () {
		this.shielded = StateShield.NoShield;
		itemsInRange = new List<GameObject> ();
	}

	/// <summary>
	/// 	Activates the shield.
	/// </summary>
	/// <param name="activate">If set to <c>true</c> put the state of the shield to Shield.
	/// 		Otherwise, put to NoShield.</param>
	public void ActivateShield(bool activate){
		if (activate)
			this.shielded = StateShield.Shield;
		else
			this.shielded = StateShield.NoShield;
	}

	/// <summary>
	/// 	Adds the item triggered.
	/// </summary>
	/// <param name="item">Item.</param>
	public void AddItemTriggered(GameObject item){
		itemsInRange.Add (item);
	}

	/// <summary>
	///		Removes the item triggered.
	/// </summary>
	/// <param name="item">Item.</param>
	public void RemoveItemTriggered(GameObject item){
		itemsInRange.Remove (item);
	}

	/// <summary>
	/// 	Takes the first item of the list.
	/// </summary>
	public void TakeItem(){
		if (itemsInRange.Count > 0) {
			GameObject itemToTake = itemsInRange [0];
			Item item = itemToTake.GetComponent<Item>();
			//if the player has picked up the item we remove it from the list of items we can pick up
			GameObject player = this.GetComponentInChildren<Inventory>().gameObject;
			if(item.PickUp(player))
				RemoveItemTriggered(itemToTake);
		}

	}
}
