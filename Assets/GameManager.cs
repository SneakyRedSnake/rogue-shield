using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	[SerializeField] string endScene = "end";
	[SerializeField] float waitTime = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void EndGame(int lvl)
	{
		StartCoroutine(EndGameImpl(lvl));
	}
	
	IEnumerator EndGameImpl(int lvl)
	{
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(endScene);
	}

}
