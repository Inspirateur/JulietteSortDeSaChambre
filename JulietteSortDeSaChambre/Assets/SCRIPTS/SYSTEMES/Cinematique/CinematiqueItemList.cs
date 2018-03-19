using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematiqueItemList : ScriptableObject {

	public List<CinematiqueItem> itemList;
	public int item;

	public void lancer(){
		Debug.Log("LANCER : "+(item));
		itemList[item].start ();
		GameControl.control.StartCoroutine (timer ());
	}

	IEnumerator timer(){

		Debug.Log ("test");
		for(var i=0;i<1;i++){
			Debug.Log("DUREE ACCES : "+itemList [item].dureeAcces);
			if(itemList[item].dureeAcces!=0){
				yield return new WaitWhile (() => itemList [item].isInDeplacement);
			}

			if(itemList[item].dureeArret!=0){
				Debug.Log ("CinemtiqueDebutTImerArret");
				yield return new WaitForSeconds(itemList[item].dureeArret);
				Debug.Log ("CinemtiqueDebutTImerArret");
			}

			if (item < itemList.Count - 1) {
				item++;
				lancer ();
			} else {
				GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CinematiqueManager> ().ActiveCinematique (false);
			}

		}
		Debug.Log ("test2");



	}
}

