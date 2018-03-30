using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase2_Courir : IA_Etat {

	public float vitesse;
	public float distanceEntreeCombat;
	public float pourcentageCharge;

	private bool doitCharger;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		// setAnimation(GOB_Animations.COURIR);
		nav.enabled = true;
		doitCharger = Random.value <= pourcentageCharge;
	}

	public override void faireEtat(){
		
		agent.definirDestination (princesse.transform.position);
		nav.speed = vitesse;

		bool orientationOK = !agent.seTournerVersPosition (princesse.transform.position);

		if(orientationOK && doitCharger){
			changerEtat (this.GetComponent<TROGAL_E_Phase2_DebutCharge> ());
		}
		else if (agent.distanceToPrincesse() <= distanceEntreeCombat) {

			changerEtat (this.GetComponent<TROGAL_E_Phase2_Combattre> ());
		}
	}

	public override void sortirEtat() {
		nav.enabled = false;
	}

	public override float getDistanceEntreeCombat(){
		return distanceEntreeCombat;
	}
}
