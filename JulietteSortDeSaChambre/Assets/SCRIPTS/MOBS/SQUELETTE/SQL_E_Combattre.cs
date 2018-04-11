using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQL_E_Combattre : IA_Etat{
	public float distanceRepousse;
	public float delaisMaxAvantAttaque;
	public float delaisMinAvantAttaque;

	private float timerAttaque;

	// Use this for initialization
	void Start() {
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
	}

	public override void entrerEtat() {
		if(agent.seTournerVersPosition (princesse.transform.position)){
			setAnimation (SQL_Animations.COURIR);
		} else {
			setAnimation (SQL_Animations.GARDER);
		}
		
		timerAttaque = Time.time + delaisMinAvantAttaque + (delaisMaxAvantAttaque - delaisMinAvantAttaque) * Random.value;
	}

	public override void faireEtat() {
		if(agent.seTournerVersPosition (princesse.transform.position)){
			setAnimation (SQL_Animations.COURIR);
		} else {
			setAnimation (SQL_Animations.GARDER);
		}
		if (!perception.aRepere(princesse, 2.0f)) {
			Debug.Log ("APPAREMENT LE SQUELETTE NE VOIT PLUS PRINCESSE");
			changerEtat (GetComponent<SQL_E_Garder> ());
		} else if (gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).IsName (SQL_Animations.GARDER) && Time.time >= timerAttaque) {
			if (agent.distanceToPrincesse () < distanceRepousse) {
				changerEtat (GetComponent<SQL_E_Repousser> ());
			} else {
				changerEtat (GetComponent<SQL_E_Attaquer> ());
			}
		} else {
			// Debug.Log ("Anim combat: " + gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).ToString ());
		}
	}

	public override void sortirEtat() {
	}
}


