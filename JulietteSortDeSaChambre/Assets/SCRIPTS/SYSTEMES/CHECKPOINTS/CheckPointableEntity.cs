using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CheckPointableEntity : MonoBehaviour {

	public CheckPoint checkPoint = null;

    void Awake()
    {
        Debug.Log("CheckPointableEntity Awake start");
        if (checkPoint == null){
            Debug.LogWarning("You forgot to assign a checkpoint to object " + gameObject.ToString());
        }
        checkPoint.onRestart += OnRespawn;
        Debug.Log("CheckPointableEntity Awake end");
    }

    public abstract void OnRespawn();
}
