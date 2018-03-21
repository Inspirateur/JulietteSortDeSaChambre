using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleObjetEvenement : Evenement {

	public void setvisibleObjet(bool visible){
		gameObject.SetActive (visible);
	}

}
