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

			// 
			foreach (Vector2 v in level.Layout) {
				Room room = level.Rooms[(int)v.x, (int)v.y];

				RemoveBlockingWalls (room);
				PopulateRoom (room);
			}

			// Instantiate each room and platforms
			foreach (Vector2 v in level.Layout) {
				Room room = level.Rooms[(int)v.x, (int)v.y];
				room.Scale(xWallScaleFactor, yWallScaleFactor);
		
				Component[,] comps = room.Components;
				
				GameObject roomFolder = new GameObject ("Room" + roomCounter);
				Transform roomFolderTransform = roomFolder.transform;
				roomFolderTransform.SetParent(environment);
				
				for(int i = 0; i < comps.GetLength(1); i++) {
					
					for(int j = 0; j < comps.GetLength(0); j++) {
						switch(comps[i, j]) {
						case Component.Platform:
							//Debug.Log("Drawing on : " + i + " " + j + "   Value : " + comps[i,j]);
							Transform platformInstance = (Transform)Instantiate (wallPlatformInstance, new Vector3 (room.getPosition().x * 20 + i * xWallScaleFactor, room.getPosition().y * 20 + j * yWallScaleFactor, 0), Quaternion.identity);
							platformInstance.SetParent (roomFolderTransform);
							break;
						case Component.Wall:
							Transform wallInstance = (Transform)Instantiate (wall, new Vector3 (room.getPosition().x * 20 + i * xWallScaleFactor, room.getPosition().y * 20 + j * yWallScaleFactor, 0), Quaternion.identity);
							wallInstance.SetParent (roomFolderTransform);
							break;
						}
					}
				}
			}

			Instantiate (player, new Vector3 (xWallScaleFactor + 2, yWallScaleFactor + 5, 0), Quaternion.identity);
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
		private void InstantiateSquare (int width, int height, Vector2 position)
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

		private Facing comingFrom = Facing.NONE;

		private void RemoveBlockingWalls (Room room)
		{
			int width = room.Width;
			int height = room.Height;
			int i = 0, j = 0;

			Debug.Log ("ComingFrom : " + comingFrom + "  Next dir : " + room.NextRoomDirection);
			switch (room.NextRoomDirection) {
			case Facing.EAST:
				i = width - 1;
				goto case Facing.WEST;

			case Facing.WEST:
				for (j = 1; j < height - 1; j++) {
					room.AddComponent(Component.None, new Vector2(i, j));
				}
				break;

			case Facing.NORTH:
				j = height - 1;
				goto case Facing.SOUTH;
			
			case Facing.SOUTH:
				for (i = 1; i < width - 1; i++) {
					room.AddComponent(Component.None, new Vector2(i, j));
				}
				break;
			default:
				break;
			}

			if (comingFrom == Facing.NONE) {
				comingFrom = room.NextRoomDirection.Opposite();
				return;
			}
				
			i = 0;
			j = 0;

			switch (comingFrom) {
			case Facing.EAST:
				i = width - 1;
				goto case Facing.WEST;
				
			case Facing.WEST:
				for (j = 1; j < height - 1; j++) {
					room.AddComponent(Component.None, new Vector2(i, j));
				}
				break;
				
			case Facing.NORTH:
				j = height - 1;
				goto case Facing.SOUTH;
				
			case Facing.SOUTH:
				for (i = 1; i < width - 1; i++) {
					room.AddComponent(Component.None, new Vector2(i, j));
				}
				break;
			default:
				break;
			}

			comingFrom = room.NextRoomDirection.Opposite();

			roomCounter ++;
		}

		private void PopulateRandom (Room room, int width, int height) {

			for (int i = 0; i < 10; i++) {
				int xRand = (int) UnityEngine.Random.Range (2, height - 2);
				int yRand = (int) UnityEngine.Random.Range (2, width - 2);
				//Debug.Log(xRand + " : " + yRand);
				room.AddComponent(Component.Platform, new Vector2(xRand, yRand));
				room.AddComponent(Component.Platform, new Vector2(xRand+1, yRand));
			}
		}

		private void PopulatePlatformGoingUp (Room room, int width, int height) {

			// First platform
			int xRef = (int) UnityEngine.Random.Range (2, width - 2);

			for (int i = 2; i < height - 2 ; i+=2) {
				int xRand = (int) UnityEngine.Random.Range (2, 5);
				int yRand = (int) UnityEngine.Random.Range (0, 2);

				int direction = (int) UnityEngine.Random.Range (0, 2);

				int xPos;
				Debug.Log("Dir : " + direction);
				if (direction == 0) // left
				{
					xPos = (xRef - xRand > 0) ? xRef - xRand : xRef + xRand;
				} else // right
				{
					xPos = (xRef + xRand < (width - 2)) ? xRef + xRand : xRef - xRand;
				}
			
				room.AddComponent (Component.Platform, new Vector2 (xPos, i + yRand));
				room.AddComponent (Component.Platform, new Vector2 (xPos + 1, i + yRand));
				xRef = xPos;
			}

			for (int i = 0; i < width; i++) {
				if (i > xRef + 2 || i < xRef -1 )
					room.AddComponent (Component.Wall, new Vector2 (i, height - 1));

			}
		}

		private void PopulateRoom( Room room) {
			int width = room.Width;
			int height = room.Height;

			switch (room.NextRoomDirection) {
			case Facing.EAST:
			case Facing.WEST:
				PopulateRandom(room, width, height);
				break;
			case Facing.SOUTH:
				PopulateRandom(room, width, height);
				break;
			case Facing.NORTH:
				PopulatePlatformGoingUp(room, width, height);
				break;
			}
		}

		/**
		 * Generate Horizontal platform beginning at position
		 */ 
		private void InstantiatePlatform (Vector2 position)
		{
				Transform platform = (Transform)Instantiate (wallPlatformInstance, new Vector3 (position.x, position.y, 0), Quaternion.identity);
				platform.SetParent (environment);
		}
	}
}