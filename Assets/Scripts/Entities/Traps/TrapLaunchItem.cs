using UnityEngine;
using System.Collections;

public class TrapLaunchItem : MonoBehaviour {
	[SerializeField]LayerMask whatCanDetect;
	[SerializeField]GameObject whatToLaunch;
	[SerializeField]float timeBetweenTwoLaunch = 5f;
	[SerializeField]Vector2 direction;

	private float currentWait = 0f;

	void Start(){
		currentWait = timeBetweenTwoLaunch;
	}

	// Update is called once per frame
	void Update () {
		currentWait += Time.deltaTime;
		if (currentWait >= timeBetweenTwoLaunch) {
			RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, direction,Mathf.Infinity, whatCanDetect);

			if (hit.collider != null) {
				currentWait = 0f;
				GameObject projectile = (GameObject)Instantiate (whatToLaunch);
				projectile.transform.position = new Vector2 (this.gameObject.transform.position.x + (direction.x)*3, this.gameObject.transform.position.y + (direction.y)*3);
				projectile.rigidbody2D.velocity = direction * 50;
				Debug.Log ("left" + hit.collider.gameObject.name);
			}
		}
	}
}
