using UnityEngine;
using UnityEditor;
using System.Collections;

public class TestSceneGenerator : MonoBehaviour {

	[Tooltip("Put here Player Prefab ! ")]
	public Transform player;
	[Tooltip("Put here Player Camera Prefab ! ")]
	public Transform playerCamera;
	[Tooltip("Put here Wall Prefab ! ")]
	public Transform wall;

	private Transform environment; 				// Stores the roo object for the environment

	// Use this for initialization
	void Start () {

		/*	Temporary hard coded parameters	*/
		int width = 100;
		int height = 40;
		Vector2 spawnPosition = new Vector2 (10, 2);

		/*	Create the root environment transform which will contain all the walls	*/
		GameObject obj = new GameObject ("Environment");
		environment = obj.GetComponent<Transform> ();

		/*	Generate the scene	*/
		generateSquare (width, height, Vector2.zero);
		generatePlatform (10, new Vector2(10, 20));
		generatePlatform (10, new Vector2(25, 10));
		generatePlatform (30, new Vector2(50, 25));

		/*	Instantiate the player	*/
        /*  Disable Player Spawn temporaly */
		//spawnPlayer (spawnPosition);

		/*	Save the scene to a file	*/
		//saveGeneratedScene ("./Assets/Scene/GeneratedScene.unity");
	}

	/**
	 * Generate a bordered square (not filled) beginning at position
	 */
	private void generateSquare(int width, int height, Vector2 position) {

		for (int i = 0; i <= width; i++) {
			for (int j = 0; j <= height; j++) {
				if(i == 0 || i == width || j == 0 || j == height) {
					
					Transform wallInstance = (Transform) Instantiate(wall, new Vector3(i + position.x, j + position.y, 0), Quaternion.identity);
					wallInstance.SetParent(environment);
				}
			}
		}
	}

	/**
	 * Generate Horizontal platform beginning at position
	 */ 
	private void generatePlatform(int width, Vector2 position) {

		for (int i = 0; i <= width; i++) {
			Transform wallInstance = (Transform) Instantiate(wall, new Vector3(i + position.x, position.y, 0), Quaternion.identity);
			wallInstance.SetParent(environment);
		}
	}

	/**
	 * Instantiate the player and camera at given spawn position
	 */ 
	private void spawnPlayer(Vector2 spawnPosition) {
		Transform playerInstance = (Transform) Instantiate (player, new Vector3 (spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
		Transform playerCameraInstance = (Transform) Instantiate (playerCamera, new Vector3 (spawnPosition.x, spawnPosition.y, playerCamera.position.z), Quaternion.identity);

		playerCameraInstance.SetParent (playerInstance);
	}

	/**
	 * Save the current scene to a scene file
	 * Stores all previously instantiated GameObject
	 */
	private void saveGeneratedScene(string path) {
		// path example : "./Assets/Scene/test.unity"
		EditorApplication.SaveScene (path);
	}
}
 