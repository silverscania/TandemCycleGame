using UnityEngine;
using System.Collections;

public class ChaseCamera : MonoBehaviour {

	public Transform target1, target2;
	public Vector3 offset;
	public float minZoom, maxZoom, zoomRange;

	// Use this for initialization
	void Start () {
		offset = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = offset;

		Transform target;// = target1.transform.position.x < target2.transform.position.x ? target1 : target2;
		Transform behind; 
		if(target1 && target2 && target1.transform.position.x < target2.transform.position.x) {
			target = target1;
			behind = target2;
		}
		else {
			target = target2;
			behind = target1;
		}

		//Center on last rider
		if(target == null) {
			target = behind;
		}
		if(behind == null) {
			behind = target;
		}

		transform.position = new Vector3(
			(behind.transform.position.x + target.transform.position.x*3)/4.0f + offset.x,
			offset.y,
			offset.z);
		
		float distance = Mathf.Abs(target.transform.position.x - behind.transform.position.x);
		camera.orthographicSize = Mathf.Lerp(minZoom, maxZoom, distance/zoomRange);
	}
}
