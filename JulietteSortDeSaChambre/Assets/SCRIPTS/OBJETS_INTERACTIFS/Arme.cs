using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : ObjetInteractifs {

	public EnumArmes typeArme;
	public EnumIconeInterraction iconeInterraction;
	private AffichageObjetRamasser affichageObjetRamasser;

	// Use this for initialization
	void Start () {
		affichageObjetRamasser = GameObject.FindGameObjectWithTag ("HUDAffichageObjetRamasser").GetComponent<AffichageObjetRamasser> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation(){
		affichageObjetRamasser.activeObjet (this);
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseArme> ().SetArmeActive (typeArme, this.gameObject);
	}


	public override EnumIconeInterraction getIconeInteraction(){
		return iconeInterraction;
	}
}
