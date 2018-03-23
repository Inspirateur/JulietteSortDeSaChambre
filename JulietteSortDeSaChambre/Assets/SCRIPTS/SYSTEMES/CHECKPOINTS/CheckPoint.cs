using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	private bool triggered;
    public event MyDelegate onTrigger;
	
    public delegate void MyDelegate();

	void Awake () {
		this.triggered = false;
	}
	
	// called whenever another collider enters our zone (if layers match)
    void OnTriggerEnter(Collider other) {

        // check we haven't been triggered yet!
        if ( !this.triggered && other.gameObject.tag.Equals("Player")) {
			trigger();
        }
    }

	// Activation du checkPoint
    public void trigger() {

		Debug.Log("CheckPoint \" " + gameObject.name + "\" atteint");
		
        this.triggered = true;

		CheckPointManager.getInstance().OnCheckPointTriggered();

		if(onTrigger != null){
			onTrigger();	// on notifit de l'activation d'un nouveau checkPoint
		}
    }

	
}
