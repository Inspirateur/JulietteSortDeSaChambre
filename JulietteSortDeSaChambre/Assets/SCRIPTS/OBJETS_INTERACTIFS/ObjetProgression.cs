using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetProgression : ObjetInteractifs {

	private princesseObjetProgression juliette;
	public EnumObjetProgression objetProgression;

	//private affichage_ObjetRamasser affichageObjetRamasser;
	//private affichage_objetActuel affichageobjetActuel;

	// Use this for initialization
	void Start () {
		juliette= GameObject.FindGameObjectWithTag("Player").GetComponent<princesseObjetProgression>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation(){
		Debug.Log ("Activation ObjetProgression");
	}
}
