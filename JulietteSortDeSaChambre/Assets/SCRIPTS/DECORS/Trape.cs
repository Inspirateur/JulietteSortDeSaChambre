using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trape : MonoBehaviour {

	public void TrapeOuverture(){
		GetComponent<Animator> ().SetBool ("CanOpen", true);
		GetComponent<BoxCollider>().enabled=false;
	}
	public void TrapeFermeture(){
		GetComponent<Animator> ().SetBool ("CanOpen", false);
		GetComponent<BoxCollider>().enabled=true;
	}
}
