using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteChargeMouvante : Evenement
{

	private AudioSource audioSource;
	[HideInInspector]
	public bool actif;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
		actif = false;
	}

	override
	public void activation() {
		GetComponent<MovingObject>().StartPlateformeBeggening();
		audioSource.Play();
		actif = true;
	}

	public void stopSound(){
		audioSource.Stop();
	}

	public void test() {

	}

	public void test2(int t) {

	}

	public int test3() {
		return 1;
	}
}
