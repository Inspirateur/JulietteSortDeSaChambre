using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barreMetalManager : MonoBehaviour {

	public void StartOpenBarre(int barreNumber){
		this.gameObject.transform.GetChild (barreNumber).gameObject.GetComponent<barresMetalDoor> ().OpenBarre();
	}

    public void StartCloseBarre(int barreNumber)
    {
        this.gameObject.transform.GetChild(barreNumber).gameObject.GetComponent<barresMetalDoor>().CloseBarre();
    }

}
