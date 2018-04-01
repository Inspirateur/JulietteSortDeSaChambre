using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase2_Charge : IA_Etat {

	public int degats;
	public float forceRecule;
	public float vitesseDeplacement;
	public AudioClip sonAttaque;
	public TROGAL_Collider colliderCharge;

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
	}

	public override void faireEtat()
	{

		this.chargerDroitDevant();

		if (colliderCharge.IsPrincesseTouchee ()) {

			princesseVie.blesser (degats, this.gameObject, forceRecule);
			changerEtat(this.GetComponent<TROGAL_E_Phase2_FinCharge>());
		} else if(colliderCharge.IsWallTouchee()){
			changerEtat(this.GetComponent<TROGAL_E_Phase2_Etourdi>());
		}
	}

	public override void sortirEtat()
	{
		
	}

	private void chargerDroitDevant(){

		Vector3 dir = this.transform.forward;
		dir.y = 0.0f;
		dir.Normalize();
		dir = dir * (vitesseDeplacement * Time.deltaTime);

		this.transform.Translate(dir, Space.World);
	}
}
