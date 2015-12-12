using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectInventory : MonoBehaviour {

	[SerializeField]IList<GameObject> inventory = new List<GameObject> ();	//the inventory is a list of game object

	/// <summary>
	/// 	Add the specified item to the inventory
	/// </summary>
	/// <param name="item">The item we want to add.</param>
	public void Add(GameObject item){
		Debug.Log ("add in the inventory");
		inventory.Add (item);
	}
	
	public string GetInventoryContent(){
		string inventoryContent = "";
		for(int i = 0; i < inventory.Count; i++){
			inventoryContent += inventory[i].name+"\n";
		}
		return inventoryContent;
	}
}
