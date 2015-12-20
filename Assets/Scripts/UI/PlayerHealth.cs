using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerHealth : HealthBehavior {

	[SerializeField] private float animateTime = 1f;
	Slider healthSlider;
	private float healthDiff;
	private float elapsedTime;

	public void OnEnable() {
		healthSlider = GameObject.Find ("HealthSlider").GetComponent<Slider>();
		if (!healthSlider) {
			Debug.Log("Cannot find the slider ");
		}
		healthSlider.minValue = 0f;
		healthSlider.maxValue = 100f;
		healthSlider.value = 100f;
		healthDiff = health;
		elapsedTime = 0;
		Debug.Log ("slider value" + healthSlider.value + " max:" + healthSlider.maxValue);
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
		Debug.Log ("on damage taken");
		healthDiff = health/maxHealth * 100;
		elapsedTime = 0;
	}


}
