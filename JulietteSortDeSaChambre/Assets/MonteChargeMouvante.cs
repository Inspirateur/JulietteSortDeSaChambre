using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonteChargeMouvante : Evenement
{

	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource>();
	}

	override
	public void activation() {
		GetComponent<MovingObject>().StartPlateformeBeggening();
		audioSource.Play();
	}

	public void test() {

	}

	public void test2(int t) {

	}

	public int test3() {
		return 1;
	}
}
