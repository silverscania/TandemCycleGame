using UnityEngine;
using System.Collections;

public class ChickenController : MonoBehaviour {

	public AudioClip[] crashSounds;

	// Use this for initialization
	void Start () {
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//make sound on collision
	void OnCollisionEnter(Collision collision) {
		int randomSound = Random.Range(0, crashSounds.Length);
		audio.PlayOneShot(crashSounds[randomSound], 1.0f);
		audio.Play();

		rigidbody.constraints = RigidbodyConstraints.None;
	}
}
