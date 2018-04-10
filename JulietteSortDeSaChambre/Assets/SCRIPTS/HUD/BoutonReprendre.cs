using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoutonReprendre : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClick(){
		GetComponentInParent<AffichagePause>().finPause();
	}

	public void onClickQuitter(){
		Debug.Log("quitter");
	}
}
