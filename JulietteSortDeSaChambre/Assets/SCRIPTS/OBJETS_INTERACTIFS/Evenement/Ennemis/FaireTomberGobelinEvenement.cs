using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaireTomberGobelinEvenement : Evenement {

	private List<IA_Agent> listeGobelins;

	void Start () {
		this.listeGobelins = new List<IA_Agent> ();
	}
	
	public override void activation(){
		foreach( IA_Agent gob in listeGobelins){
			gob.changerEtat(
				gob.GetComponent<GOB_E_Tomber>());
		}
	}
	
	public override void desactivation(){
		
	}

	void OnTriggerEnter(Collider other) {

		IA_Agent gob = other.gameObject.GetComponent<IA_Agent> ();

		if (!listeGobelins.Contains (gob)) {
					
			listeGobelins.Add (gob);
		}
    }

	void OnTriggerExit(Collider other)
    {
		IA_Agent gob = other.gameObject.GetComponent<IA_Agent> ();

		if (listeGobelins.Contains (gob)) {
					
			listeGobelins.Remove (gob);
		}
    }
}
