using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableMob : RespawnableEntity {

    private IA_Agent agent;
    private int vie;

    void Awake() {
        agent = GetComponent<IA_Agent>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        this.vie = agent.getMobVie().getVieCourante();
    }

    public override void onRespawn()
    {
        if(this.vie > 0){
            // Debug.Log(gameObject.ToString() + " : OnRespawn");
            this.agent.getMobVie().SetVie(this.vie);
            this.agent.getAnimator().enabled = false;
            this.agent.getAnimator().enabled = true;
            this.agent.respawn();
        }
    }
}
