using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Combattre : IA_Etat {

	public float distanceSortieCombat;
	public float delaisMaxAvantAttaque;
	public float delaisMinAvantAttaque;

	private float timerAttaque;
	private bool alterneAtk = false;

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
		if(!princesseVie.enVie()){
			
		}
		else if (agent.distanceToPrincesse () >= distanceSortieCombat) {
			changerEtat (GetComponent<TRO_E_Poursuivre> ());
		} else if (gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName ("combattre") && Time.time >= timerAttaque) {
			if (!alterneAtk && agent.distanceToPrincesse () < 1.6f) {
				alterneAtk = true;
				Debug.Log (">>> Troll lance atk VERTICALE <<<");
				changerEtat (GetComponent<TRO_E_AttaquerVerticalement> ());
			} else {
				alterneAtk = false;
				Debug.Log (">>> Troll lance atk HORIZONTALE <<<");
				changerEtat (GetComponent<TRO_E_AttaquerHorizontalement> ());
			}
		}
	}

	public override void sortirEtat() {

	}
}
