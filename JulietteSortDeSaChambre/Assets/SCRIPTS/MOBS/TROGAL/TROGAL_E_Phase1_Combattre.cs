using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase1_Combattre : IA_Etat {

	public float distanceSortieCombat;
	public float delaisMaxAvantAttaque;
	public float delaisMinAvantAttaque;
	public float pourcentageAttaqueTourbillon;

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
			changerEtat (GetComponent<TROGAL_E_Phase1_Marcher> ());
		}

		else if (Time.time >= timerAttaque) {
			float choixAttaque = Random.value;

			if (choixAttaque <= pourcentageAttaqueTourbillon) {
				changerEtat (GetComponent<TROGAL_E_Phase1_AttaqueTourbillon> ());
			} else {
				changerEtat (GetComponent<TROGAL_E_Phase1_AttaqueTourbillon> ());
			}
		}
	}

	public override void sortirEtat() {
		
	}
}
