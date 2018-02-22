using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : ObjetEnvironnemental {

	public List<Evenement> listEvenement;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Activation(){
		Debug.Log ("ok");
		foreach (Evenement e in listEvenement) {
			e.activation ();
		}
	}
}
