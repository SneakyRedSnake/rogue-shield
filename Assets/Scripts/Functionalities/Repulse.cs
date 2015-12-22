using UnityEngine;
using System.Collections;

/// <summary>
/// 	Component which permits an object to repulse selected objects
/// 	with a given force
/// </summary>
public class Repulse : MonoBehaviour {
	[SerializeField]LayerMask whatCanRepulse;				//what the shield can repulse
	[SerializeField]float repulseForce = 10000f;			//the force of the repulsion


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// 	When something hit this and can be repulsed,
	/// 	we repulse it.
	/// </summary>
	/// <param name="other">The collider2D of the object which collides with this</param>
	void OnTriggerEnter2D(Collider2D other){
		//we check if we can repulse the other gameObject
		if ((whatCanRepulse.value & 1<<other.gameObject.layer)>0){
			other.gameObject.rigidbody2D.AddForce(repulseForce * gameObject.transform.right);
		}
	}
}