using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaireTomberGobelinEvenement : Evenement {

	private List<IA_Agent> listeGobelins;

	void Awake () {
		this.listeGobelins = new List<IA_Agent> ();
	}
	
	public override void activation(){

		foreach( IA_Agent gob in listeGobelins){
			gob.tomber();
		}
	}
	
	public override void desactivation(){
		
	}

	void OnTriggerEnter(Collider other) {
		// Debug.Log("enter " + other.gameObject.name);
		if(other.gameObject.tag.Equals("Mob")){
			// Debug.Log("Add to liste");
			IA_Agent gob = other.gameObject.GetComponent<IA_Agent> ();
			
			if (!listeGobelins.Contains (gob)) {
						
				listeGobelins.Add (gob);
			}
		}
    }

	void OnTriggerExit(Collider other)
    {
		// Debug.Log("exit " + other.gameObject.name);
		if(other.gameObject.tag.Equals("Mob")){
			// Debug.Log("Remove from liste");
			IA_Agent gob = other.gameObject.GetComponent<IA_Agent> ();

			if (listeGobelins.Contains (gob)) {
						
				listeGobelins.Remove (gob);
			}
		}
    }
}
