using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableStatue : RespawnableEntity {

    private shakeObject s;
    private bool canShake;

	public override void onAwake() {
        s = GetComponent<shakeObject>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        canShake = s.canShake;
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        s.canShake = canShake;
    }
}
