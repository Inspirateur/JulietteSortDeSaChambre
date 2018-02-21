using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_Tomber : IA_Etat {

	public int degatsChute;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		this.nav.enabled = false;
	}

	public override void faireEtat()
	{
		if(agent.estAuSol()){
			agent.subirDegats(degatsChute, this.transform.position);
		}
	}

	public override void sortirEtat()
	{
		
	}
}
