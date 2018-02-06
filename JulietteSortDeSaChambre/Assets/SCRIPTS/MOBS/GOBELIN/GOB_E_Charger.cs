using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_Charger : IA_Etat {

	public int degats;
	public float impulsionVerticale;
	public float impulsionHorizontale;
	public float forceRecule;
	public AudioClip sonCharge;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;
	private Vector3 direction;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		colliderArme = GetComponent<IA_TriggerArme> ();

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		agent.getSoundEntity().playOneShot(sonCharge, 1.0f);
		degatsAttaqueEffectues = false;
		this.direction = this.transform.forward;
		rb.AddForce (this.transform.up * impulsionVerticale + this.direction * impulsionHorizontale);
		setAnimation (GOB_Animations.CHARGER);
	}

	public override void faireEtat()
	{
		this.transform.forward = this.direction;
		
		if (!agent.isActualAnimation(GOB_Animations.COMBATTRE)) { // l'attaque puissante est toujours en cours
			
			if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

				princesseVie.blesser (degats, this.gameObject, forceRecule);
				degatsAttaqueEffectues = true;
			}

		} else {
			changerEtat(this.GetComponent<GOB_E_Combattre>());
		}
	}

	public override void sortirEtat()
	{
		
	}
}
