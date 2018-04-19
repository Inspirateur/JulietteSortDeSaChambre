using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_PretACharger : IA_Etat {

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		nav.enabled = false;
	}

	public override void faireEtat()
	{
		changerEtat (this.GetComponent<GOB_E_ChargerSansDegats> ());
	}

	public override void sortirEtat()
	{
		
	}
}
