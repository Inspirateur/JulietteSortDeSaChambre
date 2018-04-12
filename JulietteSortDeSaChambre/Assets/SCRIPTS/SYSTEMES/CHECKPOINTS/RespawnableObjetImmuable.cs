using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableObjetImmuable : RespawnableEntity {

	public override void onAwake() {
        
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
    }
}
