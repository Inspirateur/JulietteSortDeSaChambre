using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_ColliderCharge : MonoBehaviour {

	private bool princesseTouchee;
	private bool wallTouche;

	private GameObject princesse;

	// Use this for initialization
	void Start () {
		princesse = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject.Equals (princesse)) {
			princesseTouchee = true;
		}
		else if (other.gameObject.tag.Equals("wall")) {
			wallTouche = true;
		}
	}

	void OnTriggerExit(Collider other){

		if (other.gameObject.Equals (princesse)) {
			princesseTouchee = false;
		}
		else if (other.gameObject.tag.Equals("wall")) {
			wallTouche = false;
		}
	}

	public bool IsPrincesseTouchee(){
		return princesseTouchee;
	}

	public bool IsWallTouchee(){
		return wallTouche;
	}
}
