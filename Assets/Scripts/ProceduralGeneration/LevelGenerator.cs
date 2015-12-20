using UnityEngine;
using System;
using System.Collections.Generic;

namespace Procedural
{
	public class LevelGenerator : MonoBehaviour
	{
		[Tooltip("Put here Wall Prefab ! ")]
		public Transform wall;
		public GameObject player;
		public Transform camera;
		public Transform ennemy;
		public Transform wallPlatformInstance;

		public int minimumDistance = 6;
		public int maximumDistance = 10;
		public int horizontalRoomNb = 10;
		public int verticalRoomNb = 10;

		public Vector2 startingPosition = new Vector2 (0, 0);
		public Vector2 endPosition = new Vector2 (6, 6);
		private int levelWidth;
		private int levelHeight;
		public LevelGenerationStrategy levelGenerationStrategy;

		private int roomCounter = 0;
		private Transform environment; 				// Stores the roo object for the environment
		// Use this for initialization
		void Start ()
		{
			int nbIter = 0;
			float elapsedTime = Time.realtimeSinceStartup;

			// Suppose wall prefab is a square
			int xWallScaleFactor = (int)wall.localScale.x;
			int yWallScaleFactor = (int)wall.localScale.y;

			GameObject obj = new GameObject ("Environment");
			environment = obj.GetComponent<Transform> ();

			levelGenerationStrategy = new RecursiveGeneration (horizontalRoomNb, verticalRoomNb, startingPosition, endPosition, minimumDistance, maximumDistance);
			Level level = levelGenerationStrategy.generateLevel ();

			foreach (Vector2 v in level.Layout) {
				Room room = level.Rooms[(int)v.x, (int)v.y];
				room.Scale(xWallScaleFactor, yWallScaleFactor);

				GenerateRoom (room, xWallScaleFactor, yWallScaleFactor);
				PopulateRoom (room, xWallScaleFactor, yWallScaleFactor);

				Component[,] comps = room.Components;

				for(int i = 0; i < comps.GetLength(1); i++) {

					for(int j = 0; j < comps.GetLength(0); j++) {
						if (comps[i, j] == Component.Platform) {
							Debug.Log("Drawing on : " + i + " " + j + "   Value : " + comps[i,j]);
							GeneratePlatform(new Vector2(room.getPosition().x * 20 + i * xWallScaleFactor, room.getPosition().y * 20 + j * yWallScaleFactor));
						}
					}
				}
			}
			Instantiate (player, new Vector3 (xWallScaleFactor + 2, yWallScaleFactor + 5, 0), Quaternion.identity);
			Instantiate (camera, new Vector3 (xWallScaleFactor + 5, yWallScaleFactor + 5, -2), Quaternion.identity);
			Instantiate (ennemy, new Vector3 (xWallScaleFactor + 30, yWallScaleFactor + 20, 0 ), Quaternion.identity);
		}

		public void InstanciateWall (Vector2 position)
		{
			Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (position.x, position.y, 0), Quaternion.identity);
			wallInstance.localScale = new Vector2 (0.5f, 0.5f);
			wallInstance.SetParent (environment);
		}

		/**
		 * Generate a bordered square (not filled) beginning at position
		 */
		private void GenerateSquare (int width, int height, Vector2 position)
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

		private void GenerateRoom (Room room, int xWallScaleFactor, int yWallScaleFactor)
		{
			int width = room.Width;
			int height = room.Height;

			Vector2 position = room.getPosition () * 20;

			GameObject roomFolder = new GameObject ("Room" + roomCounter);
			Transform roomFolderTransform = roomFolder.transform;
			roomFolderTransform.SetParent(environment);

			Debug.Log ("Position : " + position + "   Width : " + width + "  Height ; " + height);

			foreach (Facing facing in room.getClosedBorders()) {
				int i, j;
				i = j = 0;

				switch (facing) {
				case Facing.EAST:
					i = width;
					goto case Facing.WEST;

				case Facing.WEST:
					for (j = 0; j < height; j+=yWallScaleFactor) {
						Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (i + position.x, j + position.y, 0), Quaternion.identity);
						wallInstance.SetParent (roomFolderTransform);
					}
					break;

				case Facing.NORTH:
					j = height;
					goto case Facing.SOUTH;
				
				case Facing.SOUTH:
					for (i = 0; i < width; i+=xWallScaleFactor) {
						Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (i + position.x, j + position.y, 0), Quaternion.identity);
						wallInstance.SetParent (roomFolderTransform);
					}
					break;

				}
			}
			roomCounter ++;
		}

		private void PopulateRoom( Room room, int xScaleFactor, int yScaleFactor) {
			int width = room.Width / xScaleFactor;
			int height = room.Height / yScaleFactor;

			for (int i = 0; i < 4; i++) {
				int xRand = (int) UnityEngine.Random.Range (5, height - 5);
				int yRand = (int) UnityEngine.Random.Range (5, width - 5);
				Debug.Log(xRand + " : " + yRand);
				room.AddComponent(Component.Platform, new Vector2(xRand, yRand));
			}
		}

		/**
		 * Generate Horizontal platform beginning at position
		 */ 
		private void GeneratePlatform (Vector2 position)
		{
				Transform platform = (Transform)Instantiate (wallPlatformInstance, new Vector3 (position.x, position.y, 0), Quaternion.identity);
				platform.SetParent (environment);
		}
	}
}