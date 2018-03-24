using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableMob : RespawnableEntity {

    private IA_Agent agent;
    // private Vector3 position;
    // private Quaternion rotation;
    private int vie;

    void Awake() {
        agent = GetComponent<IA_Agent>();
    }

    public override void setInitialState()
    {
        Debug.Log(gameObject.ToString() + " : setInitialState");
        // this.position = this.transform.position;
        // this.rotation = this.transform.rotation;
        this.vie = agent.getMobVie().getVieCourante();
        Debug.Log(gameObject.ToString() + " : vie : " + this.vie);
    }

    public override void onRespawn()
    {
        if(this.vie > 0){
            Debug.Log(gameObject.ToString() + " : OnRespawn");
            this.agent.getMobVie().SetVie(this.vie);
            // this.gameObject.SetActive(true);
            // this.transform.position = this.position;
            // this.transform.rotation = this.rotation;
            this.agent.getAnimator().enabled = false;
            this.agent.getAnimator().enabled = true;
            this.agent.respawn();
        }
    }
}
