using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematiqueManager : MonoBehaviour {

	public Cinematique[] listeCinematique;

	// private bool cinematiqueEnCours;
	private Cinematique actualCinematique;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if(InputManager.GetKeyDown(KeyCode.C)){

			if(this.isCinematiqueEnCours()){
				this.skipCinematique();
			} else {
				this.lancerCinematique(0);
			}
		}
	}

	public void lancerCinematique(int numCinematique){
		this.actualCinematique = this.listeCinematique[numCinematique];
		this.actualCinematique.lancer();
	}

	public void skipCinematique(){
		this.actualCinematique.skipper();
	}

	public void notifierFinCinematique(){
		this.actualCinematique = null;
	}

	public bool isCinematiqueEnCours(){
		return this.actualCinematique != null;
	}
}
