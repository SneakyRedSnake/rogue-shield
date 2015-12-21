using System;
using UnityEngine;

namespace Procedural
{
	public enum Facing
	{
		NORTH,
		SOUTH,
		EAST,
		WEST,
		NONE
	}

	public static class Extension {
		public static Facing Opposite(this Facing facing) {
			switch (facing) {
			case Facing.EAST:
				return Facing.WEST;
			case Facing.WEST:
				return Facing.EAST;
			case Facing.NORTH:
				return Facing.SOUTH;
			case Facing.SOUTH:
				return Facing.NORTH;

			default:
				return Facing.NONE;
			}
		}
	}

}

