using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barreMetalManager : MonoBehaviour {


	public void DownBarre(int barreNumber){
		this.gameObject.transform.GetChild (barreNumber).gameObject.GetComponent<barresMetalDoor> ().CanDown = true;
	}
}
