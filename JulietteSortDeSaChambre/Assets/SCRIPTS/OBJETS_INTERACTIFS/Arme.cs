using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : ObjetInteractifs {

	public EnumArmes typeArme;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation(){
		GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseArme> ().SetArmeActive (typeArme, this.gameObject);
	}
}
