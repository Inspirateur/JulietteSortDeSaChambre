using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerEvenement : Evenement {

	private bool enCours;

	public void launchTimer(int i){
		enCours = true;
		StartCoroutine (timer ((float)i));
	}


	IEnumerator timer(float time){

		//	Debug.Log ("test");
		for(var i=0;i<1;i++){
			//	Debug.Log("DUREE ACCES : "+itemList [item].dureeAcces);

			yield return new WaitForSeconds(time);

		}
		enCours = false;
		//	Debug.Log ("test2");

	}

	public override bool evenementIsEnCours ()
	{
		return enCours;
	}
}
