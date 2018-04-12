using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reskinJuliette : MonoBehaviour {
	private GameObject bodyJuliette;
	public Material[] bras;
	public Material[] epaules;
	public Material[] robe;
	public Material[] buste;
	// public int skinIndex;

	AudioSource audiosource;
	public AudioClip clothSound;




	// Use this for initialization
	void Start () {
		// skinIndex = 1;

		audiosource = GetComponent<AudioSource> ();

		setSkin();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (Input.GetKeyDown (KeyCode.I)) {
			Reskin ();
		}
	*/

	}
	public void Reskin(){
		
		GameControl.control.skinIndex++;
		if (GameControl.control.skinIndex >= bras.Length) {
			GameControl.control.skinIndex = 0;
		}
		setSkin();
		audiosource.PlayOneShot (clothSound, 0.5f);
	}

	private void setSkin(){
		Material[] mats =GetComponent<Renderer>().materials;	
		mats[3] = buste[GameControl.control.skinIndex];
		mats[4] = robe[GameControl.control.skinIndex];		
		mats[5] = epaules[GameControl.control.skinIndex];
		mats[7] = bras[GameControl.control.skinIndex];
		gameObject.GetComponent<Renderer>().materials =  mats;
	}
}
