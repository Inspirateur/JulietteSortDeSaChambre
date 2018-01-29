using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_Combattre : IA_Etat {

	public float distanceSortieCombat;
	public float delaisMaxAvantAttaque;

	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		setAnimation (GOB_Animations.COMBATTRE);
		timer = Time.time + delaisMaxAvantAttaque * Random.value;
	}

	public override void faireEtat()
	{
		agent.seTournerVersPosition (princesse.transform.position);

		if (agent.distanceToPrincesse() >= distanceSortieCombat) {
			changerEtat (GetComponent<GOB_E_Poursuivre> ());
		}

		else if (Time.time >= timer) {
			changerEtat(GetComponent<GOB_E_AttaquerEmpaler>());
		}

	/*	else if () {
			changerEtat(GetComponent<GOB_E_Esquiver>());
		}*/
	}

	public override void sortirEtat()
	{

	}
}
