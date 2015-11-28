using UnityEngine;
using System.Collections;

/// <summary>
/// 	State of the current player
/// </summary>
public class StatePlayer : MonoBehaviour {
	
	public enum StateShield{Shield, NoShield};			

	public StateShield shielded{get; private set;}	//to know if the player has is shield activated

	/// <summary>
	/// 	Initialize the important states possible for the player
	/// </summary>
	void Start () {
		this.shielded = StateShield.NoShield;
	}

	/// <summary>
	/// 	Activates the shield.
	/// </summary>
	/// <param name="activate">If set to <c>true</c> put the state of the shield to Shield.
	/// 		Otherwise, put to NoShield.</param>
	public void ActivateShield(bool activate){
		if (activate)
			this.shielded = StateShield.Shield;
		else
			this.shielded = StateShield.NoShield;
	}


}
