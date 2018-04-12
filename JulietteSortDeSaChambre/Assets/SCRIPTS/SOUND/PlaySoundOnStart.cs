using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour {

	private SoundEntity se;

	void Start() {
		se = GetComponent<SoundEntity>();
		se.playOneShot(0);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
