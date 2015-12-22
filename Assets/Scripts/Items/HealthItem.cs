using UnityEngine;
using System.Collections;

/// <summary>
/// 	an Health item.
/// </summary>
public class HealthItem : Item {
	[SerializeField] int numberOfHealth; 			//the number of health this item give

	public override void Use(){
		Debug.Log ("used health item");
		gameObject.GetComponentInParent<HealthBehavior> ().addHealth (numberOfHealth);
		Destroy (gameObject);
	}
}
