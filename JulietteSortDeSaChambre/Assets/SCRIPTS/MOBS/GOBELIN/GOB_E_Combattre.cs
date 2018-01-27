using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_Combattre : IA_Etat {

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation (GOB_Animations.COMBATTRE);
	}

	public override void faireEtat()
	{
		
	}

	public override void sortirEtat()
	{

	}
}
