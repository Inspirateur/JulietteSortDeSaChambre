using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableMob : RespawnableEntity {

    private IA_Agent agent;
    private Vector3 position;
    private Quaternion rotation;
    private int vie;

    void Start() {
        agent = GetComponent<IA_Agent>();
    }

    public override void setInitialState()
    {
        this.position = this.transform.position;
        this.rotation = this.transform.rotation;
        this.vie = this.GetComponent<IA_MobVie>().getVieCourante();
    }

    public override void OnRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : RESTART");
        this.agent.getMobVie().SetVie(this.vie);
        this.gameObject.SetActive(true);
        this.transform.position = this.position;
        this.transform.rotation = this.rotation;
        this.agent.getAnimator().enabled = false;
        this.agent.getAnimator().enabled = true;
        this.agent.respawn();
    }
}
