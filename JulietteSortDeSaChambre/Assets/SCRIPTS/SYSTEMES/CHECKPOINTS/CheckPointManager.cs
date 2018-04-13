using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {

    private static CheckPointManager instance;
    public event MyDelegate onCheckPointChange;
    public event MyDelegate onRestart;
	
    public delegate void MyDelegate();
	private bool sceneStart;
	public int nbRestart;

    void Awake() {

        CheckPointManager.instance = this;
		sceneStart = true;
		nbRestart = 0;
    }

	void Update() {
		sceneStart = false;
	}

	public static CheckPointManager getInstance(){
		return CheckPointManager.instance;
	}

	public bool isSceneStart() {
		return this.sceneStart;
	}

    public void OnCheckPointTriggered() {
		Debug.Log(gameObject.ToString() + " : OnCheckPointTriggered");
		if(onCheckPointChange != null){
			Debug.Log(gameObject.ToString() + " : onCheckPointChange");
			onCheckPointChange();
		}
    }

	// Retour au checkPoint
	public void restartCheckPoint(){
		Debug.Log("restartCheckPoint");
		nbRestart ++;
		if(onRestart != null){
			Debug.Log("onRestart");
			onRestart();	// on notifit du retour au checkPoint
		}
	}
}
