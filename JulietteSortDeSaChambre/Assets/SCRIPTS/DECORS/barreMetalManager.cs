using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barreMetalManager : MonoBehaviour {

	public void BarreDown(int number){
		this.gameObject.transform.GetChild (number).gameObject.GetComponent<barresMetalDoor> ().CanDown = true;
	}
}
