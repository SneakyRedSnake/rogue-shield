using UnityEngine;
using System.Collections;

public class PlayerKillable : AbstractKillable {

	// Use this for initialization
	void Start () {
	
	}
	
	public override void Kill() {
		GameManager gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		Debug.Log ("we get killed");
		gm.EndGame (0);
	}
}
