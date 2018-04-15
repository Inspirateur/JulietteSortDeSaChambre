using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEvenement : Evenement {

	private bool enCours;
	private float timeFinal;
	private bool timer;


	void Start(){
		enCours = true;
		timer=false;
	}

	public void launchTimer(int i){
		enCours = true;
		timeFinal = Time.time + i;
		timer = true;

	}

	void Update(){
		if(timer){
			if(Time.time>timeFinal){
				Debug.Log ("Fine");
				enCours = false;
				timer = false;
			}
		}

	}



	public override bool evenementIsEnCours ()
	{
		
		return enCours;
	}
}
