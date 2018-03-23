using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {

    private static CheckPointManager instance;
	private PrincesseVie princesseVie;
    public event MyDelegate onCheckPointChange;
    public event MyDelegate onRestart;
	
    public delegate void MyDelegate();

    void Awake() {

        CheckPointManager.instance = this;
		princesseVie = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseVie>();
		princesseVie.onDeath += restartCheckPoint;	// quand la princesse va mourir on exécutera restartCheckPoint()
    }

	public static CheckPointManager getInstance(){
		return CheckPointManager.instance;
	}

    public void OnCheckPointTriggered() {
		Debug.Log(gameObject.ToString() + " : OnCheckPointTriggered");
		if(onCheckPointChange != null){
			Debug.Log(gameObject.ToString() + " : onCheckPointChange");
			onCheckPointChange();
		}
    }

	// Retour au checkPoint
	private void restartCheckPoint(){
		Debug.Log("restartCheckPoint");

		if(onRestart != null){
			Debug.Log("onRestart");
			onRestart();	// on notifit du retour au checkPoint
		}
	}
}
