using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase2_DebutCharge : IA_Etat {
	public float duree;
	public AudioClip sonAttaque;
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
		// setAnimation (GOB_Animations.ATTAQUER_HORIZONTALEMENT);
		timer = Time.time + this.duree;
	}

	public override void faireEtat()
	{

		if (Time.time > timer) {
			changerEtat(this.GetComponent<TROGAL_E_Phase2_Charge>());
		}
	}

	public override void sortirEtat()
	{
		
	}
}
