using System;
using System.Collections.Generic;
using UnityEngine;

namespace Procedural
{
	public class RecursiveGeneration : LevelGenerationStrategy
	{
		private List<List<Facing>> remainingDirections;
		private Room[,] rooms;
		private List<Vector2> layout;
		private Vector2 startingPosition;
		private Vector2 endPosition;
		private int horizontalRoomNb, verticalRoomNb;
		private int minimumDistance,  maximumDistance;
		private int currentGenIteration = -1;
		private Vector2 comingFrom;

		public RecursiveGeneration (int horizontalRoomNb, int verticalRoomNb, Vector2 startingPosition, Vector2 endPosition, int minimumDistance, int maximumDistance)
		{
			float elapsedTime = Time.realtimeSinceStartup;
			this.endPosition = endPosition;
			this.horizontalRoomNb = horizontalRoomNb;
			this.verticalRoomNb = verticalRoomNb;
			this.minimumDistance = minimumDistance;
			this.maximumDistance = maximumDistance;
			comingFrom = new Vector2 (startingPosition.x, startingPosition.y - 1);
		}

		#region LevelGenerationStrategy implementation
		public Level generateLevel ()
		{
			float elapsedTime = 0;
			/* Generate direction set */
			remainingDirections = new List<List<Facing>>(maximumDistance);
			
			rooms = new Room[horizontalRoomNb, verticalRoomNb];
			layout = new List<Vector2> (10);
			findPath ((int)startingPosition.x, (int)startingPosition.y, minimumDistance);
			
			elapsedTime = Time.realtimeSinceStartup - elapsedTime;
			Debug.Log ("Generation time : " + elapsedTime);
			Debug.Log ("Number of Rooms : " + layout.Count);
			
			RemoveBlockingBorders ();

			return new Level (horizontalRoomNb, verticalRoomNb, layout, rooms);
		}
		#endregion

		private bool findPath (int x, int y, int minDistance)
		{
			currentGenIteration ++;
			Debug.Log ("Starting find Path : x = " + x + "   y = " + y + "    distance = " + minDistance + "   currentGenIteration : " + currentGenIteration);

			if (currentGenIteration >=  (maximumDistance )) {
				currentGenIteration --;
				return false;
			}
			
			if ((x == endPosition.x && y == endPosition.y) && (minDistance <= 0)) {
				addRoomToLayout (x, y);
				return true;
			}
			
			if (!validPosition (x, y)) {
				currentGenIteration--;
				return false;
			}
			
			List<Facing> facings = new List<Facing> ();
			foreach (Facing facing in Enum.GetValues(typeof(Facing))) {
				facings.Add (facing);
			}

			remainingDirections.Add(facings);
			Debug.Log ("Remain Dir Coun" + remainingDirections.Count);
			remainingDirections[currentGenIteration].Remove(GetHeadingDirection(comingFrom, new Vector2(x, y)).Opposite());
			
			addRoomToLayout (x, y);
			comingFrom = layout [layout.Count - 1];

			while (remainingDirections[currentGenIteration].Count > 0) {
				int r = UnityEngine.Random.Range (0, remainingDirections[currentGenIteration].Count);
				Facing direction = remainingDirections[currentGenIteration][r];

				Debug.Log("Chosen : "+ direction + "  at Iter : "+ currentGenIteration);
				remainingDirections[currentGenIteration].Remove(direction);
				switch (direction) {
				case Facing.NORTH:
					if (findPath (x, y + 1, minDistance - 1) == true)
						return true; 
					break;
					
				case Facing.SOUTH:
					if (findPath (x, y - 1, minDistance - 1) == true)
						return true;
					break;
					
				case Facing.EAST:
					if (findPath (x + 1, y, minDistance - 1) == true)
						return true;
					break;
					
				case Facing.WEST:
					if (findPath (x - 1, y, minDistance - 1) == true)
						return true;
					break;
				}
				
				Debug.Log("Fail : "+ direction);
			}
			Debug.Log ("Number of Rooms : " + layout.Count);

			remainingDirections.RemoveAt (remainingDirections.Count - 1);
			currentGenIteration--;
			
			layout.RemoveAt (layout.Count - 1);
			rooms [x, y] = null;
			return false;
		}

		private void addRoomToLayout (int x, int y)
		{
			layout.Add (new Vector2 (x , y));
			rooms [x, y] = new Room (new Vector2 (x, y ));
		}

		/**
	 *	Determine if the position is valid to generate a room
	 *	Check if position is already filled
	 *	Check if position is within boundaries
	 */
		private bool validPosition (int x, int y)
		{
			if (x == endPosition.x && y == endPosition.y)
				return false;
			
			foreach (Vector2 position in layout) {
				if (position.x == x && position.y == y)
					return false;
			}
			
			return (((x >= 0) && (x < horizontalRoomNb)) && ((y >= 0) && (y < verticalRoomNb)));
		}

		public void RemoveBlockingBorders() {
			
			foreach (Vector2 lastRoomPos in layout) {
				
				int currentIndex = layout.IndexOf(lastRoomPos);

				if ( lastRoomPos.x == endPosition.x && lastRoomPos.y == endPosition.y) {
					return;
				}

				Room lastRoom = rooms[(int)lastRoomPos.x, (int)lastRoomPos.y];
				Room headingRoom = rooms[(int)layout[currentIndex + 1].x, (int)layout[currentIndex + 1].y];
				
				Facing goingTo = GetHeadingDirection(lastRoomPos, layout[currentIndex + 1]);
				
				switch (goingTo) {
				case Facing.NORTH:
					headingRoom.removeClosedBorder (Facing.SOUTH);
					if (lastRoom != null)
						lastRoom.removeClosedBorder (Facing.NORTH);
					break;
					
				case Facing.SOUTH:
					headingRoom.removeClosedBorder (Facing.NORTH);
					if (lastRoom != null)
						lastRoom.removeClosedBorder (Facing.SOUTH);
					break;
					
				case Facing.EAST:
					headingRoom.removeClosedBorder (Facing.WEST);
					if (lastRoom != null)
						lastRoom.removeClosedBorder (Facing.EAST);
					break;
					
				case Facing.WEST:
					headingRoom.removeClosedBorder (Facing.EAST);
					if (lastRoom != null)
						lastRoom.removeClosedBorder (Facing.WEST);
					break;
				}
			}
		}

		public Facing GetHeadingDirection(Vector2 from, Vector2 to) {
			if (from.x < to.x && from.y == to.y)
				return Facing.EAST;
			else if (from.x > to.x && from.y == to.y)
				return Facing.WEST;
			else if (from.y < to.y && from.x == to.x)
				return Facing.NORTH;
			else if (from.y > to.y && from.x == to.x)
				return Facing.SOUTH;
			
			return Facing.EAST;
		}
	}
}

