using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class BoutonReprendre : MonoBehaviour, ISelectHandler, IDeselectHandler {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onClick(){
		GetComponentInParent<AffichagePause>().finPause();
	}

	public void onClickQuitter(){
		Debug.Log("quitter");
	}

	public void OnSelect(BaseEventData eventData){
		Debug.Log("OnSe;ect");
		var color=new Color32(180,180,180,255);
		GetComponentInChildren<UnityEngine.UI.Text>().color=color;
	}

	public void OnDeselect(BaseEventData eventData){
		Debug.Log("OnDese;ect");
		var color=new Color32(255,255,255,255);
		GetComponentInChildren<UnityEngine.UI.Text>().color=color;
	}
}
