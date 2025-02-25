﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CinematiqueItem {

	public AudioClip son;

	public string texte;

	public int dureeArret = 0;
	public int dureeAcces = 0;

	public Vector3 pos;
	public Vector3 rot;
	private Vector3 velocity;


	private Vector3 posInit;
	private Vector3 rotInit;
	private float distanceInitiale;
	private Vector3 forwardInitial;

	private Coroutine actualDeplacement;

	public bool isShaking;


	public bool isInDeplacement;

	public void start(){
	//	Debug.Log ("DebutCinosh");
		velocity = Vector3.zero;
		if (dureeAcces == 0) {
	//		Debug.Log ("instant");
			Camera.main.transform.position = pos;
			Camera.main.transform.LookAt (pos + rot);
			if(son!=null){
				GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().playOneShot (son);
			}
		} else {
	//		Debug.Log ("traveling");
			isInDeplacement = true;
			this.distanceInitiale = (pos - Camera.main.transform.position).magnitude;
			this.forwardInitial = Camera.main.transform.forward;
			if(son!=null){
				GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().playOneShot (son);
			}
			if(texte!=""){
				GameObject.FindGameObjectWithTag ("HUDAffichageCinematique").GetComponent<AffichageCinematique> ().activeText (texte);
			}
			actualDeplacement = GameControl.control.StartCoroutine (deplacement());
		}
//		Debug.Log ("FinCinosh");
	}


	IEnumerator deplacement(){


	//	Debug.Log ("Debut corotine");
		while((((pos - Camera.main.transform.position).magnitude)>=0.5f) || (rot - Camera.main.transform.forward).magnitude>=0.01f){
	//		Debug.Log ("deplacement");
			Camera.main.transform.position = Vector3.SmoothDamp (Camera.main.transform.position, pos, ref velocity, 0.01f,dureeAcces);
			float ratio = (pos - Camera.main.transform.position).magnitude / this.distanceInitiale;
			Camera.main.transform.forward = this.forwardInitial * ratio + rot * (1.0f - ratio);
			//Debug.Log (Camera.main.transform.position +"/"+ pos +":"+((pos - Camera.main.transform.position).magnitude));
			yield return new WaitForFixedUpdate ();
		}
	//	Debug.Log ("Fin corotine");
		isInDeplacement = false;

	}

	public void stop(){
		if(actualDeplacement!=null){
			GameControl.control.StopCoroutine (actualDeplacement);
		}

	}
		


}
