using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour
{
	[Tooltip("Put here Wall Prefab ! ")]
	public Transform wall;
	public int minimumDistance = 6;
	public int maximumDistance = 10;
	public Vector2 endPosition = new Vector2(6, 6);
	public int levelWidth = 10;
	public int levelHeight = 10;
	
	private Room[,] level;
    private List<Vector2> layout;

	private Transform environment; 				// Stores the roo object for the environment

    enum Facing
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    // Use this for initialization
    void Start()
    {
		int nbIter = 0;
		float elapsedTime = Time.realtimeSinceStartup;

		GameObject obj = new GameObject ("Environment");
		environment = obj.GetComponent<Transform> ();

        level = new Room[levelWidth, levelHeight];
		layout = new List<Vector2> (10);
		while (findPath(0, 0, minimumDistance) == false) {
			nbIter ++;
			if(nbIter > 2) {
				Debug.Log("Path not found");
				break;

			}
		}

		elapsedTime = Time.realtimeSinceStartup - elapsedTime;
		Debug.Log ("Number of iteration : " + nbIter);
		Debug.Log ("Generation time : " + elapsedTime);
        Debug.Log ("Number of Rooms : " + layout.Count);
		Debug.Log ("Rooms Layout: ");
		foreach(Vector2 v in layout) {
			Debug.Log(v);
			generateSquare(10, 10, level[(int)v.x, (int)v.y].getPosition() * 10);
		}
		
    }

    private bool findPath(int x, int y, int minDistance)
    {
        Debug.Log("Starting find Path : x = " + x +"   y = " + y + "    distance = " + minDistance);

        if ((x == endPosition.x && y == endPosition.y) && (minDistance <= 0) && (minDistance > -(maximumDistance - minimumDistance))) {
			addRoomToLayout (x, y);
			return true;
		}
            
        if (!validPosition(x, y))
            return false;

		addRoomToLayout (x, y);

        Facing r = (Facing)Random.Range(0, 4);
        Debug.Log("r : " + r);
        switch (r)
        {
            case Facing.NORTH:
                if (findPath(x, y + 1, minDistance - 1) == true) return true; 
				goto case Facing.SOUTH;

            case Facing.SOUTH:
                if (findPath(x, y - 1, minDistance - 1) == true) return true;
				goto case Facing.EAST;
                
            case Facing.EAST:
                if (findPath(x + 1, y, minDistance - 1) == true) return true;
				goto case Facing.WEST;

            case Facing.WEST:
                if (findPath(x - 1, y, minDistance - 1) == true) return true;
                break;

            default:
                Debug.Log("Error");
                break;
        }
        layout.RemoveAt(layout.Count - 1);
		level [x, y] = null;
        return false;
    }

	private void addRoomToLayout(int x, int y) {
		layout.Add(new Vector2(x, y));
		level [x, y] = new Room (new Vector2 (x, y));
	}

    private bool validPosition(int x, int y)
    {
		foreach(Vector2 position in layout) {
				if(position.x == x && position.y == y)
					return false;
		}
        return (((x >= 0) && (x < levelWidth)) && ((y >= 0) && (y < levelHeight)));
    }


	private void instanciateWall(Vector2 position) {
		Transform wallInstance = (Transform) Instantiate(wall, new Vector3(position.x, position.y, 0), Quaternion.identity);
		wallInstance.localScale = new Vector2 (0.5f, 0.5f);
		wallInstance.SetParent(environment);
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
}
