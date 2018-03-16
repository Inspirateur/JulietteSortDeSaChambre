using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematiqueManager : Evenement {

    public List<CinematiqueItemList> cinematique;
	public bool isInCinematique;

	private Vector3 posInit;
	private Vector3 forwardInit;

	private AffichageCinematique hudCinematique;
	private GameObject hud;

	// Use this for initialization
	void Start () {
		//cinematique = new List<CinematiqueItemList> ();
		isInCinematique = false;
		hudCinematique = GameObject.FindGameObjectWithTag ("HUDAffichageCinematique").GetComponent<AffichageCinematique>();
		hud = GameObject.FindGameObjectWithTag ("HUD");
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ActiveCinematique(bool active){
		if(active){
			hudCinematique.setActiveBandeNoir (true);
			hud.SetActive (false);
			posInit = Camera.main.transform.position;
			forwardInit = Camera.main.transform.forward;
			isInCinematique = true;
		}else{
			hudCinematique.setActiveBandeNoir (false);
			hud.SetActive (true);
			Camera.main.transform.position = posInit;
			Camera.main.transform.forward = forwardInit;
			isInCinematique = false;
		}

	}

	public void lanceCinématique(int indice){
		if (indice >= 0 && indice < cinematique.Count) {
			ActiveCinematique (true);
			cinematique[indice].item = 0;
			cinematique[indice].lancer ();
		}

	}

}
