using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_TriggerArme : MonoBehaviour {

	private bool princesseTouchee;

	private GameObject princesse;
	private PrincesseVie princesseVie;

	// Use this for initialization
	void Start () {
		princesse = GameObject.FindGameObjectWithTag("Player");
		princesseVie = princesse.GetComponent<PrincesseVie>();
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.Equals (princesse)) {
			princesseTouchee = true;
		}
	}

	void OnTriggerExit(Collider other){

		if (other.gameObject.Equals (princesse)) {
			princesseTouchee = false;
		}
	}

	public bool IsPrincesseTouchee(){
		return princesseTouchee && princesseVie.enVie();
	}
}
