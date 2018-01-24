using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BruiteurPas : MonoBehaviour {

	private SoundEntity se;
	private float timerStep;
	
	public float minPitch;
	public float maxPitch;
	public float minVolume;
	public float maxVolume;


	void Awake () {
		se = GetComponent<SoundEntity> ();
	}

	public void pas () {
		int indice = Random.Range (0, se.nbClips());
		float volume = Random.Range (minVolume, maxVolume);
		float pitch = Random.Range (minPitch, maxPitch);
		se.playOneShot(indice, volume, pitch);
	}
}
