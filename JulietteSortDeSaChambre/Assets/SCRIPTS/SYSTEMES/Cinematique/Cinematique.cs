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
	// protected camera cam;
	private CinematiqueManager cinematiqueManager;
    protected Vector3 initialPositionCam;
    protected Vector3 initialForwardCam;

	// Use this for initialization
	void Awake () {
		// this.cam = Camera.main.GetComponent<camera>();

		this.cinematiqueManager = GameObject.FindGameObjectWithTag("CinematiqueManager").GetComponent<CinematiqueManager>();
	}

	public void Update(){
		if(this.active){
			this.mettreAJour();
		}
	}

	public void lancer(){
		this.active = true;
		this.numPOVActuel = 0;
        this.initialPositionCam = Camera.main.transform.position;
        this.initialForwardCam = Camera.main.transform.forward;
		this.entrer();
	}

	public void terminer(){
		this.active = false;
		this.sortir();
		this.cinematiqueManager.notifierFinCinematique();
	}

	public void skipper(){

		if(this.skippable){

			this.terminer();
		}
	}

	public abstract void entrer();

	public abstract void mettreAJour();

	public abstract void sortir();

	public bool estEnCours(){
		return this.active;
	}

    protected void placer(CinematiquePointOfView pov){
        Camera.main.transform.position = pov.transform.position;
        Camera.main.transform.LookAt(pov.transform.position + pov.transform.forward);
    }

	protected void replacerPositionAvantLancementCinematique(){
		Camera.main.transform.position = this.initialPositionCam;
        Camera.main.transform.forward = this.initialForwardCam;
	}
}
