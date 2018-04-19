using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleObjetEvenement : Evenement {

	public void setvisibleObjet(bool visible){
		gameObject.SetActive (visible);
	}

	public void setvisibleObjetChild(bool visible){
		foreach(Transform t in gameObject.GetComponentsInChildren<Transform>()){
			t.gameObject.SetActive (visible);
		}


	}

}
