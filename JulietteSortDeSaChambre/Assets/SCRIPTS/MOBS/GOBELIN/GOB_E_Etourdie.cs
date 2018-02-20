using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_Etourdie : IA_Etat {

	public float dureeEtourdissement;

	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		this.timer = Time.time + this.dureeEtourdissement;
	}

	public override void faireEtat()
	{
		if(Time.time >= this.timer){
			changerEtat(GetComponent<GOB_E_Combattre>());
		}
	}

	public override void sortirEtat()
	{
		this.anim.enabled = true;
	}
}
