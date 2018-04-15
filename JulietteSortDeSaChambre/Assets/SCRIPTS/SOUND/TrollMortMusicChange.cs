using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrollMortMusicChange : MonoBehaviour {

	private ChangeMusicBackground musicChange;
	private IA_MobVie vie;

	private bool uneFois;

	// Use this for initialization
	void Start () {
		vie = GetComponent<IA_MobVie>();
		uneFois = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!vie.estEnVie() && !uneFois) {
			musicChange.ChangeSong(0);
		}
	}
}
