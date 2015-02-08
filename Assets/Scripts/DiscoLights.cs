using UnityEngine;
using System.Collections;

public class DiscoLights : MonoBehaviour {
	
	Color randomColour, anotherRandomColour;
	public Rigidbody frame;
	// Use this for initialization
	void Start () {
		float h1, h2 ,s,v;

		h1 = Random.Range(0.65f, 1.0f);
		h2 = Random.Range(0.65f, 1.0f);

		s = 0.8f;
		v = 0.7f;

		randomColour = UnityEditor.EditorGUIUtility.HSVToRGB( h1, s, v);
		anotherRandomColour = UnityEditor.EditorGUIUtility.HSVToRGB( h2, s ,v);

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
