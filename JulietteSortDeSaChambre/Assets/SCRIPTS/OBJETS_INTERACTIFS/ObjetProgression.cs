using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetProgression : ObjetInteractifs {

	private PrincesseObjetProgression juliette;
	public EnumObjetProgression objetProgression;
	private AffichageObjetRamasser affichageObjetRamasser;
	//private affichage_objetActuel affichageobjetActuel;

	// Use this for initialization
	void Start () {
		juliette= GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseObjetProgression>();
		affichageObjetRamasser = GameObject.FindGameObjectWithTag ("HUDAffichageObjetRamasser").GetComponent<AffichageObjetRamasser> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation(){
		affichageObjetRamasser.activeObjet (this);
		juliette.addItem (this.objetProgression);
		supprimerObjet ();
	}
}
