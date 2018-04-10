using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematiqueItemList : ScriptableObject {

	public List<CinematiqueItem> itemList;
	public int item;
	public bool isPassable;
	public bool desactiveBandeNoir;


	private Coroutine actualCinematique;


	public void lancer(){
//		Debug.Log("LANCER : "+(item));
		if (itemList [item].isShaking) {
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<camera> ().activeShaking ();
			GameControl.control.StartCoroutine (timerShaking ());
		} else {
			itemList[item].start ();
			actualCinematique = GameControl.control.StartCoroutine (timer ());
		}

	}

	IEnumerator timerShaking(){

		//	Debug.Log ("test");
		for(var i=0;i<1;i++){
			//	Debug.Log("DUREE ACCES : "+itemList [item].dureeAcces);

			yield return new WaitWhile (() => GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<camera> ().shakingIsActive);
			cinematiqueSuivant ();

		}
		//	Debug.Log ("test2");
	
	}

	IEnumerator timer(){

	//	Debug.Log ("test");
		for(var i=0;i<1;i++){
		//	Debug.Log("DUREE ACCES : "+itemList [item].dureeAcces);
			if(itemList[item].dureeAcces!=0){
				yield return new WaitWhile (() => itemList [item].isInDeplacement);
			}

			if(itemList[item].dureeArret!=0){
		//		Debug.Log ("CinemtiqueDebutTImerArret");
				yield return new WaitForSeconds(itemList[item].dureeArret);
		//		Debug.Log ("CinemtiqueDebutTImerArret");
			}
				
			cinematiqueSuivant ();



		}
	//	Debug.Log ("test2");



	}

	private void cinematiqueSuivant(){
		GameObject.FindGameObjectWithTag ("HUDAffichageCinematique").GetComponent<AffichageCinematique> ().desactiveText ();
		if (item < itemList.Count - 1) {
			item++;
			lancer ();
		} else {
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CinematiqueManager> ().ActiveCinematique (false);
		}
	}

	public void stopCinematique(){
		GameControl.control.StopCoroutine (actualCinematique);
		itemList [item].stop ();
	}
}

