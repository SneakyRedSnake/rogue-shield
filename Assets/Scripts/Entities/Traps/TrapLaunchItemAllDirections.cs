using UnityEngine;
using System.Collections;

public class TrapLaunchItemAllDirections : MonoBehaviour {
	[SerializeField]LayerMask whatCanDetect;
	[SerializeField]GameObject whatToLaunch;
	[SerializeField]float timeBetweenTwoLaunch = 5f;
	
	private float currentWait = 0f;
	
	void Start(){
		currentWait = timeBetweenTwoLaunch;
	}
	
	// Update is called once per frame
	void Update () {
		currentWait += Time.deltaTime;
		if (currentWait >= timeBetweenTwoLaunch) {
			RaycastHit2D left = Physics2D.Raycast(this.gameObject.transform.position, -Vector2.right,Mathf.Infinity, whatCanDetect);
			RaycastHit2D right = Physics2D.Raycast(this.gameObject.transform.position, Vector2.right,Mathf.Infinity, whatCanDetect);
			RaycastHit2D up = Physics2D.Raycast(this.gameObject.transform.position, Vector2.up,Mathf.Infinity, whatCanDetect);
			RaycastHit2D down = Physics2D.Raycast(this.gameObject.transform.position, -Vector2.up,Mathf.Infinity, whatCanDetect);

			Vector2 direction = -Vector2.right;
			if (left.collider != null) {
				LaunchItem(direction);
				Debug.Log ("left" + left.collider.gameObject.name);
			}

			direction = Vector2.right;
			if (right.collider != null) {
				LaunchItem(direction);
				Debug.Log ("right" + right.collider.gameObject.name);
			}

			direction = -Vector2.up;
			if (down.collider != null) {
				LaunchItem(direction);
				Debug.Log ("down" + down.collider.gameObject.name);
			}

			direction = Vector2.up;
			if (up.collider != null) {
				LaunchItem(direction);
				Debug.Log ("up" + up.collider.gameObject.name);
			}
		}
	}

	void LaunchItem(Vector2 direction){
		currentWait = 0f;
		GameObject projectile = (GameObject)Instantiate (whatToLaunch);
		projectile.transform.position = new Vector2 (this.gameObject.transform.position.x + (direction.x)*3, this.gameObject.transform.position.y + (direction.y)*3);
		projectile.rigidbody2D.velocity = direction * 50;
	}
}
