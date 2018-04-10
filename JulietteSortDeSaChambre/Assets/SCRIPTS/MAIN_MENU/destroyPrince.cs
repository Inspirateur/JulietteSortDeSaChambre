using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyPrince : MonoBehaviour {

	private SoundManager sm;

	public AudioSource[] SourceAudio;
	public AudioClip Splash;

	void Start () {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		sm.addAudioSource(SourceAudio[0]);
		sm.addAudioSource(SourceAudio[1]);
	}
	void OnTriggerEnter (Collider other) {
		sm.playOneShot(Splash, 0.2f);
		Destroy(other);
	}

}
