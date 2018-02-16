using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoeurItem : Item {

	PrincesseVie juliette ;
	public int valeurSoin;

	// Use this for initialization
	void Start () {
		juliette = GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseVie>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	override
	public void Activation()
	{
		juliette.soigner (valeurSoin);
		supprimerObjet ();

	}
}
