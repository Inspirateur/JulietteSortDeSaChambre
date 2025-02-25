﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrogalEvenement : Evenement {

	private bool deplacement;
	private Vector3 posInit;
	private Vector3 posFinal;
	private Vector3 velocity;
	private bool encours;
	public AudioClip sonBoss;

	void Awake(){
		encours = true;
		velocity = Vector3.zero;
		deplacement = false;
		posInit = GetComponent<Transform> ().position;
	}

	void Start(){
		CheckPointManager.getInstance().onRestart += OnRestart;
	}

	void Update(){
		if(deplacement){
			if(GetComponent<Transform> ().localPosition.x>70.0f){
				deplacement = false;
				encours = false;
				gameObject.SetActive (false);
			}
			Vector3 x = GetComponent<Transform> ().position;
			x.x += 0.31f;
			GetComponent<Transform> ().position = x;
		}

	}

	public void pousserBoss(){
		GetComponent<Animator>().SetTrigger("envoieboss");
		//GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager> ().playOneShot (sonBoss);
		deplacement = true;
	}

	public void OnRestart(){
		GetComponent<Transform> ().position = posInit;
		// encours = true;
		// velocity = Vector3.zero;
		// deplacement = false;
		gameObject.SetActive (true);
	}

	public override bool evenementIsEnCours ()
	{
		return encours;
	}

}
