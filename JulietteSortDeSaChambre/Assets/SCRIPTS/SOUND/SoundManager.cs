﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip[] listeClips;
	public AudioClip defaultLevelMusic;
	public float dureeTransition;

	public AudioSource levelAudioSingle;
	public AudioSource levelAudioLoop1;
	public AudioSource levelAudioLoop2;

	private List<AudioSource> listAudioSource;

	private bool changeMusicTo2;
	private bool changeMusicTo1;
	private Queue<AudioClip> musicQueue;

	private float timer;
	private int i;
	private bool paused;

	public int volumeGeneral;

	public event MyDelegateVolume onVolumeChange;

	public delegate void MyDelegateVolume();

	// Use this for initialization
	void Awake() {
		levelAudioLoop1.clip = defaultLevelMusic;
		levelAudioLoop1.Play ();

		levelAudioLoop2.volume = 0.0f;

		listAudioSource = new List<AudioSource> ();
		musicQueue = new Queue<AudioClip> ();
		changeMusicTo2 = false;
		changeMusicTo1 = false;

		i = 0;
		paused = false;
		volumeGeneral=PlayerPrefs.GetInt("volumeGeneral",volumeGeneral);//sur 10
		float newVol = volumeGeneral;
		newVol/=10;
		levelAudioLoop2.volume = newVol;
		levelAudioLoop1.volume = newVol;
		levelAudioSingle.volume= newVol;
	}
	
	// Update is called once per frame
	void Update () {

		// if (Input.GetKeyDown (KeyCode.O)) {
		// 	i = (i + 1) % listeClips.Length;
		// 	setBackgroundMusic (i);
		// }

		// if (Input.GetKeyDown (KeyCode.P)) {
		// 	if (paused) {
		// 		paused = false;
		// 		resumeAllSound ();
		// 	} else {
		// 		paused = true;
		// 		pauseAllSound ();
		// 	}
		// }
		
		if (!changeMusicTo2 && !changeMusicTo1 && musicQueue.Count > 0) {

			timer = Time.time;

			if (levelAudioLoop1.volume == 1.0f*(volumeGeneral/10)) {//tweak le 1f
				changeMusicTo2 = true;
				levelAudioLoop2.clip = musicQueue.Dequeue ();
				levelAudioLoop2.volume = 1.0f*(volumeGeneral/10);
				levelAudioLoop2.Play ();
			} else {
				levelAudioLoop1.volume = 1.0f*(volumeGeneral/10);
				changeMusicTo1 = true;
				levelAudioLoop1.clip = musicQueue.Dequeue ();
				levelAudioLoop1.Play ();
			}
		}

		if (changeMusicTo2) {
			bool end = switchMusicFromTo (levelAudioLoop1, levelAudioLoop2);
			if (end) {
				changeMusicTo2 = false;
				levelAudioLoop1.Stop ();
			}
		}

		if (changeMusicTo1) {
			bool end = switchMusicFromTo (levelAudioLoop2, levelAudioLoop1);
			if (end) {
				changeMusicTo1 = false;
				levelAudioLoop2.Stop ();
			}
		}
	}

	public void addAudioSource(AudioSource source) {
		listAudioSource.Add (source);
	}

	public void pauseAllSound() {
		this.pauseAllSoundExceptMusic();
		levelAudioSingle.Pause ();
		levelAudioLoop1.Pause ();
		levelAudioLoop2.Pause ();
	}

	public void pauseAllSoundExceptMusic() {
		foreach(AudioSource a in listAudioSource) {
			if(a!=null){
				a.Pause ();
			}
		}
	}

	public void pauseAllMusic() {
		levelAudioLoop1.Pause();
		levelAudioLoop2.Pause();
	}

	public void resumeAllSound() {
		foreach(AudioSource a in listAudioSource) {
			if(a!=null){
				a.Pause ();
			}
		}
		levelAudioSingle.UnPause ();
		levelAudioLoop1.UnPause ();
		levelAudioLoop2.UnPause ();
	}

	public void setBackgroundMusic(int indice) {
		setBackgroundMusic (listeClips [indice]);
	}

	public void setBackgroundMusic(AudioClip music) {
		musicQueue.Enqueue (music);
	}

	public void playOneShot(AudioClip music) {;
		playOneShot (music, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume) {
		playOneShot (music, volume, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume, float pitch) {
		levelAudioSingle.pitch = pitch;
		levelAudioSingle.PlayOneShot (music, volume*(volumeGeneral/10));//tweak
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

	private bool switchMusicFromTo(AudioSource from, AudioSource to) {
		float volume = Mathf.Min (1.0f, (Time.time - timer) / dureeTransition)*(volumeGeneral/10);//tweak
		from.volume = 1.0f*(volumeGeneral/10) - volume;//tweak le 1f
		to.volume = volume;

		return volume == 1.0f*(volumeGeneral/10);//tweak le nax
	}

	public void stopSon(){
		Debug.Log ("STOP");
		levelAudioSingle.Stop ();
	}

	public void notifVolumeChange(){
		onVolumeChange();
		float newVol = volumeGeneral;
		newVol/=10;
		Debug.Log(newVol);
		levelAudioLoop2.volume = newVol;
		levelAudioLoop1.volume = newVol;
		levelAudioSingle.volume= newVol;
	}
}
