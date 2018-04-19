using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollMortMusicChange : MonoBehaviour {

	private ChangeMusicBackground musicChange;
	private IA_MobVie vie;

	private bool uneFois;
	private SoundManager sm;

	// Use this for initialization
	void Start () {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		musicChange = GetComponent<ChangeMusicBackground>();
		vie = GetComponent<IA_MobVie>();
		uneFois = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!vie.estEnVie() && !uneFois) {
			sm.setBackgroundMusic(sm.listeClips[0]);
			uneFois = true;
		}
	}
}
