using System;
using System.Collections.Generic;
using UnityEngine;

namespace Procedural
{
	public class Level
	{
		private int levelWidth = 10;
		private int levelHeight = 10;
		private List<Vector2> layout;
		private Room[,] rooms;
		private Vector2 endPosition = new Vector2 (6, 6);

		public Level (int levelWidth, int levelHeight, List<Vector2> layout, Room[,] rooms)
		{
			this.levelWidth = levelWidth;
			this.levelHeight = levelHeight;
			this.layout = layout;
			this.rooms = rooms;
		}

		public List<Vector2> Layout {
			get {
				return this.layout;
			}
		}

		public Room[,] Rooms {
			get {
				return this.rooms;
			}
		}
	}
}

