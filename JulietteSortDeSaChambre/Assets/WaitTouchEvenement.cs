using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTouchEvenement : Evenement {

	public bool active;
	public bool enCours;

	// Use this for initialization
	void Start () {
		active = false;
		enCours = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(active){
			if(InputManager.GetButtonDown("Interagir")){
				enCours = false;
			}
		}
	}

	public void WaitTouche(){
		active = true;
	}

	public override bool evenementIsEnCours ()
	{
		return enCours;
	}
}
