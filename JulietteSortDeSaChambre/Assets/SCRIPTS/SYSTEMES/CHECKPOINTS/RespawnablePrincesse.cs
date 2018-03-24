using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnablePrincesse : RespawnableEntity {

	private PrincesseVie princesseVie;
    private PrincesseArme princesseArme;
    public EnumArmes armeActive;
    private camera cam;

	void Awake() {
        princesseVie = GetComponent<PrincesseVie>();
        princesseArme = GetComponent<PrincesseArme>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();
    }

    public override void setInitialState()
    {
        Debug.Log(gameObject.ToString() + " : setInitialState");
        this.armeActive = this.princesseArme.armeActive;
    }

    public override void onRespawn()
    {
        Debug.Log(gameObject.ToString() + " : OnRespawn");
		princesseVie.fullSoigner();
        this.princesseArme.SetArmeActive(this.armeActive);
        cam.centrerCamera();
    }
}
