using UnityEngine;
using System.Collections;

/// <summary>
/// 	The abstract class Item.
/// 	We add the functionality of the trigger
/// </summary>
public abstract class Item : IItem {

	private string itemName;			//the name of the item

	public Item(string itemName){
		this.itemName = itemName;
	}

	public abstract void Use();
	public void PickUp(GameObject receiver){
		Inventory inventory = receiver.GetComponent<Inventory> ();
		if (inventory) {
			inventory.Add(this);
		}
		Debug.Log(inventory.GetInventoryContent());
	}

	public bool isDestroyedAfterUse(){
		return true;
	}

	public string GetName(){
		return itemName;
	}
}
