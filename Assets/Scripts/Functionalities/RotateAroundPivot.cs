using UnityEngine;
using System.Collections;

/// <summary>
/// 	Components to permits rotation of a game object in a specified angle
/// </summary>
public class RotateAroundPivot : MonoBehaviour {
	[SerializeField] GameObject pivot;
	[SerializeField] float rotateAngle;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	/// <summary>
	/// 	Rotation under the z axes using Quaternion.Euler
	/// </summary>
	/// <param name="z">The angle with the z axes</param>
	public void RotateAroundZ(float z, float distOfPivot){
		//We rotate the game object
		transform.rotation = Quaternion.Euler(0,0,z);

		//then we put it at the correct position
		float rad = (z+rotateAngle) * Mathf.Deg2Rad;
		transform.position = pivot.transform.position + distOfPivot * new Vector3(Mathf.Sin (rad), -Mathf.Cos (rad));
	}
}
