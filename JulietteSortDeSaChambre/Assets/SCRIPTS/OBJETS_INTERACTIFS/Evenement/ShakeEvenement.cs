using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEvenement : Evenement {

	public void shake(bool actif){
		foreach(shakeObject s in GetComponentsInChildren<shakeObject>()){
			s.canShake = actif;
		}
		// GetComponent<shakeObject> ().canShake = actif;
	}
}
