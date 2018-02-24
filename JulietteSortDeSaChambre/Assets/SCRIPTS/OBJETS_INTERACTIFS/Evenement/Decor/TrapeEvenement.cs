using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapeEvenement : Evenement {

	override
	public void activation(){
		Trape t = this.GetComponent<Trape> ();
		t.TrapeOuverture ();
	}

	override
	public void desactivation(){
		Trape t = this.GetComponent<Trape> ();
		t.TrapeFermeture ();
	}
}
