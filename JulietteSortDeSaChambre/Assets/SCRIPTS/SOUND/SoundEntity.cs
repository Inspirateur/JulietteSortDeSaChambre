using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEntity : MonoBehaviour {

	public AudioClip[] listeClips;

	private AudioSource audio;
	private SoundManager sm;

	public int volumeGeneral;

	void Awake() {
		sm = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager>();
		audio = GetComponent<AudioSource>();
		//volumeGeneral=PlayerPrefs.GetInt("volumeGeneral",volumeGeneral);
		volumeGeneral = sm.volumeGeneral;
	}

	// Use this for initialization
	void Start () {
		
		// Debug.Log(audio);
		sm.addAudioSource (this.audio);
		sm.onVolumeChange+=onVolumeChange;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void stop() {
		audio.Stop();
	}

	public void pause() {
		audio.Pause ();
	}

	public void resume() {
		audio.UnPause ();
	}

	public bool isPlaying() {
		return audio.isPlaying;
	}

	public void playOneShot(AudioClip music) {
		playOneShot (music, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume) {
		playOneShot (music, volume, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume, float pitch) {
		// Debug.Log(audio);
		audio.pitch = pitch;
		audio.PlayOneShot (music, volume*(volumeGeneral/10));//tweak
	}

	public void playOneShot(int indice) {
		playOneShot (indice, 1.0f);
	}

	public void playOneShot(int indice, float volume) {
		playOneShot (indice, volume, 1.0f);
	}

	public void playOneShot(int indice, float volume, float pitch) {
		playOneShot (listeClips [indice], volume, pitch);
	}

	public int nbClips() {
		return listeClips.Length;
	}

	public void onVolumeChange(){
		volumeGeneral= sm.volumeGeneral;
		if(audio!=null){
			audio.volume=volumeGeneral;
		}
	}
}
