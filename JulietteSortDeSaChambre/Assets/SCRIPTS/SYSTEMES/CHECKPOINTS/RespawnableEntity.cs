using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RespawnableEntity : MonoBehaviour {

    private bool existAtLastCheckPoint;
    private Vector3 position;
    private Quaternion rotation;
    private bool actif;

    void Awake() {
        onAwake();
    }

    public abstract void onAwake();

    void Start()
    {
        CheckPointManager.getInstance().onRestart += OnRestart;
        CheckPointManager.getInstance().onCheckPointChange += OnCheckPointChange;
        updateState();
        existAtLastCheckPoint = CheckPointManager.getInstance().isSceneStart();
    }

    public void OnCheckPointChange(){
        updateState();
        this.existAtLastCheckPoint = true;
    }

    private void updateState(){
        setInitialState();
        this.position = this.transform.position;
        this.rotation = this.transform.rotation;
        this.actif = this.gameObject.activeSelf;
        // Debug.Log(gameObject.name + " : " + this.actif);
    }

    public abstract void setInitialState();

    public void OnRestart(){
        if(this.existAtLastCheckPoint){
            this.gameObject.SetActive(this.actif);
            this.transform.position = this.position;
            this.transform.rotation = this.rotation;
            onRespawn();
        }
        else {
            this.gameObject.SetActive(false);
        }
    }

    public abstract void onRespawn();

    void OnDestroy() {
        CheckPointManager.getInstance().onRestart -= OnRestart;
        CheckPointManager.getInstance().onCheckPointChange -= OnCheckPointChange;
    }
}
