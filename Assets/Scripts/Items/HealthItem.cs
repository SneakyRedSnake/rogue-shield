using UnityEngine;
using System.Collections;

/// <summary>
/// 	an Health item.
/// </summary>
public class HealthItem : Item {
	[SerializeField] int numberOfHealth; 			//the number of health this item give

	public override void Use(){
		//TODO add health
		Debug.Log ("add " + numberOfHealth + "pv");
		Destroy (gameObject);
	}
}
