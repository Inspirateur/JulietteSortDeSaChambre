using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableBarre : RespawnableEntity {

    private barresMetalDoor barre;

	public override void onAwake() {
        barre = GetComponent<barresMetalDoor>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        // barre.isMoving = false;
        // barre.canMove = false;
    }
}
