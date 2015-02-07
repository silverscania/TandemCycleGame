using UnityEngine;
using System.Collections;

public class GeometrySpawn : MonoBehaviour {

	public Transform prefabToCreate;
	public Transform[] chicken;
	public Vector3 offset = new Vector3(20, 0, 0);

	// Use this for initialization
	void Start () {
		collider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log (other.name);

		collider.enabled = false;
		//Debug.Log("hey");
		Instantiate(prefabToCreate, transform.parent.transform.position + offset, Quaternion.identity);

		//Spawn random chicken flocks
		int numFlocks = Random.Range(1, 2);
		for(int flock = 0; flock < numFlocks; ++flock){
			Vector3 flockLoc = new Vector3(
				Random.Range(-80, 0),
				4.5f,
				Random.Range(-10, 10));

			Instantiate(chicken[(int)Random.Range(0, chicken.Length)], 
		            transform.parent.transform.position + 
		            offset + 
			        flockLoc,
		            Quaternion.Euler(new Vector3(0, Random.Range(0, 180), 0)));
		}
	}
}
