using UnityEngine;
using System.Collections;

public class GameLord : MonoBehaviour {
	
	public BikeController team1;
	public BikeController team2;

	public Camera guiCamera;

	public GameObject title;
	public GameObject endText;

	public TextMesh winnerName;
	public TextMesh countDown;

	public AudioClip countSound;
	public AudioClip goSound;

	public float winDistance; // If the teams are this far apart, crash the back one and the first one wins

	public bool gameOver = false;

	private int countDownTime = 4;

	// Use this for initialization
	void Start () {
		title.SetActive (true);
		endText.SetActive (false);

		team1.enabled = false;
		team2.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Mathf.Abs (team1.frame.transform.localPosition.z - team2.frame.transform.localPosition.z);
		BikeController winner = team1.frame.transform.localPosition.z > team2.frame.transform.localPosition.z ? team1 : team2;

		if (distance >= winDistance) {
			gameOver = true;
			endText.SetActive(true);

			team1.brakeToStop();
			team2.brakeToStop();
			team1.enabled = false;
			team2.enabled = false;

			if(winner.alive){
				winnerName.text = winner.gameObject.name;
			} else {
				winnerName.text = "No one";
			}
		}

		if (Input.GetButtonUp ("Fire1") && title.activeSelf) {
			// Start the game
			title.SetActive(false);
			countDownGo();
		}

		if (Input.GetButtonUp ("Fire1") && gameOver) {
			Application.LoadLevel("main 4");
		}

		if (Input.GetButtonUp ("Cancel")) {
			Application.Quit();
		}
	}

	private void count(){
		countDownTime--;

		if (countDownTime == 0) {
			countDown.text = "GO!";
			AudioSource.PlayClipAtPoint(goSound, transform.position, 0.9f);
		} else if (countDownTime == -1) {
			countDown.text = "";
			team1.enabled = true;
			team2.enabled = true;
			CancelInvoke();
		} else {
			countDown.text = countDownTime + "";
			AudioSource.PlayClipAtPoint(countSound, transform.position, 1f);
		}
	}

	private void countDownGo(){
		InvokeRepeating("count", 1f, 1f);
	}

	void OnGUI(){
//		if (gameOver) {
//			GUI.Label (new Rect (0, Screen.height / 2, Screen.width, Screen.height), "GAME OVER! ");
//		}
//		GUI.Label (new Rect (0, Screen.height / 2 + 20, Screen.width, Screen.height), Time.time + "  " + Mathf.Abs (team1.frame.transform.localPosition.z - team2.frame.transform.localPosition.z));
	}
}
