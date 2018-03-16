using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformeMouvante : Evenement {

	override
	public void activation(){
		GetComponent<MovingObject> ().StartPlateformeBeggening ();
	}

	public void test(){

	}

	public void test2(int t){

	}

	public int test3(){
		return 1;
	}
}
