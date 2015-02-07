using UnityEngine;
using System.Collections;

public class AIBikeController : BikeController {
	
	// Update is called once per frame
	void Update () {
		setTorqueAndBrakeFront(5, 0);
		setTorqueAndBrakeBack(5, 0);

		if(frame.transform.position.y < -5) {
			Destroy(gameObject);
		}
	}
}
