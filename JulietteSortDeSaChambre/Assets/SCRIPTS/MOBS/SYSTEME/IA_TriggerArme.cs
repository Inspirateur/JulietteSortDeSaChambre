using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_TriggerArme : MonoBehaviour {

	private bool princesseTouchee;

	private GameObject princesse;

	// Use this for initialization
	void Start () {
		princesse = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.Equals (princesse)) {
			princesseTouchee = true;
			Debug.Log ("LE SQUELETTE A TOUCHAOW");
		}
	}

	void OnTriggerExit(Collider other){

		if (other.gameObject.Equals (princesse)) {
			princesseTouchee = false;
		}
	}

	public bool IsPrincesseTouchee(){
		return princesseTouchee;
	}
}
