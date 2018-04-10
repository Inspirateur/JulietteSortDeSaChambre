using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_EtreEtourdie : IA_Etat {

	public float dureeEtourdissement;
	public Transform stundEffect;

	private float timer;
	private Transform actualEffect;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		this.timer = Time.time + this.dureeEtourdissement;
		actualEffect = Instantiate (stundEffect, this.transform.position + new Vector3(0.0f, 1.3f, 0.0f), stundEffect.transform.rotation);
	}

	public override void faireEtat()
	{
		if(Time.time >= this.timer){
			changerEtat(GetComponent<GOB_E_Combattre>());
		}
	}

	public override void sortirEtat()
	{
		Destroy(actualEffect.gameObject);
	}
}
