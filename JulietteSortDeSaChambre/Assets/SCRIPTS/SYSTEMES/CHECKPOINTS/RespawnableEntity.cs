using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RespawnableEntity : MonoBehaviour {

    void Start()
    {
        CheckPointManager.getInstance().onRestart += OnRespawn;
        CheckPointManager.getInstance().onCheckPointChange += setInitialState;
        setInitialState();
    }

    public abstract void setInitialState();

    public abstract void OnRespawn();
}
