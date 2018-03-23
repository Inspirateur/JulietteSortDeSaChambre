using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RespawnableEntity : MonoBehaviour {

	public CheckPoint checkPoint = null;    // le checkPoint responsable de cette entité
    public bool newCheckPointTracking;

    void Awake()
    {
        if (checkPoint == null){
            Debug.LogWarning("You forgot to assign a checkpoint to object " + gameObject.ToString());
        }
        checkPoint.onRestart += OnRespawn;
        checkPoint.onTrigger += setInitialState;

        if(newCheckPointTracking){
            CheckPointManager.getInstance().onCheckPointChange += updateCheckPoint;
        }
    }

    private void updateCheckPoint(){
        this.checkPoint = CheckPointManager.getInstance().getCurrentCheckPoint();
    }

    public abstract void setInitialState();

    public abstract void OnRespawn();
}
