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
	private Vector3 positionGarde = Vector3.zero;
	private Vector3 forwardPositionGarde;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		if(emplacementAGarder == null && positionGarde.Equals(Vector3.zero)){
			
			this.positionGarde = this.transform.position;
			this.forwardPositionGarde = this.transform.forward;
			enDeplacement = false;
			enRotation = false;
			enGarde = false;
			nav.enabled = false;
		}
		else {

			// if(positionGarde.Equals(Vector3.zero)) {

				this.positionGarde = this.emplacementAGarder.transform.position;
				this.forwardPositionGarde = this.emplacementAGarder.transform.forward;
			// }
		
			setAnimation(GOB_Animations.COURIR);
			nav.speed = vitesse;
			nav.enabled = true;
			agent.definirDestination(this.positionGarde);
			enDeplacement = true;
			enRotation = false;
			enGarde = false;
		}
	}

	public override void faireEtat()
	{
		if (perception.aRepere(princesse, 1.0f)) {
			agent.getSoundEntity ().playOneShot (sonPoursuite);
			changerEtat (this.GetComponent<GOB_E_Poursuivre> ());

		} else if (!enDeplacement && perception.aRepere(princesse, 1.5f)) {
			agent.getSoundEntity ().playOneShot (sonPoursuite);
			changerEtat (this.GetComponent<GOB_E_Poursuivre> ());

		} else if (enDeplacement) {
			if (agent.destinationCouranteAtteinte ()) {
				nav.enabled = false;
				enDeplacement = false;
				enRotation = true;
			}
		} else if (enRotation) {

			enRotation = agent.seTournerEnDirectionDe(this.forwardPositionGarde);

		} else if (!enGarde && !enRotation) {
			enGarde = true;
			setAnimation (GOB_Animations.GARDER);
		}
	}

	public override void sortirEtat()
	{
		
	}
}
