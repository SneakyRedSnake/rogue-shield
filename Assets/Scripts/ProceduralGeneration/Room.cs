using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Procedural
{
	public class Room
	{
		private Vector2 position;
		private int width = 20;
		private int height = 20;
		private HashSet<Facing> closedBorders;
		private Component[,] components;

		private Facing nextRoomDirection; 

		public Room (Vector2 position)
		{
			this.position = position;
			closedBorders = new HashSet<Facing> ();
			foreach (Facing facing in Enum.GetValues(typeof(Facing))) {
				closedBorders.Add (facing);
			}
			components = new Component[width, height];
			for (int i = 0; i < width; i++) {
				for(int j = 0; j < height; j++) {
					if((j==0) || (j == height -1) || (i == 0) || i == (width - 1))
						components[i,j] = Component.Wall;
				}
			}
		}

		public Vector2 getPosition ()
		{
			return position;
		}

		public void removeClosedBorder (Facing facing)
		{
			closedBorders.Remove (facing);
		}

		public HashSet<Facing> getClosedBorders ()
		{
			return closedBorders;
		}

		public Facing NextRoomDirection {
			get;
			set;
		}

		public int Width {
			get {
				return this.width;
			}
		}

		public int Height {
			get {
				return this.height;
			}
		}

		public void AddComponent(Component component, Vector2 position) {
			components [(int)position.x, (int)position.y] = component;
		}

		public Component[,] Components {
			get  {
				return components;
			}
		}

		public void Scale(int xScale, int yScale) {
			this.width *= xScale;
			this.height *= yScale;
			this.position.x *= xScale;
			this.position.y *= yScale;
		}
	}
}