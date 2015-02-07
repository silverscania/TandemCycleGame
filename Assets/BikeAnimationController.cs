using UnityEngine;
using System.Collections;

public class BikeAnimationController : MonoBehaviour {
	public Transform[] rotateyBits;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		foreach(Transform t in rotateyBits) {
			Vector3 current = t.localRotation.eulerAngles;
			t.localRotation = Quaternion.Euler(0,90,Time.time*300);
		}
	}
}
