using UnityEngine;
using System.Collections;

/// <summary>
/// 	The abstract class Item.
/// 	We add the functionality of the trigger
/// </summary>
public abstract class Item : MonoBehaviour, IItem {

	[SerializeField]string itemName;						//the name of the object
	[SerializeField]LayerMask whoCanTakeTheItem;			//who can take the item

	public abstract void Use();
	public bool PickUp(GameObject receiver){
		Inventory inventory = receiver.GetComponent<Inventory> ();
		//if there is an inventory
		if (inventory) {
			//we add the gameobject to the inventory
			inventory.Add(this.gameObject);
			//the current game object is inactive for the moment
			this.gameObject.SetActive(false);
			Debug.Log("inventory : "+inventory.GetInventoryContent());
			return true;
		}
		return false;
	}

	/// <summary>
	/// 	Tell if the item is destroyed after we use it
	/// </summary>
	/// <returns><c>true</c>, if destroyed after use, <c>false</c> otherwise.</returns>
	public bool isDestroyedAfterUse(){
		return true;
	}

	/// <summary>
	/// 	The name of the item.
	/// </summary>
	/// <returns>The name of the item.</returns>
	public string GetItemName(){
		return itemName;
	}

	/// <summary>
	/// 	When something trigger the item
	/// 	we add it in his inventory if we can
	/// 	or if it's a player we inform him he could pick up the item
	/// </summary>
	/// <param name="other">The Collider2D of the gameobject which trigger the Item</param>
	void OnTriggerEnter2D(Collider2D other){
		if ((whoCanTakeTheItem & 1 << other.gameObject.layer)>0) {
			//if it's the player, inform him that he can pick the item
			if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
				other.gameObject.GetComponentInParent<StatePlayer>().AddItemTriggered(gameObject);
			}
		}
	}

	/// <summary>
	/// 	When a player exit the trigger of the item
	/// 	we remome it from his list of items he could pick up
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerExit2D(Collider2D other){
		//if it's the player, inform him that he can't pick up the item anymore
		if ((whoCanTakeTheItem & 1 << other.gameObject.layer)>0 && other.gameObject.layer == LayerMask.NameToLayer("Player")) {
			other.gameObject.GetComponentInParent<StatePlayer>().RemoveItemTriggered(gameObject);

		}
	}
}
