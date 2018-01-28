using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_Esquiver : IA_Etat {

	public float impulsionEsquive;
	public float facteurVertical;
	public float dureeEsquive;

	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation (GOB_Animations.ESQUIVER);
		rb.AddForce (this.transform.forward * -1.0f * impulsionEsquive + this.transform.up * impulsionEsquive * facteurVertical);
		timer = Time.time + dureeEsquive;
	}

	public override void faireEtat()
	{
		if (Time.time >= timer) {
			changerEtat (GetComponent<GOB_E_Combattre> ());
		}
	}

	public override void sortirEtat()
	{
		
	}
}
