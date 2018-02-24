using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeMouvante : Evenement {

	override
	public void activation(){
		GetComponent<MovingObject> ().StartPlateformeBeggening ();
	}
}
