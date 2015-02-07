using UnityEngine;
using System.Collections;

public class RagdollController : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//disable ragdoll
		Component[] rigidBodies = GetComponentsInChildren<Rigidbody>();
		foreach (Rigidbody body in rigidBodies) {
			body.constraints = RigidbodyConstraints.FreezeAll;
			body.Sleep ();

		}

	}


	// Update is called once per frame
	void Update () {
	
	}

	void gotCollision() {
		Debug.Log ("collided");
	}
	
}
