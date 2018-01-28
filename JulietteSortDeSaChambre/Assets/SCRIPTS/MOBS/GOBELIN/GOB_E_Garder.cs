using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_Garder : IA_Etat {

	public IA_PointInteret emplacementAGarder;
	public float vitesse;
	public AudioClip sonPoursuite;

	private bool enDeplacement;
	private bool enRotation;
	private bool enGarde;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation(GOB_Animations.COURIR);
		nav.speed = vitesse;
		nav.enabled = true;
		agent.definirDestination(emplacementAGarder);
		enDeplacement = true;
		enRotation = false;
		enGarde = false;
	}

	public override void faireEtat()
	{
		if (perception.aReperer(princesse, 1.0f)) {
			changerEtat (this.GetComponent<GOB_E_Poursuivre> ());

		} else if (!enDeplacement && perception.aReperer(princesse, 1.5f)) {
			changerEtat (this.GetComponent<GOB_E_Poursuivre> ());

		} else if (enDeplacement) {
			if (agent.destinationCouranteAtteinte ()) {
				nav.enabled = false;
				enDeplacement = false;
				enRotation = true;
			}
		} else if (enRotation) {

			enRotation = agent.seTournerDansOrientationDe (emplacementAGarder.gameObject);

		} else if (!enGarde && !enRotation) {
			enGarde = true;
			setAnimation (GOB_Animations.GARDER);
		}
	}

	public override void sortirEtat()
	{
		agent.getSoundEntity ().playOneShot (sonPoursuite);
	}
}
