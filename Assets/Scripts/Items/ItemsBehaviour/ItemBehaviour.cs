using UnityEngine;
using System.Collections;

/// <summary>
/// 	The behaviour of the item gameobject
/// </summary>
public abstract class ItemBehaviour:MonoBehaviour{
	[SerializeField]protected string itemName;				//the name of the object
	[SerializeField]LayerMask whoCanTakeTheItem;			//who can take the item
	protected Item item;									//the item

	/// <summary>
	/// 	When something trigger the item
	/// 	we add it in his inventory if we can
	/// </summary>
	/// <param name="other">The Collider2D of the gameobject which trigger the Item</param>
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("trigger item game object");
		if ((whoCanTakeTheItem & 1 << other.gameObject.layer)>0) {
			Debug.Log ("Pick up");
			item.PickUp(other.gameObject);
			Destroy (gameObject);
		}
	}
}

