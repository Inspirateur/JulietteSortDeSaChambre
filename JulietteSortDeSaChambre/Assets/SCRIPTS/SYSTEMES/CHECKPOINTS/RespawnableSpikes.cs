using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableSpikes : RespawnableEntity {

    private Spikes spikes;
    private bool active;

	void Awake() {
        spikes = GetComponent<Spikes>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        active = spikes.Activated;
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        spikes.Activated = active;
    }
}
