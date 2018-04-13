using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableGroupeSocle : RespawnableEntity {

    private GroupeSocle gs;
    private bool active;

	public override void onAwake() {
        gs = GetComponent<GroupeSocle>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        active = gs.active;
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        gs.active = active;
    }
}
