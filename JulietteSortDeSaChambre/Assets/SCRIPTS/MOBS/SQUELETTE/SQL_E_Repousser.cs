using System;
using UnityEngine;

public class SQL_E_Repousser : IA_Etat {
	public int degats;
	public float forceRecul;
	public AudioClip sonAttaque;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;
	private float timerFinAttaque;
	private float timerChargement;

	void Start() {
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		colliderArme = GetComponent<IA_TriggerArme> ();

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat() {
		degatsAttaqueEffectues = false;
		setAnimation (SQL_Animations.REPOUSSER);
		timerChargement = Time.time + 1.5f;
		timerFinAttaque = timerChargement + 1f;
	}

	public override void faireEtat() {
		agent.seTournerVersPosition(agent.getPrincesse().transform.position);
		if (Time.time < timerFinAttaque) { // l'attaque est toujours en cours
			Debug.Log("en attaque");
			if(Time.time >= timerChargement){
				Debug.Log("fini chargement");
				if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {
					princesseVie.blesser (degats, this.gameObject, forceRecul);
					degatsAttaqueEffectues = true;
				}
			}
		} else {
			changerEtat(this.GetComponent<SQL_E_Combattre>());
		}
	}

	public override void sortirEtat(){
	}
}
