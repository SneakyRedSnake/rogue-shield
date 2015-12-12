using UnityEngine;
using System.Collections;

/// <summary>
/// 	Health item behaviour.
/// </summary>
public class HealthItemBehaviour : ItemBehaviour {
	[SerializeField]int numberOfHealth;			//The number of health this health item gave

	// Use this for initialization
	void Start () {
		this.item = new HealthItem (this.itemName, numberOfHealth);
	}
}
