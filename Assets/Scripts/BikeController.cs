using UnityEngine;
using System.Collections;

public class BikeController : MonoBehaviour {

	public bool playerTwo;

	public WheelCollider frontWheel1, frontWheel2, backWheel1, backWheel2;
	public Rigidbody frame, balanceWeight;
	public GameObject mark;
	public AudioSource audioSourceClick;
	public AudioSource audioSourcePedal;
	public float maxSpeed = 50.0f;
	public float steerSpeed = 10.0f;
	public float maxSteerAngle = 10.0f;
	public float straightAmount; // how quickly the bikes straighten up
	public float brakeAmount = 20; // How hard to brake when not moving controls
	public float brakeThreshold = 1; // How long time before we brake when not giving proper input
	public float speedModifier = 0.1f; // 0-1

	public bool alive = true;

	// Joy input

	private float lastAngleRight;    // Angle at the last update
	private float deltaAngleRight;
	private float currentAngleRight;

	private float lastAngleLeft;    // Angle at the last update
	private float deltaAngleLeft;
	private float currentAngleLeft;

	private string playerName = "P1";

	private float lastGoodUpdate; // point in time where we last moved forward (brake when it's been half a second or so)

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

		if(frame.transform.position.y < -5) {
			Destroy(gameObject);
			return;
		}

