using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableObjetImmuable : RespawnableEntity {

	void Awake() {
        
    }

    public override void setInitialState()
    {
        Debug.Log(gameObject.ToString() + " : setInitialState");
    }

    public override void onRespawn()
    {
        Debug.Log(gameObject.ToString() + " : OnRespawn");
    }
}
