using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : MonoBehaviour {
	private List<IA_Agent> agents;
	private GameObject princesse;

	void Awake(){
		agents = new List<IA_Agent> ();
		princesse = GameObject.FindGameObjectWithTag("Player");
	}

	// Use this for initialization
	void Start () {

	}

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
		print ("Agent ajouté, nombre d'agent: " + agents.Count);
	}

	public Vector3 getDestination(IA_Agent demandeur){
		return princesse.transform.position+new Vector3(0,0,2);
	}
}
