using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAudioSourceToSM : MonoBehaviour {

	private SoundManager sm;

	// Use this for initialization
	void Start () {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		sm.addAudioSource(GetComponent<AudioSource>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
