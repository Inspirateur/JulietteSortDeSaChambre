using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableTrape : RespawnableEntity {

    private Trape trape;
    private bool etatOuvert;

	void Awake() {
        trape = GetComponent<Trape>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        etatOuvert = trape.etatOuvert;
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        trape.etatOuvert = etatOuvert;
        trape.updateEtat();
    }
}
