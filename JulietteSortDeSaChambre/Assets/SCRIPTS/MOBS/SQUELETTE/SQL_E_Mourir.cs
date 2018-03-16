using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQL_E_Mourir : IA_Etat {

	public Transform prefabCadavre;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat() {
		Instantiate(prefabCadavre, this.transform.position, this.transform.rotation);
		this.gameObject.SetActive (false);
	}

	public override void faireEtat()
	{

	}

	public override void sortirEtat()
	{

	}
}
