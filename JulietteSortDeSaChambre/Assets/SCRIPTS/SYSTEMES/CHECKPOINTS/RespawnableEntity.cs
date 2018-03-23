using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RespawnableEntity : MonoBehaviour {

	public CheckPoint checkPoint = null;

    void Awake()
    {
        if (checkPoint == null){
            Debug.LogWarning("You forgot to assign a checkpoint to object " + gameObject.ToString());
        }
        checkPoint.onRestart += OnRespawn;
        checkPoint.onTrigger += setInitialState;
        // setInitialState();
    }

    public abstract void setInitialState();

    public abstract void OnRespawn();
}
