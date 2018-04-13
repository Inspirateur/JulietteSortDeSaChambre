using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySongLoop : MonoBehaviour {

	private SoundManager sm;

	public AudioClip[] Play;

	// Use this for initialization
	void Start () {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		sm.setBackgroundMusic(Play[0]);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
