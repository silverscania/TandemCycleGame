using UnityEngine;
using System.Collections;

public class GameLord : MonoBehaviour {

	public BikeController team1;
	public BikeController team2;

	public float winDistance; // If the teams are this far apart, crash the back one and the first one wins

	public bool gameOver = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Mathf.Abs (team1.frame.transform.localPosition.z - team2.frame.transform.localPosition.z);
		if (distance >= winDistance) {
			gameOver = true;
		}
	}

	void OnGUI(){
		if (gameOver) {
			GUI.Label (new Rect (0, Screen.height / 2, Screen.width, Screen.height), "GAME OVER! ");
		}
		GUI.Label (new Rect (0, Screen.height / 2 + 20, Screen.width, Screen.height), Time.time + "  " + Mathf.Abs (team1.frame.transform.localPosition.z - team2.frame.transform.localPosition.z));
	}
}
