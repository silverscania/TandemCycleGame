using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour {

	public WheelCollider frontWheel, backWheel;

	// Use this for initialization
	void Start () {
		frontWheel.brakeTorque = 0;
		frontWheel.motorTorque = 0;
		backWheel.motorTorque = 10;
	}
	
	// Update is called once per frame
	void Update () {
//		backWheel.motorTorque = 10;
//		if (Input.GetKey(KeyCode.W)) {
//			backWheel.motorTorque = 10;
//		}
//		else {
//			backWheel.brakeTorque = 10;
//		}
	}
}
