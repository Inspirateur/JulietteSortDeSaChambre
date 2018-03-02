using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobLoot : MonoBehaviour {

	public Transform[] loots;

	private IA_Agent agent;
	private bool lootDone;

	// Use this for initialization
	void Start () {
		this.agent = GetComponent<IA_Agent>();
		lootDone = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!agent.estEnVie() && !lootDone){
			lootDone = true;

			foreach(Transform loot in loots){
				Transform l = Instantiate (loot, this.transform.position, loot.transform.rotation);
			}
		}
	}
}
