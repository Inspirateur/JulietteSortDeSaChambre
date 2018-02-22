using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : ObjetEnvironnemental {


	public EvenementItemList listEvent;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Activation(){
		listEvent.activerEvenement ();
	}
}
