using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour {
	private List<IA_Agent> agents;
	private GameObject princesse;
	private List<IA_Agent> poursuivants;
	private List<Vector3> decalages;
	private float limiteAngle;

	void Awake(){
		agents = new List<IA_Agent>();
		limiteAngle = 100f;
		poursuivants = new List<IA_Agent> ();
		decalages = new List<Vector3> ();
		princesse = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () {}

	private double Distance(IA_Agent agent1, IA_Agent agent2){
		return Mathf.Pow (agent1.getRigidbody ().position.x - agent2.getRigidbody ().position.x, 2) +
		Mathf.Pow (agent1.getRigidbody ().position.y - agent2.getRigidbody ().position.y, 2) +
		Mathf.Pow (agent1.getRigidbody ().position.z - agent2.getRigidbody ().position.z, 2);
	}

	private IA_Agent VoisinProche(IA_Agent agent){
		if(agents.Count > 1){
			double distmin;
			int minid;
			if(agents[0] == agent){
				distmin = Distance (agent, agents [1]);
				minid = 1;
			} else {
				distmin = Distance (agent, agents [0]);
				minid = 0;
			}
			double dist;
			for(int i=0; i<agents.Count; ++i){
				if(agents[i] != agent){
					dist = Distance (agent, agents [i]);
					if(distmin < dist){
						minid = i;
						distmin = dist;
					}
				}
			}
			return agents [minid];
		} else {
			return agent;
		}
	}

	private Vector3 vectAgents(IA_Agent agent1, IA_Agent agent2){
		return agent1.getRigidbody ().position - agent2.getRigidbody ().position;
	}

	public void AjouterAgent(IA_Agent agent){
		agents.Add (agent);
		// Debug.Log ("Agent ajouté, nombre d'agent: " + agents.Count);
	}

	public void notifEtat(IA_Agent agent){
		poursuivants.Remove (agent);
	}

	public Vector3 getDestination(IA_Agent demandeur){
		//POUR TESTER AVEC/SANS: commenter/décommenter ça
		//return princesse.transform.position;

		if (!poursuivants.Contains (demandeur)) {
			poursuivants.Add (demandeur);
			// Debug.Log ("poursuivant ajouté");
		}
		if (poursuivants.Count == 1) {
			return princesse.transform.position;
		} else if (poursuivants.Count == 2) {
			//Dans ce cas très courant, on va simplement maximiser l'angle entre les vecteurs Gob1Princesse et Gob2Princesse
			//On récupère d'abord l'angle entre les 2 poursuivants(le "max" est de 180° ou -180°, quand les 2 vecteurs sont opposés)
			IA_Agent other;
			if (demandeur.Equals (poursuivants [0])) {
				other = poursuivants [1];
			} else {
				other = poursuivants [0];
			}
			Vector3 Gob1Princesse = demandeur.transform.position - princesse.transform.position;
			Vector3 Gob2Princesse = other.transform.position - princesse.transform.position;
			float angle = Vector3.SignedAngle (Gob1Princesse, Gob2Princesse, new Vector3 (0, 1, 0));
			// Debug.Log ("2 poursuivants - angle: " + angle);
			if (angle > -100 && angle < 100) {
				//On écarte le demandeur
				Vector3 decalageDemandeur;
				if (angle < 0) {
					//Le demandeur doit aller a sa droite
					decalageDemandeur = Vector3.Cross (princesse.transform.up, Gob1Princesse);
				} else {
					//Le demandeur doit aller a sa gauche
					decalageDemandeur = Vector3.Cross (Gob1Princesse, princesse.transform.up);
				}
				decalageDemandeur = decalageDemandeur.normalized * (demandeur.getEtatCourant ().getDistanceEntreeCombat () - 0.1f);
				return princesse.transform.position + decalageDemandeur;
			} else {
				//Les 2 poursuivants sont déjà bien répartis
				return princesse.transform.position;
			}
		} else { // Ce traitement est lourd donc on garde les résultats et on les rafraichit toute les secondes
			//Dans ce cas il y a plus de 2 poursuivants: le but est que chaque goblin s'éloigne de son voisin le plus proche (dans le repere de juliette)
			float angleMin1 = 180;
			float angleMin2 = -180;
			int idmin1 = 0;
			int idmin2 = 0;
			float angleComp;
			Vector3 GobPrincesse = demandeur.transform.position - princesse.transform.position;
			//On cherche le voisin le plus proche du demandeur
			for (int j = 0; j < poursuivants.Count; ++j) {
				if (!demandeur.Equals (poursuivants [j])) {
					angleComp = Vector3.SignedAngle (GobPrincesse, poursuivants [j].transform.position - princesse.transform.position, new Vector3 (0, 1, 0));
					if (angleComp > 0 && angleComp < angleMin1) {
						angleMin1 = angleComp;
						idmin1 = j;
					} else if (angleComp < 0 && angleComp > angleMin2) {
						angleMin2 = angleComp;
						idmin2 = j;
					}
				}
			}
			//Si la diff d'ecart entre les 2 voisins est plus grande que le plus petit ecart
			angleMin2 = Mathf.Abs(angleMin2);
			if (Mathf.Abs(angleMin1-angleMin2) > Mathf.Min(angleMin1,angleMin2)) {
				Vector3 decalage;
				if (angleMin1 > angleMin2) {
					decalage = Vector3.Cross (princesse.transform.up, GobPrincesse);
				} else {
					decalage = Vector3.Cross (GobPrincesse, princesse.transform.up);
				}
				return princesse.transform.position + decalage.normalized * (demandeur.getEtatCourant ().getDistanceEntreeCombat () - 0.1f);
			} else {
				return princesse.transform.position;
			}
		}
	}
}
