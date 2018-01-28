using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_AttaquerEmpaler : IA_Etat {

	public int degatsParAttaque;
	public float forceRecule;
	public float distanceParcourue;
	public float vitesse;
	public AudioClip sonAttaque;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;
	private int numAttaque;

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
		degatsAttaqueEffectues = false;
		setAnimation (GOB_Animations.ATTAQUER_EMPALER);
		nav.enabled = true;
		nav.speed = vitesse;
		agent.definirDestination (this.transform.position + this.transform.forward * distanceParcourue * 0.33f);
		numAttaque = 1;
	}

	public override void faireEtat()
	{
		
		switch (numAttaque) {

		case 1:
			if (!agent.isActualAnimation (GOB_Animations.ATTAQUER_EMPALER + "2")) { // l'attaque 1 est toujours en cours
				testerAttaque();
			} else {
				numAttaque++;
				degatsAttaqueEffectues = false;
				agent.definirDestination (this.transform.position + this.transform.forward * distanceParcourue * 0.33f);
				setAnimation (GOB_Animations.COMBATTRE);
			}
			break;

		case 2:
			if (!agent.isActualAnimation (GOB_Animations.ATTAQUER_EMPALER + "3")) { // l'attaque 1 est toujours en cours
				testerAttaque();
			} else {
				numAttaque++;
				degatsAttaqueEffectues = false;
				agent.definirDestination (this.transform.position + this.transform.forward * distanceParcourue * 0.33f);
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


		/*

		if (numAttaque == 1){

			if (!agent.isActualAnimation (GOB_Animations.ATTAQUER_EMPALER + "2")) { // l'attaque 1 est toujours en cours


			}
		} else {
			changerEtat(this.GetComponent<GOB_E_Combattre>());
		}*/
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}

	private void testerAttaque(){
		if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

//			princesseVie.blesser (degatsAttaquePuissante, this.gameObject, forceReculeAttaquePuissante);
			degatsAttaqueEffectues = true;
			Debug.Log ("touché attaque " + numAttaque);
		}
	}
}
