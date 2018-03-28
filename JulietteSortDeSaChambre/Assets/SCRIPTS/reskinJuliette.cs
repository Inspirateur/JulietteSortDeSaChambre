using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reskinJuliette : MonoBehaviour {
	private GameObject bodyJuliette;
	public Material[] bras;
	public Material[] epaules;
	public Material[] robe;
	public Material[] buste;
	public int skinIndex;

	AudioSource audiosource;
	public AudioClip clothSound;




	// Use this for initialization
	void Start () {
		skinIndex = 1;

		audiosource = GetComponent<AudioSource> ();


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
		
			if (skinIndex >= bras.Length) {
				skinIndex = 0;
			}
			Material[] mats =GetComponent<Renderer>().materials;	
			mats[3] = buste[skinIndex];
			mats[4] = robe[skinIndex];		
			mats[5] = epaules[skinIndex];
			mats[7] = bras[skinIndex];
			gameObject.GetComponent<Renderer>().materials =  mats;
		audiosource.PlayOneShot (clothSound, 0.5f);
			skinIndex++;
		}

}
