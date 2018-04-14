using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_CasseMur : IA_Etat {
	public bool enAttente;
	public IA_PointInteret apparition;

	// Bindez cet event dans le script qui le trigger (pas ici donc)
	public FaireCasserMurTrollEvenement ev;

	// Use this for initialization
	void Start() {
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
	}

	public override void entrerEtat() {
		
	}

	public override void faireEtat(){
		//Déclencheur de l'évènement (dans le script qui déclenche l'event, pas celui là normalement)
		if (!enAttente) {
			transform.position = new Vector3 (apparition.transform.position.x, transform.position.y, apparition.transform.position.z);
			changerEtat (GetComponent<TRO_E_Patrouiller> ());
		}
	}

	public override void sortirEtat() {
		enAttente = true;
	}
}
