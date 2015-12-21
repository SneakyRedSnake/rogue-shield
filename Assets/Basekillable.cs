using UnityEngine;
using System.Collections;

public class Basekillable : AbstractKillable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Kill() {
		Destroy (gameObject);
	}
}
