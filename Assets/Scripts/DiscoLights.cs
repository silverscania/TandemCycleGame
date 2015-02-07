using UnityEngine;
using System.Collections;

public class DiscoLights : MonoBehaviour {
	
	Color randomColour, anotherRandomColour;
	// Use this for initialization
	void Start () {
		randomColour.r = Random.Range(0.1f, 0.8f);
		randomColour.g = Random.Range(0.1f, 0.8f);
		randomColour.b = Random.Range(0.1f, 0.8f);

		anotherRandomColour.r = Random.Range(0.1f, 0.8f);
		anotherRandomColour.g = Random.Range(0.1f, 0.8f);
		anotherRandomColour.b = Random.Range(0.1f, 0.8f);

	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAround(transform.localPosition, Vector3.up, 10.0f * Time.deltaTime);

		Vector3 newPosition = new Vector3(Mathf.Sin(Time.deltaTime) * 0.005f, Mathf.Cos(Time.deltaTime) * 0.005f, 0.0f);
		transform.Translate(newPosition);

		
		light.color = Color.Lerp(randomColour, anotherRandomColour, Mathf.Sin(Time.time));
	
	}
}
