using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSoundCollide : MonoBehaviour {

	private SoundManager sm;

	// Use this for initialization
	void Start () {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			sm.pauseAllMusic();
			this.enabled = false;
		}
	}

}
