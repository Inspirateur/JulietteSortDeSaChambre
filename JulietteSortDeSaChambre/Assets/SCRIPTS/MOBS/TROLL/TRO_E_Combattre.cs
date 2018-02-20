using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Combattre : IA_Etat {

	public float distanceSortieCombat;
	public float delaisMaxAvantAttaque;
	public float delaisMinAvantAttaque;

	private float timerAttaque;

	// Use this for initialization
	void Start() {
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
	}

	public override void entrerEtat() {
		setAnimation (TRO_Animations.COMBATTRE);
		timerAttaque = Time.time + delaisMinAvantAttaque + (delaisMaxAvantAttaque - delaisMinAvantAttaque) * Random.value;
	}

	public override void faireEtat() {
		agent.seTournerVersPosition (princesse.transform.position);

		if (agent.distanceToPrincesse() >= distanceSortieCombat) {
			changerEtat (GetComponent<TRO_E_Poursuivre> ());
		} else if (Time.time >= timerAttaque) {
			changerEtat (GetComponent<TRO_E_AttaquerVerticalement> ());
		}
	}

	public override void sortirEtat() {

	}
}
