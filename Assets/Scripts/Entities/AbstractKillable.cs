using UnityEngine;

using System.Collections;


public abstract class AbstractKillable : MonoBehaviour, IKillable {
	
	public abstract void Kill();
}

