using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase2_AttaquePoingDroit : IA_Etat {

	public int degats;
	public float forceRecule;
	public AudioClip sonAttaque;
	public TROGAL_Collider colliderPoing;
	public int nombreMoyenDeCoupDePoing;
	public float vitesseDeplacement;

	private bool degatsAttaqueEffectues;
	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		// agent.getSoundEntity().playOneShot(sonAttaque, 1.0f);
		degatsAttaqueEffectues = false;
		// setAnimation (GOB_Animations.ATTAQUER_HORIZONTALEMENT);
		timer = Time.time + 0.7f;

		agent.seTournerVersPosition (princesse.transform.position);
	}

	public override void faireEtat()
	{
		this.avancer();

		if (timer > Time.time) { // l'attaque est toujours en cours
			if (!degatsAttaqueEffectues && colliderPoing.IsPrincesseTouchee ()) {

				princesseVie.blesser (degats, this.gameObject, forceRecule);
				degatsAttaqueEffectues = true;
			}
		} else {
			float choix = Random.value;
			if(choix <= 1.0f / (float)nombreMoyenDeCoupDePoing){
				changerEtat(this.GetComponent<TROGAL_E_Phase2_DebutCharge>());
			} else {
				changerEtat(this.GetComponent<TROGAL_E_Phase2_AttaquePoingGauche>());
			}
		}
	}

	public override void sortirEtat()
	{
		
	}
	
	private void avancer(){

		Vector3 dir = this.transform.forward;
		dir.y = 0.0f;
		dir.Normalize();
		dir = dir * (vitesseDeplacement * Time.deltaTime);

		this.transform.Translate(dir, Space.World);
	}
}
