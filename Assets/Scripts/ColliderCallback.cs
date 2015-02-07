using UnityEngine;
using System.Collections;

public class ColliderCallback : MonoBehaviour {
	
	void OnCollisionEnter(Collision collision) {
		Debug.Log ("banana");
	}
	
}
