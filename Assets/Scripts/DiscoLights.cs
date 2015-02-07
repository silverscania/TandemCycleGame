using UnityEngine;
using System.Collections;

public class DiscoLights : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAround(transform.localPosition, Vector3.up, 10.0f * Time.deltaTime);

		Vector3 newPosition = new Vector3(Mathf.Sin(Time.deltaTime) * 0.005f, Mathf.Cos(Time.deltaTime) * 0.005f, 0.0f);
		transform.Translate(newPosition);

		Color newColour = new Color(0.5f + Mathf.Cos(Time.deltaTime) * 0.1f, 0.5f - Mathf.Sin(Time.deltaTime) * 0.1f, 0.5f + Mathf.Cos(Time.deltaTime) * 0.1f);
		
		light.color = newColour;
	
	}
}
