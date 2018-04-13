using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvenement : Evenement {

	public List<AudioClip> sound;


	public void lancerSon(int i){
		GetComponent<SoundManager> ().playOneShot (sound [i]);
	}

	public void stopSon(){
		GetComponent<SoundManager> ().stopSon ();
	}
}
