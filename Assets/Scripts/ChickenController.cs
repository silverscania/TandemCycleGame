using UnityEngine;
using System.Collections;

public class ChickenController : MonoBehaviour {

	public AudioClip[] crashSounds;
	protected float collisionTime;
	// Use this for initialization
	void Start () {
		//rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		collider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(collider.enabled == false && Time.time > collisionTime + 5) {
			Destroy(gameObject);
		}
		if(transform.position.y < -5 || transform.position.y > 10) {
			Destroy(gameObject);
		}
	}

	//make sound on collision
	void OnCollisionEnter(Collision collision) {
		Debug.Log(collision.relativeVelocity.sqrMagnitude);
		if( collision.relativeVelocity.sqrMagnitude > 20) {
			collider.enabled = false;
			collisionTime = Time.time;

			int randomSound = Random.Range(0, crashSounds.Length);
			audio.PlayOneShot(crashSounds[randomSound], 1.0f);
			audio.Play();

			//rigidbody.constraints = RigidbodyConstraints.None;
			//rigidbody.AddExplosionForce(100.0f, collision.transform.position, 5.0f);
			rigidbody.AddForce(new Vector3(-20, 150, 0));
		}
	}
}
