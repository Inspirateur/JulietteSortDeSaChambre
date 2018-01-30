using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_AttaquerHorizontalement : IA_Etat {

	public int degats;
	public float forceRecule;
	public AudioClip sonAttaque;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;

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
		setAnimation (GOB_Animations.ATTAQUER_HORIZONTALEMENT);
	}

	public override void faireEtat()
	{
		if (!agent.isActualAnimation (GOB_Animations.COMBATTRE)) { // l'attaque est toujours en cours
			if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

	//			princesseVie.blesser (degatsAttaquePuissante, this.gameObject, forceReculeAttaquePuissante);
				degatsAttaqueEffectues = true;
				Debug.Log ("horizontale touché");
			}
		} else {
			changerEtat(this.GetComponent<GOB_E_Combattre>());
		}
	}

	public override void sortirEtat()
	{

	}
}
