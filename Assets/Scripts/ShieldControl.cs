using UnityEngine;
using System.Collections;

/// <summary>
/// 	The controller of the shield
/// </summary>
[RequireComponent(typeof(RotateAroundPivot))]
public class ShieldControl : MonoBehaviour {

	private RotateAroundPivot rotateAround;			//The component to do a rotation around sth
	[SerializeField]float distShieldPlayer = 4;		//The distance we want between the shield and the player

	// Use this for initialization
	void Start () {
		rotateAround = GetComponent<RotateAroundPivot>();
	}
	
	// Update is called once per frame
	void Update () {
		//we get the position of the player
		//TODO changer le GetChild en get player?
		Vector3 pos = transform.parent.GetChild(0).position;
		
		//we get the position of the camera relative at the current scene
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//we calculate the angle
		float deg = Mathf.Rad2Deg * Mathf.Atan2(mousePos.y - pos.y, mousePos.x - pos.x);

		//we do a rotation
		rotateAround.RotateAroundZ (deg, distShieldPlayer);
	}
}
