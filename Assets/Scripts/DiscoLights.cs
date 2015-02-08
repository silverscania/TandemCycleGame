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

		randomColour = HSVToRGB( h1, s, v);
		anotherRandomColour = HSVToRGB( h2, s ,v);

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

	public static Color HSVToRGB(float H, float S, float V)
	{
		if (S == 0f)
			return new Color(V,V,V);
		else if (V == 0f)
			return new Color(0,0,0);
		else
		{
			Color col = Color.black;
			float Hval = H * 6f;
			int sel = Mathf.FloorToInt(Hval);
			float mod = Hval - sel;
			float v1 = V * (1f - S);
			float v2 = V * (1f - S * mod);
			float v3 = V * (1f - S * (1f - mod));
			switch (sel + 1)
			{
			case 0:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 1:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			case 2:
				col.r = v2;
				col.g = V;
				col.b = v1;
				break;
			case 3:
				col.r = v1;
				col.g = V;
				col.b = v3;
				break;
			case 4:
				col.r = v1;
				col.g = v2;
				col.b = V;
				break;
			case 5:
				col.r = v3;
				col.g = v1;
				col.b = V;
				break;
			case 6:
				col.r = V;
				col.g = v1;
				col.b = v2;
				break;
			case 7:
				col.r = V;
				col.g = v3;
				col.b = v1;
				break;
			}
			col.r = Mathf.Clamp(col.r, 0f, 1f);
			col.g = Mathf.Clamp(col.g, 0f, 1f);
			col.b = Mathf.Clamp(col.b, 0f, 1f);
			return col;
		}
	}

}
