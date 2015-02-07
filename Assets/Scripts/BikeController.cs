using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour {

	public bool playerTwo;

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

	private string playerName = "P1";

	// Use this for initialization
	void Start () {
		mark = GameObject.Find("Mark");

		if (playerTwo) {
			playerName = "P2";
		}
	}
	
	// Update is called once per frame
	void Update () {
		float speed = frame.rigidbody.velocity.magnitude;

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

		// Joystick input
		Vector3 rotationLeft = new Vector3 (0, Mathf.Atan2 (Input.GetAxis (playerName + "LeftVertical"), Input.GetAxis (playerName + "LeftHorizontal")) * -180 / Mathf.PI, 0);
		Vector3 rotationRight = new Vector3 (0, Mathf.Atan2 (Input.GetAxis (playerName + "RightVertical"), Input.GetAxis (playerName + "RightHorizontal")) * -180 / Mathf.PI, 0);

		currentAngleLeft = rotationLeft.y;
		currentAngleRight = rotationRight.y;

		// RIGHT STICK
		if (Mathf.RoundToInt (currentAngleRight) != Mathf.RoundToInt (lastAngleRight)) {
			// only do something if current and last angle er either both positive or both negative
			if ((currentAngleRight < 0 && lastAngleRight < 0) ||
			    (currentAngleRight > 0 && lastAngleRight > 0) ) {
				
				deltaAngleRight = lastAngleRight - currentAngleRight;
				//rotateAmount += deltaAngle;
				setTorqueAndBrakeFront(deltaAngleRight/10, 0);
			}
		}

		// LEFT STICK
		if (Mathf.RoundToInt (currentAngleLeft) != Mathf.RoundToInt (lastAngleLeft)) {
			if ((currentAngleLeft < 0 && lastAngleLeft < 0) ||
			    (currentAngleLeft > 0 && lastAngleLeft > 0) ) {
				
				deltaAngleLeft = lastAngleLeft - currentAngleLeft;
				//rotateAmount += deltaAngle;
				setTorqueAndBrakeBack(deltaAngleLeft/10, 0);
			}
		}
		// remember latest angles
		lastAngleRight = currentAngleRight;
		lastAngleLeft = currentAngleLeft;

		
		// RIGHT TRIGGER
		float rightTrigger = Input.GetAxis (playerName + "RightTrigger");
		frontWheel.steerAngle += steerSpeed*Time.deltaTime * rightTrigger;

		// LEFT TRIGGER
		float leftTrigger = Input.GetAxis (playerName + "LeftTrigger");
		frontWheel.steerAngle -= steerSpeed*Time.deltaTime * leftTrigger;	

		frontWheel.steerAngle = Mathf.Clamp(frontWheel.steerAngle, -maxSteerAngle, maxSteerAngle);

		// gradually straighten up the bike
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


