using UnityEngine;
using System.Collections;

public class GeometrySpawn : MonoBehaviour {

	public Transform prefabToCreate;
	public Transform[] chicken;
	public Transform ai;
	public Vector3 offset = new Vector3(20, 0, 0);

	// Use this for initialization
	void Start () {
		collider.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log (other.name);
		if(other.name .Equals("Main Camera")) {
			collider.enabled = false;
			//Debug.Log("hey");
			Instantiate(prefabToCreate, transform.parent.transform.position + offset, Quaternion.identity);

			spawnChickens();
			spawnAi();
		}
	}

	void spawnChickens() {
		//Spawn random chicken flocks
		int numFlocks = Random.Range(1, 2);
		for(int flock = 0; flock < numFlocks; ++flock){
			Vector3 flockLoc = new Vector3(
				Random.Range(-30, 0),
				4.5f,
				Random.Range(-1, 9));
			
			Instantiate(chicken[(int)Random.Range(0, chicken.Length)], 
			            transform.parent.transform.position + 
			            offset + 
			            flockLoc,
			            Quaternion.Euler(new Vector3(0, Random.Range(0, 180), 0)));
		}
	}

	void spawnAi() {
		int numAi = Random.Range(0,2);
		for(int i = 0; i < numAi; ++i) {
			Vector3 flockLoc = new Vector3(
				Random.Range(-30, 0),
				0.9f,
				Random.Range(-1, 9));
			
			Instantiate(ai, 
			            transform.parent.transform.position + 
			            offset + 
			            flockLoc,
			            Quaternion.Euler(0, 270, 0));
		}
	}
}
