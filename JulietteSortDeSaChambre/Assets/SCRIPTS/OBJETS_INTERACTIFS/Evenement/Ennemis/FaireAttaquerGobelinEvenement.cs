using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaireAttaquerGobelinEvenement : Evenement {

	public IA_Agent[] listeGobelins;
	public GameObject[] listeStatues;
	public Transform effetBreak;

	void Start () {
		
	}
	
	public override void activation(){

		foreach( GameObject s in listeStatues){
			s.SetActive(false);
			Instantiate (effetBreak, s.transform.position + s.transform.up * 1.5f, effetBreak.transform.rotation);
		}

		foreach( IA_Agent gob in listeGobelins){
			gob.gameObject.SetActive(true);
		}
	}
	
	public override void desactivation(){
		
	}
}
