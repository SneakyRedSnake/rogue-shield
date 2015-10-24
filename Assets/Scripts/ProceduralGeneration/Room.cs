using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Procedural
{
	public class Room
	{
		private Vector2 position;
		private int width = 10;
		private int height = 10;
		private HashSet<Facing> facings;

		public Room (Vector2 position)
		{
			this.position = position;
			facings = new HashSet<Facing> ();
			foreach (Facing facing in Enum.GetValues(typeof(Facing))) {
				facings.Add (facing);
			}
		}

		public Vector2 getPosition ()
		{
			return position;
		}

		public void removeFacing (Facing facing)
		{
			facings.Remove (facing);
		}

		public HashSet<Facing> getFacings ()
		{
			return facings;
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
	}
}