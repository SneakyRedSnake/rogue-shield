using UnityEngine;
using System.Collections;

/// <summary>
/// 	The controller of the shield
/// </summary>
[RequireComponent(typeof(RotateAroundPivot))]
[RequireComponent(typeof(BoxCollider2D))]
public class ShieldControl : MonoBehaviour {
	private const int RIGHTCLICK = 1;					//the code of the Right click of the mouse

	[SerializeField]private BoxCollider2D boxCollider;	//The box collider of the gameobject 
	private RotateAroundPivot rotateAround;				//The component to do a rotation around sth
	[SerializeField]float distShieldPlayer = 4;			//The distance we want between the shield and the player

	private StatePlayer statePlayer;

	// Use this for initialization
	void Start () {
		rotateAround = GetComponent<RotateAroundPivot>();
		statePlayer = transform.root.GetComponent<StatePlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		//we get the position of the player
		//TODO changer le GetChild en get player?
		Vector3 pos = transform.parent.GetChild(0).position;
		
		//we get the position of the camera relative at the current scene
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//it's a trigger only if the shield is active
		bool clicked = Input.GetMouseButton (RIGHTCLICK);
		statePlayer.ActivateShield (clicked);
		boxCollider.isTrigger = clicked;

		//we calculate the angle
		float deg = Mathf.Rad2Deg * Mathf.Atan2(mousePos.y - pos.y, mousePos.x - pos.x);

		//we do a rotation
		rotateAround.RotateAroundZ (deg, distShieldPlayer);
	}
}
