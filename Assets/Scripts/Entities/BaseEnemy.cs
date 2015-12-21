using UnityEngine;
using System.Collections;

public class BaseEnemy : HealthBehavior {
	private Movement movement;
	// Use this for initialization
	void Start () {
		movement = GetComponent<Movement> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnDamageTaken(){
		
	}
	
	
	public override void KnockBack() {
		if(movement != null)
			movement.KnockBack ();
	}

}
