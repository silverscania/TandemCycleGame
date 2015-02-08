using UnityEngine;
using System.Collections;

public class DiscoLights : MonoBehaviour {
	
	Color randomColour, anotherRandomColour;
	public Rigidbody frame;
	// Use this for initialization
	void Start () {
		randomColour.r = Random.Range(0.65f, 1.0f);
		randomColour.g = Random.Range(0.65f, 1.0f);
		randomColour.b = Random.Range(0.65f, 1.0f);

		anotherRandomColour.r = Random.Range(0.6f, 1.0f);
		anotherRandomColour.g = Random.Range(0.6f, 1.0f);
		anotherRandomColour.b = Random.Range(0.65f, 1.0f);

	}
	
	// Update is called once per frame
	void Update () {
		if(frame.rigidbody.velocity.magnitude > 4.5) {
			light.enabled = true;
			transform.localRotation = Quaternion.Euler(90, Time.time*100, 0);
			light.color = Color.Lerp(randomColour, anotherRandomColour, Mathf.Sin(Time.time));
		}
		else {
			light.enabled = false;
		}
		//transform.RotateAround(transform.localPosition, Vector3.up, 10.0f * Time.deltaTime);
		//Vector3 newPosition = new Vector3(Mathf.Sin(Time.deltaTime) * 0.005f, Mathf.Cos(Time.deltaTime) * 0.005f, 0.0f);
		//transform.Translate(newPosition);
	}
}
