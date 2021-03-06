﻿using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	public BikeController teamOne;
	public BikeController teamTwo;

	public AudioSource[] tracks;
	public AudioClip fanfare;

	public float maxSpeed;
	public float musicVolume = 1;
	private float fastestVelocity;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		fastestVelocity = Mathf.Max (teamOne.frame.velocity.magnitude, teamTwo.frame.velocity.magnitude);
		for (int n = 0 ; n < tracks.Length ; n++) {
			if(fastestVelocity > (maxSpeed / tracks.Length-1) * (n)){
				//tracks[n].volume = 1;
				if(n == 1){
					tracks[n].volume = musicVolume;
				} else {
					tracks[n].volume = musicVolume;
				}
				if(n > 0) tracks[n-1].volume = 0;
			} else if(n>0) {
				tracks[n].volume = 0;
			}
		}
	}

	public void stopAll(){
		for (int n = 0; n < tracks.Length; n++) {
			tracks[n].Stop();
		}
	}

	public void playFanfare(){
		AudioSource.PlayClipAtPoint (fanfare, transform.position, musicVolume);
	}
}
