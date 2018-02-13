using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Poursuivre : IA_Etat {

	public float vitesse;
	public float dureeRecherchePrincesse;
	public float distanceEntreeCombat;
	public float distanceMaxDash;
	public float distanceMinDash;
	public AudioClip sonPrincessePerdu;

//	public IA_Etat etatSiPrincessePerdue;

	private Vector3 dernierePositionPrincesseConnue;
	private bool princessePerdue;
	private float delaiActuelRecherche;
	private bool enRotation;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		nav.speed = vitesse;
		nav.enabled = true;
		delaiActuelRecherche = 0.0f;
		dernierePositionPrincesseConnue = princesse.transform.position;
		//agent.definirDestination(dernierePositionPrincesseConnue);
		agent.definirDestinationStrat();
		princessePerdue = false;
		enRotation = true;
	}

	public override void faireEtat(){
		
		if (!princessePerdue) {
			if (perception.aRepere(princesse, 1.0f) && !dernierePositionPrincesseConnue.Equals (princesse.transform.position)) {
				
				dernierePositionPrincesseConnue = princesse.transform.position;
				//agent.definirDestination (dernierePositionPrincesseConnue);
				agent.definirDestinationStrat();
				enRotation = true;
			}
		}

		if (enRotation) {
			//enRotation = agent.seTournerVersPosition (dernierePositionPrincesseConnue);
			enRotation = agent.seTournerVersPosition (agent.getNav().destination);
		}

		if (agent.distanceToPrincesse() <= distanceEntreeCombat) {

			changerEtat (this.GetComponent<GOB_E_Combattre> ());

		} else if (agent.destinationCouranteAtteinte ()) {
			
			if (delaiActuelRecherche == 0.0f) {
				
				princessePerdue = true;
				delaiActuelRecherche = Time.time + dureeRecherchePrincesse;
				setAnimation (TRO_Animations.CHERCHER);
				enRotation = false;
			}

			if (Time.time <= delaiActuelRecherche) {
				
				if (perception.aRepere(princesse, 2.0f)) {
					princessePerdue = false;
					delaiActuelRecherche = 0.0f;
					dernierePositionPrincesseConnue = princesse.transform.position;
					//agent.definirDestination (dernierePositionPrincesseConnue);
					agent.definirDestinationStrat();
					enRotation = true;
				}
			} else {
				
				agent.getSoundEntity ().playOneShot (sonPrincessePerdu);
				changerEtat (agent.etatInitial);
			}
		}
	}

	public override void sortirEtat() {
		nav.enabled = false;
	}

	public override float getDistanceEntreeCombat(){
		return distanceEntreeCombat;
	}
}
