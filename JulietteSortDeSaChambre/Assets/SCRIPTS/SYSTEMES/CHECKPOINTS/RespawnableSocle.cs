using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableSocle : RespawnableEntity {

    private Socle socle;
    private bool utilise;

	void Awake() {
        socle = GetComponent<Socle>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        utilise = socle.utilise;
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        if(socle.utilise && !utilise){
            socle.Desactivation();
        }
    }
}
