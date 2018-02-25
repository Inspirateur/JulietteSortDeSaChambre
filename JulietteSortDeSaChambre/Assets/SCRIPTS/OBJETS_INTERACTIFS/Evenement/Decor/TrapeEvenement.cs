using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapeEvenement : Evenement {

	// private List<IA_Agent> listeGobelins;

	void Start () {
		// this.listeGobelins = new List<IA_Agent> ();
	}
	
	public override void activation(){
		Trape t = this.GetComponent<Trape> ();
		t.TrapeOuverture ();
		
		// foreach( IA_Agent gob in listeGobelins){
		// 	gob.tomber();
		// }
		// this.listeGobelins.Clear();
	}
	
	public override void desactivation(){
		Trape t = this.GetComponent<Trape> ();
		t.TrapeFermeture ();
	}

	// void OnCollisionEnter(Collision collisionInfo) {
	// 	Debug.Log("enter " + collisionInfo.collider.gameObject.name);
	// 	if(!collisionInfo.collider.gameObject.Equals(GameObject.FindGameObjectWithTag("Player"))){
	// 		IA_Agent gob = collisionInfo.collider.gameObject.GetComponent<IA_Agent> ();
			
	// 		if (!listeGobelins.Contains (gob)) {
						
	// 			listeGobelins.Add (gob);
	// 		}
	// 	}
    // }

	// void OnCollisionExit(Collision collisionInfo) {
	// 	Debug.Log("exit " + collisionInfo.collider.gameObject.name);
	// 	if(!collisionInfo.collider.gameObject.Equals(GameObject.FindGameObjectWithTag("Player"))){
	// 		IA_Agent gob = collisionInfo.collider.gameObject.GetComponent<IA_Agent> ();

	// 		if (listeGobelins.Contains (gob)) {
						
	// 			listeGobelins.Remove (gob);
	// 		}
	// 	}
    // }
}
