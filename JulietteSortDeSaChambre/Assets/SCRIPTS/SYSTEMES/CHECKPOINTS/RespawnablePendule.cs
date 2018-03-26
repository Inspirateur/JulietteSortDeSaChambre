using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnablePendule : RespawnableEntity {

    private pendule pend;
    private bool stop;
    private float time;

	void Awake() {
        pend = GetComponent<pendule>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        stop = pend.stop;
        time = pend.getTime();
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        pend.stop = stop;
        pend.setTime(time);
    }
}
