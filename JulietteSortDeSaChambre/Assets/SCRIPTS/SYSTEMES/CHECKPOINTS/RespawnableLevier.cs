using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableLevier : RespawnableEntity {

    private Levier levier;
    private bool active;

	public override void onAwake() {
        levier = GetComponent<Levier>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        active = levier.getActive();
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        levier.setActive(active);
    }
}
