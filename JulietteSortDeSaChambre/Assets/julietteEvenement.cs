using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class julietteEvenement : Evenement {

	public bool active;

	void Start(){
		active = false;
	}

	// Update is called once per frame
	void Update () {
		if(active){
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().canMove = false;
			Vector3 x = GetComponent<Transform> ().position;
			x.z += 0.1f;
			GetComponent<Transform> ().position = x;
		}
	}

	public void activeDeplacementFinDeNiveau(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().isFin = true;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().LockPrincesse ();
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<camera> ().active = false;

		active = true;
	}
}
