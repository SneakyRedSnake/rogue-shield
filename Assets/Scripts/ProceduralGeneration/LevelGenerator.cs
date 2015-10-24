using UnityEngine;
using System;
using System.Collections.Generic;

namespace Procedural
{
	public class LevelGenerator : MonoBehaviour
	{
		[Tooltip("Put here Wall Prefab ! ")]
		public Transform wall;
		public int minimumDistance = 6;
		public int maximumDistance = 10;
		public Vector2 startingPosition = new Vector2 (0, 0);
		public Vector2 endPosition = new Vector2 (6, 6);
		public int levelWidth = 10;
		public int levelHeight = 10;
		public LevelGenerationStrategy levelGenerationStrategy;

		private Transform environment; 				// Stores the roo object for the environment
		// Use this for initialization
		void Start ()
		{
			int nbIter = 0;
			float elapsedTime = Time.realtimeSinceStartup;

			GameObject obj = new GameObject ("Environment");
			environment = obj.GetComponent<Transform> ();

			levelGenerationStrategy = new RecursiveGeneration (levelWidth, levelHeight, startingPosition, endPosition, minimumDistance, maximumDistance);
			Level level = levelGenerationStrategy.generateLevel ();

			foreach (Vector2 v in level.Layout) {
				generateRoom (level.Rooms[(int)v.x, (int)v.y]);
			}		
		}

		public void instanciateWall (Vector2 position)
		{
			Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (position.x, position.y, 0), Quaternion.identity);
			wallInstance.localScale = new Vector2 (0.5f, 0.5f);
			wallInstance.SetParent (environment);
		}

		/**
		 * Generate a bordered square (not filled) beginning at position
		 */
		private void generateSquare (int width, int height, Vector2 position)
		{
		
			for (int i = 0; i <= width; i++) {
				for (int j = 0; j <= height; j++) {
					if (i == 0 || i == width || j == 0 || j == height) {
					
						Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (i + position.x, j + position.y, 0), Quaternion.identity);
						wallInstance.SetParent (environment);
					}
				}
			}
		}

		private void generateRoom (Room room)
		{
			int width = room.Width;
			int height = room.Height;
			Vector2 position = room.getPosition ();

			foreach (Facing facing in room.getFacings()) {
				int i, j;
				i = j = 0;

				switch (facing) {
				case Facing.EAST:
					i = width;
					goto case Facing.WEST;

				case Facing.WEST:
					for (j = 0; j < height; j++) {
						Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (i + position.x, j + position.y, 0), Quaternion.identity);
						wallInstance.SetParent (environment);
					}
					break;

				case Facing.NORTH:
					j = height;
					goto case Facing.SOUTH;
				
				case Facing.SOUTH:
					for (i = 0; i < width; i++) {
						Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (i + position.x, j + position.y, 0), Quaternion.identity);
						wallInstance.SetParent (environment);
					}
					break;

				}
			}
		}

		/**
		 * Generate Horizontal platform beginning at position
		 */ 
		private void generatePlatform (int width, Vector2 position)
		{
		
			for (int i = 0; i <= width; i++) {
				Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (i + position.x, position.y, 0), Quaternion.identity);
				wallInstance.SetParent (environment);
			}
		}
	}
}