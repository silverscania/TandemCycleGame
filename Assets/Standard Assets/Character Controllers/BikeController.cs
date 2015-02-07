﻿using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour {

	public WheelCollider frontWheel, backWheel1, backWheel2;
	public Rigidbody frame, balanceWeight;
	public GameObject mark;
	public float maxSpeed = 50.0f;
	public float steerSpeed = 10.0f;
	public float maxSteerAngle = 10.0f;

	// Use this for initialization
	void Start () {
		mark = GameObject.Find("Mark");
	}
	
	// Update is called once per frame
	void Update () {
		float speed = frame.rigidbody.velocity.magnitude;

		//Get desired angle based on rotational speed
		//float rotSpeed = frame.angularVelocity.y;//*Time.deltaTime;
		//float bankAngle = -rotSpeed*3; //*speed;

		//mark.gameObject.transform.localRotation = Quaternion.Euler(0,0,bankAngle);
		//mark.transform.rotation = gameObject.transform.rotation;// * Quaternion.Euler(0, 0, 0);
		//Debug.Log(bankAngle);

		//float rotation = Mathf.Clamp(frame.rigidbody.rotation.z-bankAngle, -30, 30);
		//float torque = -rotation*20*speed; //0*speed;//*Time.deltaTime;

		//balanceWeight.gameObject.transform.localPosition = new Vector3(torque, 0, 0);


		//torque = Mathf.Min(100, torque);
		//frame.AddTorque(new Vector3(0,0, torque));
	//	frame.rigidbody.inertiaTensorRotation = Quaternion.Euler(0,0,0);
	//	frame.rigidbody.angularVelocity = new Vector3();

		if (Input.GetKey(KeyCode.W)) {
			if (speed < maxSpeed) {
				setTorqueAndBrake(10, 0);
			}
			else {
				setTorqueAndBrake(0,0);
			}
		}
		else {
			setTorqueAndBrake(0, 20);
		}

		//JointSpring hingeSpring = balanceWeight.hingeJoint.spring;
		if(Input.GetKey(KeyCode.A)) {
			frontWheel.steerAngle += -steerSpeed*Time.deltaTime;
			//hingeSpring.targetPosition = 10;
		}
		else if(Input.GetKey(KeyCode.D)) {
			frontWheel.steerAngle += steerSpeed*Time.deltaTime;
			//hingeSpring.targetPosition = 10;
		}
		else {
			frontWheel.steerAngle *= 0.8f; //TODO: not linked to Time.delta
		//	hingeSpring.targetPosition = 0;
		}

		//frame.rigidbody.AddRelativeTorque(0,0, -frontWheel.steerAngle*3);
		//frame.rigidbody.centerOfMass = new Vector3(-frontWheel.steerAngle, 0, 0);
	
		//balanceWeight.hingeJoint.spring = hingeSpring;

		frontWheel.steerAngle = Mathf.Clamp(frontWheel.steerAngle, -maxSteerAngle, maxSteerAngle);
	}

	void setTorqueAndBrake(float torque, float brake) {
		backWheel1.motorTorque = backWheel2.motorTorque = torque;
		backWheel1.brakeTorque = backWheel2.brakeTorque = brake;
	}
}
