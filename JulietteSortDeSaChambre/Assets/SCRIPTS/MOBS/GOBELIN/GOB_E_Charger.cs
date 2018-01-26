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
		this.transform.forward = (princesse.transform.position - this.transform.position).normalized;
		degatsAttaqueEffectues = false;
		rb.AddForce (this.transform.up * impulsionVerticale + this.transform.forward * impulsionHorizontale);
//		setAnimation ("attackPuissante");
	}

	public override void faireEtat()
	{
//		if (!agent.isActualAnimation("idleCombat")) { // l'attaque puissante est toujours en cours
			
			if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

//				princesseVie.blesser (degatsAttaquePuissante, this.gameObject, forceReculeAttaquePuissante);
				degatsAttaqueEffectues = true;
				Debug.Log ("princesse touchée par charge");
			}

//		} else {
//			changerEtat(this.GetComponent<gob_E_combat>());
//		}
	}

	public override void sortirEtat()
	{

	}

	public override void subirDegats(int valeurDegats, Vector3 hitPoint)
	{

	}
}
