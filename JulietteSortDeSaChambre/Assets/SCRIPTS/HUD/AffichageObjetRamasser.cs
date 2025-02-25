﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageObjetRamasser : MonoBehaviour {

	private bool time;
	private Transform[] listImagePaused;
	private Dictionary<EnumObjetProgression,GameObject> dicoObjet;
	private Dictionary<EnumArmes,GameObject> dicoArme;
	private SoundManager soundManager;
	private ObjetInteractifs objetActuel;

	// Use this for initialization
	void Start () {
		time = true;
		listImagePaused = gameObject.GetComponentsInChildren<Transform>(true);
		dicoObjet = new Dictionary<EnumObjetProgression, GameObject> ();
		foreach(EnumObjetProgressionIcone enu in GetComponentsInChildren<EnumObjetProgressionIcone>(true)){
			dicoObjet.Add (enu.typeObjetProgression, enu.gameObject);
		}
		dicoArme = new Dictionary<EnumArmes, GameObject> ();
		foreach(EnumArmeIcone enu in GetComponentsInChildren<EnumArmeIcone>(true)){
			dicoArme.Add (enu.typeArme, enu.gameObject);
		}

		soundManager = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
	}


	// Update is called once per frame
	void Update () {
		if (!time) {
			if (Time.timeScale != 0) {
				Time.timeScale = 0;
				soundManager.pauseAllSoundExceptMusic();
			} else {
				if (Input.GetButtonDown("Interagir")) {
					time = true;
					desaffiche ();
					Time.timeScale = 1;
					soundManager.resumeAllSound();
					if(objetActuel.declencheEvenement){
						objetActuel.evenementStart ();
					}
				}
			}
		} 
	}


	private void affichageObjet(Arme arme){
		if(dicoArme.ContainsKey(arme.typeArme)){
			dicoArme [arme.typeArme].SetActive (true);
		}
		objetActuel = arme;


	}
	private void affichageObjet(ObjetProgression objetProgression){
		if(dicoObjet.ContainsKey(objetProgression.objetProgression)){
			dicoObjet [objetProgression.objetProgression].SetActive (true);
		}
		objetActuel = objetProgression;
	}

	public void activeObjet(ObjetInteractifs objet){
		time = false;

		Arme arme = objet as Arme;
		ObjetProgression objetProgression = objet as ObjetProgression;

		if (arme != null) {
			affichageObjet (arme);
		} else if (objetProgression != null) {
			affichageObjet (objetProgression);
		}


		for (int i = 1; i < listImagePaused.Length; i++) {
			switch (listImagePaused [i].name) {
			case "Fond":
				listImagePaused [i].gameObject.SetActive(true);
				break;
			case "Nom":
				listImagePaused [i].gameObject.SetActive (true);
				listImagePaused [i].GetComponent<UnityEngine.UI.Text> ().text = objet.nomObjet;
				break;
			case "Description":
				listImagePaused [i].gameObject.SetActive (true);
				listImagePaused [i].GetComponent<UnityEngine.UI.Text> ().text = objet.descriptionObjet;
				break;
			}
		}

	}

	public void desaffiche(){
		for (int i = 1; i < listImagePaused.Length; i++) {
			listImagePaused [i].gameObject.SetActive (false);
		}
	}


}
