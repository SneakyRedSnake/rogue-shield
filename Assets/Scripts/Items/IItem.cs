using UnityEngine;
using System.Collections;

/// <summary>
/// 	An interface for the items.
/// 	We can pick up an item and we can use it.
/// </summary>
public interface IItem{
	/// <summary>
	/// 	Do an action after someone use the item
	/// </summary>
	void Use();

	/// <summary>
	///		to know if the item must be destroy after use.
	/// </summary>
	/// <returns><c>true</c>, if is destroyed after use, <c>false</c> otherwise.</returns>
	bool isDestroyedAfterUse();

	/// <summary>
	/// 	Do an action when someone pick up the item
	/// </summary>
	/// <param name="receiver">The gameobject which receives the object</param>
	/// <returns><c>true</c> if has been picked up, <c>false</c> otherwise.</returns>
	bool PickUp(GameObject receiver);
}
