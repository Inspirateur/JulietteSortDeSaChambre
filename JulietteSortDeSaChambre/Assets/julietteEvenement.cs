using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class julietteEvenement : Evenement {

	public bool active;
	public bool encours;
	public bool activeDeux;

	void Start(){
		active = false;
		encours = true;
		activeDeux = false;
	}

	// Update is called once per frame
	void Update () {
		if(active){
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().canMove = false;
			Vector3 x = GetComponent<Transform> ().position;
			x.z += 0.1f;
			GetComponent<Transform> ().position = x;

			if(InputManager.GetButtonDown("Interagir")){
				encours = false;
			}
		}

		if(activeDeux){
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().canMove = false;
			Vector3 x = GetComponent<Transform> ().position;
			x.z -= 0.1f;
			GetComponent<Transform> ().position = x;

			if(InputManager.GetButtonDown("Interagir")){
				encours = false;
			}
		}
	}

	public void activeDeplacementFinDeNiveau(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().isFin = true;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().LockPrincesse ();
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<camera> ().active = false;

		active = true;
	}

	public void activeDeplacementFinDeNiveauDeux(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().isFin = true;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().LockPrincesse ();
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<camera> ().active = false;

		activeDeux = true;
	}

	public override bool evenementIsEnCours ()
	{
		return encours;
	}
}
