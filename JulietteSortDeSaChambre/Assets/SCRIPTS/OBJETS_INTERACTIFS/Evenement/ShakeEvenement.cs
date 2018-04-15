using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEvenement : Evenement {

	public void shake(bool actif){
		GetComponent<shakeObject> ().canShake = actif;
	}
}
