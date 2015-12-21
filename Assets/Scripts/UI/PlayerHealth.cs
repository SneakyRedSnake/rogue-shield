using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
/* Base behavior for the health of the player, includes the UI and the knockback effect
 */
[RequireComponent(typeof(Movement))]
public class PlayerHealth : HealthBehavior {

	[SerializeField] private float animateTime = 1f;//time for the health bar to reach it's true value
	Slider healthSlider;
	private float healthDiff;
	private float elapsedTime;
	private Movement movement;

	public void OnEnable() {
		movement = GetComponent<Movement> ();
		healthSlider = GameObject.Find ("HealthSlider").GetComponent<Slider>();
		if (!healthSlider) {
			Debug.Log("Cannot find the slider ");
		}
		healthSlider.minValue = 0f;
		healthSlider.maxValue = 100f;
		healthSlider.value = 100f;
		healthDiff = health;
		elapsedTime = 0;
	}


	public void Update() {
		if (healthDiff != health && elapsedTime < animateTime) {
			float t = elapsedTime / animateTime;
			t = Mathf.Sin (t * Mathf.PI * 0.5f);
			healthSlider.value = Mathf.Lerp (healthDiff, health / maxHealth * 100, t);
			elapsedTime += Time.deltaTime;
		}


	}


	public override void OnDamageTaken(){
		healthDiff = health/maxHealth * 100;
		elapsedTime = 0;
	}


	public override void KnockBack() {
		movement.KnockBack ();
	}


}
