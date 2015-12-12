using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 	Represents an inventory.
/// </summary>
public class Inventory : MonoBehaviour {
	private IList<Item> inventory;			//the inventory is a list of Item
	[SerializeField] int maxSize;			//the max size of the inventory;

	private int currentSize;				//the current size of the inventory;	

	/// <summary>
	/// 	Initialize the inventory
	/// </summary>
	void Start () {
		inventory = new List<Item> ();
		currentSize = 0;
	}

	/// <summary>
	/// 	Add the specified item to the inventory
	/// </summary>
	/// <param name="item">The item we want to add.</param>
	/// <returns><c>true</c> if we achieve to add it, <c>false</c> otherwise</returns>
	public bool Add(Item item){
		if (currentSize < maxSize) {
			Debug.Log ("In the inventory");
			inventory.Add (item);
			currentSize++;
			return true;
		}
		return false;
	}

	/// <summary>
	/// 	Use the item at the specified position.
	/// </summary>
	/// <param name="position">Position of the object we want to use</param>
	public void Use(int position){
		if (position < currentSize && position >= 0) {
			Item item = inventory[position];
			if(item.isDestroyedAfterUse()){
				inventory.RemoveAt(position);
				currentSize--;
			}
			item.Use ();
		}
	}

	public string GetInventoryContent(){
		string inventoryContent = "";
		for(int i = 0; i < currentSize; i++){
			inventoryContent += inventory[i].GetName()+"\n";
		}
		return inventoryContent;
	}

}
