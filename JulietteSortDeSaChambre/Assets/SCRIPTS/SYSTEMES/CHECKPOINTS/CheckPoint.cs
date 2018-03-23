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

	// Activation du checkPoint
    public void trigger()
    {
        this.triggered = true;

		princesseVie.onDeath += restartCheckPoint;	// quand la princesse va mourir on exécutera restartCheckPoint()

		CheckPointManager.getInstance().OnCheckPointTriggered(this);

		if(onTrigger != null){
			onTrigger();	// on notifit de l'activation d'un nouveau checkPoint
		}

		Debug.Log("CheckPoint \" " + gameObject.name + "\" atteint");
    }

	// Retour au checkPoint
	private void restartCheckPoint(){
		Debug.Log("restartCheckPoint : " + gameObject.name);

		if(onRestart != null){
			onRestart();	// on notifit du retour au checkPoint
		}
	}
}
