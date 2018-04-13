using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeplacementScripteGobelin : Evenement {

	public IA_PointInteret nouvellePosition;
	// private IA_Agent agent;
	private GOB_E_Garder etatGarder;
	private float timer;

	// Use this for initialization
	void Awake () {
		etatGarder = GetComponent<GOB_E_Garder>();
		timer = Time.time + 2.0f;
	}

	public void changerPosition(){
		IA_PointInteret ancien = etatGarder.emplacementAGarder;
		etatGarder.emplacementAGarder = nouvellePosition;
		etatGarder.entrerEtat();
		etatGarder.emplacementAGarder = ancien;
	}
		
}
