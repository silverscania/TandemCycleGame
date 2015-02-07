using UnityEngine;
using System.Collections;

public class GeometrySpawn : MonoBehaviour {

	public Transform prefabToCreate;
	public Vector3 offset = new Vector3(20, 0, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log("hey");
		Instantiate(prefabToCreate, transform.parent.transform.position + offset, Quaternion.identity);
	}
}
