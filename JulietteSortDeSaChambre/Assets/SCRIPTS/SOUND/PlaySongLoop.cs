using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySongLoop : MonoBehaviour {

	private SoundManager sm;

	public int NumberMusicSM;

	// Use this for initialization
	void Start () {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		sm.setBackgroundMusic(sm.listeClips[NumberMusicSM]);
		CheckPointManager.getInstance().onRestart += OnRestart;
	}

	public void OnRestart(){
		sm.setBackgroundMusic(sm.listeClips[NumberMusicSM]);
	}
}
