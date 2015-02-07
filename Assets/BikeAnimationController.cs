using UnityEngine;
using System.Collections;

public class BikeAnimationController : MonoBehaviour {
	public Transform[] rotateyBits;
	public Transform[] xRotateyBits;

	public WheelCollider backWheel;
	protected float angle;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		angle += backWheel.rpm * Time.deltaTime;
		angle = angle % 360;
		foreach(Transform t in rotateyBits) {
			t.localRotation = Quaternion.Euler(0,90,angle*8);
		}
		foreach(Transform t in xRotateyBits) {
			t.localRotation = Quaternion.Euler(angle*360/60,0,0);
		}
	}
}
