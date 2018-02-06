using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_Poursuivre : IA_Etat {

	public float vitesse;
	public float dureeRecherchePrincesse;
	public float distanceEntreeCombat;
	public float distanceMaxDash;
	public float distanceMinDash;
	public float pourcentageUtilisationCharge;
	public AudioClip sonPrincessePerdu;

//	public IA_Etat etatSiPrincessePerdue;

	private Vector3 dernierePositionPrincesseConnue;
	private bool princessePerdue;
	private float delaiActuelRecherche;
	private bool enRotation;
	private bool chargePrevue;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		setAnimation(GOB_Animations.COURIR);
		nav.speed = vitesse;
		nav.enabled = true;
		delaiActuelRecherche = 0.0f;
		dernierePositionPrincesseConnue = princesse.transform.position;
		//agent.definirDestination(dernierePositionPrincesseConnue);
		agent.definirDestinationStrat();
		princessePerdue = false;
		enRotation = true;
		chargePrevue = Random.value < pourcentageUtilisationCharge;
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

		} else if (chargePrevue &&
			agent.distanceToPrincesse() <= distanceMaxDash &&
			agent.distanceToPrincesse() >= distanceMinDash &&
			Vector3.Angle(this.transform.forward, princesse.transform.position - this.transform.position) <= 10.0f) {
			
			changerEtat(this.GetComponent<GOB_E_Charger>());

		} else if (agent.destinationCouranteAtteinte ()) {
			
			if (delaiActuelRecherche == 0.0f) {
				princessePerdue = true;
				delaiActuelRecherche = Time.time + dureeRecherchePrincesse;
				setAnimation (GOB_Animations.CHERCHER);
				enRotation = false;
			}

			if (Time.time <= delaiActuelRecherche) {

				if (perception.aRepere(princesse, 2.0f)) {
					setAnimation(GOB_Animations.COURIR);
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
