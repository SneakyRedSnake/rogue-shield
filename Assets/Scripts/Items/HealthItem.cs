using UnityEngine;
using System.Collections;

/// <summary>
/// 	an Health item.
/// </summary>
public class HealthItem : Item {
	private int numberOfHealth; 			//the number of health this item give

	public HealthItem(string itemName, int numberOfHealth) : base(itemName){
		this.numberOfHealth = numberOfHealth;
	}

	public override void Use(){
		//TODO add health
		Debug.Log ("add " + numberOfHealth + "pv");
	}
}
