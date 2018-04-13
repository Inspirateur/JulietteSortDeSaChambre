using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCharacter : MonoBehaviour {

	private PrincesseDeplacement deplacement;

	void Start() {
		deplacement = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>();
	}

	void OnTriggerStay (Collider col) 
	{
		if (col.tag == ("Player") && !deplacement.attaqueBegin) {
			col.transform.parent = gameObject.transform;
		}
	}

	void OnTriggerExit (Collider col) 
	{
		col.transform.parent = null;
	}
}
