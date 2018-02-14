using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_AttaquerEmpaler : IA_Etat {

	public int degatsParAttaque;
	public float forceRecule;
	public float distanceParcourue;
	public float vitesse;
	public AudioClip sonAttaque;
	public float dureeChargement;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;
	private int numAttaque;
	private Vector3 direction;
	private float timerChargement;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		colliderArme = GetComponent<IA_TriggerArme> ();

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		agent.getSoundEntity().playOneShot(sonAttaque, 1.0f);
		degatsAttaqueEffectues = true;
		setAnimation (GOB_Animations.ATTAQUER_EMPALER);
		numAttaque = 0;
		timerChargement = Time.time + this.dureeChargement;
	}

	public override void faireEtat()
	{
		switch (numAttaque) {

		case 0:
			if (Time.time < timerChargement) { // le chargement est toujours en cours
				agent.seTournerVersPosition(princesse.transform.position);
				this.direction = this.transform.forward;
			}
			else {
				setAnimation (GOB_Animations.COMBATTRE);
				this.transform.forward = this.direction;
				numAttaque++;
				degatsAttaqueEffectues = false;
				nav.enabled = true;
				nav.speed = vitesse;
				agent.definirDestination (this.transform.position + this.direction * distanceParcourue * 0.33f);
			}
			break;

		case 1:
			if (!agent.isActualAnimation (GOB_Animations.ATTAQUER_EMPALER + "2")) { // l'attaque 1 est toujours en cours
				testerAttaque();
			} else {
				numAttaque++;
				degatsAttaqueEffectues = false;
				agent.definirDestination (this.transform.position + this.direction * distanceParcourue * 0.33f);
			}
			break;

		case 2:
			if (!agent.isActualAnimation (GOB_Animations.ATTAQUER_EMPALER + "3")) { // l'attaque 1 est toujours en cours
				testerAttaque();
			} else {
				numAttaque++;
				degatsAttaqueEffectues = false;
				agent.definirDestination (this.transform.position + this.direction * distanceParcourue * 0.33f);
			}
			break;

		case 3:
			if (!agent.isActualAnimation (GOB_Animations.COMBATTRE)) { // l'attaque 1 est toujours en cours
				testerAttaque();
			} else {
				changerEtat(this.GetComponent<GOB_E_Combattre>());
			}
			break;
		}
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}

	private void testerAttaque(){
		if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

			princesseVie.blesser (degatsParAttaque, this.gameObject, forceRecule);
			degatsAttaqueEffectues = true;
		}
	}
}
