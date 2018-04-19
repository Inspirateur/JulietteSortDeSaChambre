using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicBackground : MonoBehaviour {

	private SoundManager sm;

	public AudioClip[] Play;

	// Use this for initialization
	void Start () {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
	}
	
	// Update is called once per frame
	public void ChangeSong (int num) {
		sm.setBackgroundMusic(Play[num]);
	}
}
