using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class julietteEvenement : Evenement {

	public bool active;
	public bool encours;
	public bool activeDeux;
	public bool activeTrois;
	public bool activeQ;

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
		if(activeTrois){
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().canMove = false;
			Vector3 x = GetComponent<Transform> ().position;
			x.x += 0.1f;
			GetComponent<Transform> ().position = x;

			if(GetComponent<Transform> ().localPosition.x >17.2f){
				encours = false;
				activeTrois = false;
				activeQ = true;
			}
		}
		if(activeQ){
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().canMove = false;
			Vector3 x = GetComponent<Transform> ().position;
			x.x += 0.08f;
			GetComponent<Transform> ().position = x;

			if(GetComponent<Transform> ().localPosition.x >20.0f){
				activeQ = false;
				end ();
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

	public void activeDeplacementDebutNiveaux(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().isFin = true;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().LockPrincesse ();
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<camera> ().active = false;

		activeTrois = true;
	}

	public void end(){
		
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().isFin = false;
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseDeplacement> ().UnlockPrincesse ();
		GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<camera> ().active = true;
	}

	public override bool evenementIsEnCours ()
	{
		return encours;
	}
}
