using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour {

	public WheelCollider frontWheel, backWheel1, backWheel2;
	public Rigidbody frame, balanceWeight;
	public GameObject mark;
	public float maxSpeed = 50.0f;
	public float steerSpeed = 10.0f;
	public float maxSteerAngle = 10.0f;

	// Joy input
	private float lastAngleRight;    // Angle at the last update
	private float deltaAngleRight;
	private float currentAngleRight;

	private float lastAngleLeft;    // Angle at the last update
	private float deltaAngleLeft;
	private float currentAngleLeft;

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


		// Joystick input
		Vector3 rotationLeft = new Vector3 (0, Mathf.Atan2 (Input.GetAxis ("LeftVertical"), Input.GetAxis ("LeftHorizontal")) * -180 / Mathf.PI, 0);
		Vector3 rotationRight = new Vector3 (0, Mathf.Atan2 (Input.GetAxis ("RightVertical"), Input.GetAxis ("RightHorizontal")) * -180 / Mathf.PI, 0);

		currentAngleLeft = rotationLeft.y;
		currentAngleRight = rotationRight.y;

//		if (Mathf.RoundToInt (currentAngleLeft) != Mathf.RoundToInt (lastAngleLeft)) {
//
//			// only do something if current and last angle er either both positive or both negative
//			if ((currentAngle < 0 && lastAngle < 0) ||
//			    (currentAngle > 0 && lastAngle > 0) ) {
//
//				deltaAngle = lastAngle - currentAngle;
//				//rotateAmount += deltaAngle;
//				setTorqueAndBrake(deltaAngle, 0);
//			}
//		}
//		lastAngle = currentAngle;
	}

	void OnGUI(){
		//GUI.Label (new Rect (0, Screen.height / 2, Screen.width, Screen.height), "Angle: " + currentAngle + " Delta: " + deltaAngle + "\nAmount: " + rotateAmount);
	}

	void setTorqueAndBrake(float torque, float brake) {
		backWheel1.motorTorque = backWheel2.motorTorque = torque;
		backWheel1.brakeTorque = backWheel2.brakeTorque = brake;
	}

	void setTorqueAndBrakeFront(float torque, float brake) {
		frontWheel.motorTorque = torque;
		frontWheel.brakeTorque = brake;
	}

	void setTorqueAndBrakeBack(float torque, float brake) {
		backWheel1.motorTorque = backWheel2.motorTorque = torque;
		backWheel1.brakeTorque = backWheel2.brakeTorque = brake;
	}
}


