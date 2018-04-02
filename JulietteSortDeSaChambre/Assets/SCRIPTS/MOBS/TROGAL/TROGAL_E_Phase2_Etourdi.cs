using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase2_Etourdi : IA_Etat {

	public float dureeEtourdissement;

	private float timer;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		// setAnimation (GOB_Animations.COMBATTRE);
		timer = Time.time + dureeEtourdissement;
	}

	public override void faireEtat(){

		if (Time.time >= timer) {
			changerEtat (GetComponent<TROGAL_E_Phase2_Combattre> ());
		}
	}

	public override void sortirEtat() {
		nav.enabled = false;
	}
}
