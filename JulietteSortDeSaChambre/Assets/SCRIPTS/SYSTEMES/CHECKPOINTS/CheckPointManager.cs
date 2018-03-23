using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour {

    private static CheckPointManager instance;

    private CheckPoint currentCheckPoint;
    public event MyDelegate onCheckPointChange;
	
    public delegate void MyDelegate();

    void Awake() {

        CheckPointManager.instance = this;
    }

	public static CheckPointManager getInstance(){
		return CheckPointManager.instance;
	}

	public CheckPoint getCurrentCheckPoint(){
		return this.currentCheckPoint;
	}

    public void OnCheckPointTriggered(CheckPoint newCheckPoint)
    {
        this.currentCheckPoint = newCheckPoint;

		if(onCheckPointChange != null){
			onCheckPointChange();
		}
    }
}
