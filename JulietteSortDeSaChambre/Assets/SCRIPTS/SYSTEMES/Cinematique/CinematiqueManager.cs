using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematiqueManager : MonoBehaviour {

    public List<CinematiqueItemList> cinematique;
	public bool isInCinematique;

	private Vector3 posInit;
	private Vector3 forwardInit;

	// Use this for initialization
	void Start () {
		//cinematique = new List<CinematiqueItemList> ();
		isInCinematique = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.J)) {
			ActiveCinematique (true);
			cinematique[0].item = 0;
			cinematique[0].lancer ();
		}
	}

	public void ActiveCinematique(bool active){
		if(active){
			posInit = Camera.main.transform.position;
			forwardInit = Camera.main.transform.forward;
			isInCinematique = true;
		}else{
			Camera.main.transform.position = posInit;
			Camera.main.transform.forward = forwardInit;
			isInCinematique = false;
		}

	}
}
