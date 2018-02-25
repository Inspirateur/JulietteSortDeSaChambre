using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socle : ObjetEnvironnemental {

	public int BarreNumberToOpen;
	public GameObject SocleEvenement;


	public override void Activation(){
		this.gameObject.transform.GetChild (0).gameObject.SetActive(true);
		SocleEvenement.GetComponent<barreMetalManager> ().DownBarre(BarreNumberToOpen);
	}
}
