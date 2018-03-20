using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase1_AttaqueTourbillon : IA_Etat {

	public int degats;
	public float forceRecule;
	public float dureeAttaque;
	public float vitesseRotation;
	public float vitesseDeplacement;
	public AudioClip sonAttaque;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;
	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		// colliderArme = GetComponent<IA_TriggerArme> ();

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		// agent.getSoundEntity().playOneShot(sonAttaque, 1.0f);
		degatsAttaqueEffectues = false;
		// setAnimation (GOB_Animations.ATTAQUER_HORIZONTALEMENT);
		timer = Time.time + this.dureeAttaque;
	}

	public override void faireEtat()
	{

		this.tourner();

		if (timer > Time.time) { // l'attaque est toujours en cours
			// if (!degatsAttaqueEffectues && colliderArme.IsPrincesseTouchee ()) {

			// 	princesseVie.blesser (degats, this.gameObject, forceRecule);
			// 	degatsAttaqueEffectues = true;
			// }
		} else {
			changerEtat(this.GetComponent<TROGAL_E_Phase1_Desoriente>());
		}
	}

	public override void sortirEtat()
	{
		
	}

	private void tourner(){

		Vector3 dir = princesse.transform.position - this.transform.position;
		dir.y = 0.0f;
		dir.Normalize();
		dir = dir * (vitesseDeplacement * Time.deltaTime);

		this.transform.Translate(dir, Space.World);

		this.transform.Rotate(0.0f, vitesseRotation * Time.deltaTime, 0.0f);
	}
}
