using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase2_Combattre : IA_Etat {

	public float distanceSortieCombat;
	public float delaisMaxAvantAttaque;
	public float delaisMinAvantAttaque;
	
	private float timerAttaque;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		// setAnimation (GOB_Animations.COMBATTRE);
		timerAttaque = Time.time + delaisMinAvantAttaque + (delaisMaxAvantAttaque - delaisMinAvantAttaque) * Random.value;
	}

	public override void faireEtat(){
		
		agent.seTournerVersPosition (princesse.transform.position);

		if (agent.distanceToPrincesse() >= distanceSortieCombat) {
			changerEtat (GetComponent<TROGAL_E_Phase2_Courir> ());
		}
		else if (Time.time >= timerAttaque) {
			// changerEtat (GetComponent<TROGAL_E_Phase1_AttaqueChocSismique> ());
		}
	}

	public override void sortirEtat() {
		
	}
}
