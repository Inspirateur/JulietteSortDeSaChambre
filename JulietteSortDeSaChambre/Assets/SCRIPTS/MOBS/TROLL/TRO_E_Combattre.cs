using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Combattre : IA_Etat {

	public float distanceSortieCombat;
	public float delaisMaxAvantAttaque;
	public float delaisMinAvantAttaque;
	public float pourcentageAttaqueEmpaler;
	public float delaisEsquive;
	public float pourcentageEsquive;

	private float timerAttaque;
	private float timerEsquive;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
		timerEsquive = Time.time;
	}

	public override void entrerEtat()
	{
		setAnimation (GOB_Animations.COMBATTRE);
		timerAttaque = Time.time + delaisMinAvantAttaque + (delaisMaxAvantAttaque - delaisMinAvantAttaque) * Random.value;
	}

	public override void faireEtat()
	{
		agent.seTournerVersPosition (princesse.transform.position);

		if (agent.distanceToPrincesse() >= distanceSortieCombat) {
			changerEtat (GetComponent<GOB_E_Poursuivre> ());
		}

		else if (Time.time >= timerAttaque) {
			float choixAttaque = Random.value;

			if (choixAttaque <= pourcentageAttaqueEmpaler) {
				changerEtat (GetComponent<GOB_E_AttaquerEmpaler> ());
			} else {
				changerEtat (GetComponent<GOB_E_AttaquerHorizontalement> ());
			}
		}

		else if (princesseArme.isAttaqueEnCours() && Time.time >= timerEsquive) {
			timerEsquive = Time.time + delaisEsquive;
			if(Random.value <= pourcentageEsquive){
				changerEtat(GetComponent<GOB_E_Esquiver>());
			}
		}
	}

	public override void sortirEtat()
	{

	}
}
