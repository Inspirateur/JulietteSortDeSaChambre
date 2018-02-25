using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaireAttaquerGobelinEvenement : Evenement {

	public IA_Agent[] listeGobelins;

	void Start () {
		
	}
	
	public override void activation(){

		foreach( IA_Agent gob in listeGobelins){
			gob.changerEtat(gob.GetComponent<GOB_E_Charger>());
			IA_Perception p = gob.GetComponent<IA_Perception>();
			p.estAveugle = false;
			p.estSourd = false;
		}
	}
	
	public override void desactivation(){
		
	}
}