		//JointSpring hingeSpring = balanceWeight.hingeJoint.spring;
		if(Input.GetKey(KeyCode.A)) {
			frontWheel1.steerAngle += -steerSpeed*Time.deltaTime;
			frontWheel2.steerAngle = frontWheel1.steerAngle;
			//hingeSpring.targetPosition = 10;
		}
		else if(Input.GetKey(KeyCode.D)) {
			frontWheel1.steerAngle += steerSpeed*Time.deltaTime;
			frontWheel2.steerAngle = frontWheel1.steerAngle;
			//hingeSpring.targetPosition = 10;
		}
		else {
			frontWheel1.steerAngle *= 0.8f; //TODO: not linked to Time.delta
			frontWheel2.steerAngle = frontWheel1.steerAngle;
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
				(currentAngleRight > 0 && lastAngleRight > 0)) {
				
				deltaAngleRight = lastAngleRight - currentAngleRight;
				//rotateAmount += deltaAngle;
				setTorqueAndBrakeFront (-deltaAngleRight * speedModifier, 0);
			}
		} else {
			//setTorqueAndBrakeFront(0,brakeAmount);
			deltaAngleRight = 0;
		}

		// LEFT STICK
		if (Mathf.RoundToInt (currentAngleLeft) != Mathf.RoundToInt (lastAngleLeft)) {
			if ((currentAngleLeft < 0 && lastAngleLeft < 0) ||
				(currentAngleLeft > 0 && lastAngleLeft > 0)) {
				
				deltaAngleLeft = lastAngleLeft - currentAngleLeft;
				setTorqueAndBrakeBack (-deltaAngleLeft * speedModifier, 0);
			}
		} else {
			// slow down
			//setTorqueAndBrakeBack(0,brakeAmount);
			deltaAngleLeft = 0;
		}

		//clamp speed
		if(speed > maxSpeed) {
			setTorqueAndBrakeBack(0,0);
			setTorqueAndBrakeFront(0,0);
		}

		// is this a 'good update', i.e. both players moving and moving in the same direction
		if (deltaAngleLeft < 0 && deltaAngleRight < 0) {
			lastGoodUpdate = Time.time;
		}

		// Brake if players don't know how to ride a tandem bike
		if ((Time.time - lastGoodUpdate) > brakeThreshold) {
			//Debug.Log ("BRAKE!!! " + (Time.time - lastGoodUpdate));
			setTorqueAndBrakeBack(0, brakeAmount);
			setTorqueAndBrakeFront(0, brakeAmount);
		}

		playBikeSounds ();

		// remember latest angles
		lastAngleRight = currentAngleRight;
		lastAngleLeft = currentAngleLeft;

		
		// RIGHT TRIGGER
		float rightTrigger = Input.GetAxis (playerName + "RightTrigger");
		if (rightTrigger != 0) {
			frontWheel1.steerAngle += steerSpeed * Time.deltaTime * rightTrigger;
			frontWheel2.steerAngle = frontWheel1.steerAngle;
		}

		// LEFT TRIGGER
		float leftTrigger = Input.GetAxis (playerName + "LeftTrigger");
		if (leftTrigger != 0) {
			frontWheel1.steerAngle -= steerSpeed * Time.deltaTime * leftTrigger;
			frontWheel2.steerAngle = frontWheel1.steerAngle;
		}

		frontWheel1.steerAngle = Mathf.Clamp(frontWheel1.steerAngle, -maxSteerAngle, maxSteerAngle);
		frontWheel2.steerAngle = frontWheel1.steerAngle;

		// gradually straighten up the bike
		if (leftTrigger == 0 && rightTrigger == 0) {
			// Going right
			if (frame.transform.eulerAngles.y > 270) {
				frontWheel1.steerAngle -= straightAmount * Time.deltaTime;
				frontWheel2.steerAngle = frontWheel1.steerAngle;
			}
			// Going left
			if (frame.transform.eulerAngles.y < 270) {
				frontWheel1.steerAngle += straightAmount * Time.deltaTime;
				frontWheel2.steerAngle = frontWheel1.steerAngle;
			}
		}

		if(Input.GetKey(KeyCode.W)) {
			setTorqueAndBrakeFront(5, 0);
			setTorqueAndBrakeBack(5, 0);
		}
	}

	float audioClickTime = 2;
	private void playBikeSounds(){
		// Wheel click
		if (audioSourceClick && audioClickTime <= 0) {
			audioClickTime = 2;
			audioSourceClick.Play();
		}
		audioClickTime -= frame.velocity.magnitude / 10;

		// Pedal whoosh
//		if (currentAngleLeft == 90 && lastAngleLeft != currentAngleLeft) {
//			audioSourcePedal.Play();
//		}
//
//		if (currentAngleRight == 90 && lastAngleRight != currentAngleRight) {
//			audioSourcePedal.Play();
//		}
	}

	void OnGUI(){
		if (!playerTwo) {
			//	GUI.Label (new Rect (0, Screen.height / 2, Screen.width, Screen.height), "Angle: " + frame.transform.eulerAngles.y);
			//GUI.Label (new Rect (0, Screen.height / 2, Screen.width, Screen.height), "DeltaAngleLeft: " + deltaAngleLeft + "\nDeltaAngleRight: " + deltaAngleRight + "\nLastGoodUpdate: " + lastGoodUpdate + "\nTime: " + (Time.time - lastGoodUpdate));
			GUI.Label (new Rect (0, Screen.height / 2, Screen.width, Screen.height), "Vel: " + frame.velocity.magnitude);
		}
	}

	protected void setTorqueAndBrake(float torque, float brake) {
		backWheel1.motorTorque = backWheel2.motorTorque = torque;
		backWheel1.brakeTorque = backWheel2.brakeTorque = brake;
	}

	protected void setTorqueAndBrakeFront(float torque, float brake) {
		torque = Mathf.Clamp (torque, -maxSpeed, maxSpeed);
		frontWheel1.motorTorque = frontWheel2.motorTorque = torque;
		frontWheel1.brakeTorque = frontWheel2.brakeTorque = brake;
	}

	protected void setTorqueAndBrakeBack(float torque, float brake) {
		torque = Mathf.Clamp (torque, -maxSpeed, maxSpeed);
		backWheel1.motorTorque = backWheel2.motorTorque = torque/2;
		backWheel1.brakeTorque = backWheel2.brakeTorque = brake;
	}

	public void brakeToStop(){
		setTorqueAndBrakeBack (0, brakeAmount / 5);
		setTorqueAndBrakeFront (0, brakeAmount / 5);
	}

}


