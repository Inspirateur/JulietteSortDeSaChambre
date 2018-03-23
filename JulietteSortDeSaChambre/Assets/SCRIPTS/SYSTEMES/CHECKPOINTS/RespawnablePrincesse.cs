using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnablePrincesse : RespawnableEntity {

	private PrincesseVie princesseVie;
    private Vector3 position;
    private Quaternion rotation;

	void Awake() {
        princesseVie = GetComponent<PrincesseVie>();
    }

    public override void setInitialState()
    {
        Debug.Log(gameObject.ToString() + " : setInitialState");
        this.position = this.transform.position;
        this.rotation = this.transform.rotation;
    }

    public override void OnRespawn()
    {
        Debug.Log(gameObject.ToString() + " : OnRespawn");
		princesseVie.fullSoigner();
        this.transform.position = this.position;
        this.transform.rotation = this.rotation;
    }
}
