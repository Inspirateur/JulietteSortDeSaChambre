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
			yield return new WaitForSeconds(itemList[item].dureeArret);

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

