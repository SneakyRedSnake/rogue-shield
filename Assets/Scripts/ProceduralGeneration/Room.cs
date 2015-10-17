using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room
{
    enum Facing
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

	private Vector2 position;
    private int width = 40;
    private int height = 40;
    private HashSet<Facing> facings;

    public Room(Vector2 position)
    {
		this.position = position;
    }

	public Vector2 getPosition() {
		return position;
	}


}
