using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_AttaquerHorizontalement : IA_Etat {

	public int degats;
	public float forceRecule;
	public float distanceParcourue;
	public AudioClip sonAttaque;
	public float vitesse;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;
	private float timerFinAttaque;
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
		degatsAttaqueEffectues = false;
		setAnimation (GOB_Animations.ATTAQUER_HORIZONTALEMENT);
		nav.enabled = true;
		nav.speed = vitesse;
		agent.definirDestination (this.transform.position + this.transform.forward * this.distanceParcourue);
		timerChargement = Time.time + 0.5f;
		timerFinAttaque = timerChargement + 0.96f;
	}

	public override void faireEtat()
	{
		if (Time.time < timerFinAttaque) { // l'attaque est toujours en cours
			Debug.Log("en attaque");
			if(Time.time >= timerChargement){
				Debug.Log("fini chargement");
				if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

					princesseVie.blesser (degats, this.gameObject, forceRecule);
					degatsAttaqueEffectues = true;
				}
			}
			
		} else {
			changerEtat(this.GetComponent<GOB_E_Combattre>());
		}
	}

	public override void sortirEtat()
	{
		nav.enabled = false;
	}
}
