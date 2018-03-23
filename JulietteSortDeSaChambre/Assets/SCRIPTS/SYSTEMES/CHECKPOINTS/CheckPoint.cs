using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	private bool triggered;
	private PrincesseVie princesseVie;
    public event MyDelegate onRestart;
    public event MyDelegate onTrigger;
	
    public delegate void MyDelegate();

	void Awake () {
		this.triggered = false;
		princesseVie = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseVie>();
	}
	
	// called whenever another collider enters our zone (if layers match)
    void OnTriggerEnter(Collider other) {

        // check we haven't been triggered yet!
        if ( !this.triggered && other.gameObject.tag.Equals("Player")) {
			trigger();
        }
    }
    public void trigger()
    {
        this.triggered = true;

		princesseVie.onDeath += restartCheckPoint;

		if(onTrigger != null){
			onTrigger();
		}

		Debug.Log("CheckPoint atteint");
    }

	private void restartCheckPoint(){
		Debug.Log("restartCheckPoint");

		if(onRestart != null){
			onRestart();
		}
	}
}
