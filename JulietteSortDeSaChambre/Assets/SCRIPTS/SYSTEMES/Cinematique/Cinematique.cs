using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cinematique : MonoBehaviour {

	[Header("Paramètre généraux")]
	
	[Tooltip("Liste des différents point par lequelles la caméra va passer")]
	public CinematiquePointOfView[] listeCinematiquePointOfView;

	[Tooltip("Vrai si on peut passer la cinématique")]
	public bool skippable;

	protected bool active;
	protected int numPOVActuel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.miseAJourCamera();
	}

	public void lancer(){
		this.active = true;
		this.numPOVActuel = 0;
	}

	public void skipper(){

	}

	public abstract void miseAJourCamera();

	public bool estEnCours(){
		return this.active;
	}
}
