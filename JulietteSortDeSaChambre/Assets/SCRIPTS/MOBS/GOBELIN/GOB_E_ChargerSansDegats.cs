using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_ChargerSansDegats : IA_Etat {

	public float impulsionVerticale;
	public float impulsionHorizontale;
	public AudioClip sonCharge;

	private Vector3 direction;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		agent.getSoundEntity().playOneShot(sonCharge, 1.0f);
		this.direction = this.transform.forward;
		rb.AddForce (this.transform.up * impulsionVerticale + this.direction * impulsionHorizontale);
		Debug.Log(this.gameObject.name + " charge !");
		setAnimation (GOB_Animations.CHARGER);
	}

	public override void faireEtat()
	{
		this.transform.forward = this.direction;
		
		if (!agent.isActualAnimation(GOB_Animations.COMBATTRE)) { // l'attaque puissante est toujours en cours

		} else {
			changerEtat(this.GetComponent<GOB_E_Combattre>());
		}
	}

	public override void sortirEtat()
	{
		
	}
}
