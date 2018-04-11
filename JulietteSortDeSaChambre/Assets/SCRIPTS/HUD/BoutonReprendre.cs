using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.
using UnityEngine.SceneManagement;

public class BoutonReprendre : MonoBehaviour, ISelectHandler, IDeselectHandler {

	// Use this for initialization
	private int selected;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(selected==1 && (Input.GetButtonDown("Interagir")||Input.GetKeyDown(KeyCode.Return))){
			onClick();
		}else if(selected==2 && (Input.GetButtonDown("Interagir")||Input.GetKeyDown(KeyCode.Return))){
			onClickQuitter();
		}
	}

	public void onClick(){
		GetComponentInParent<AffichagePause>().finPause();
		Debug.Log("Reprendre");
	}

	public void onClickQuitter(){
		//Application.Quit();
		//LoadAsync de la scene
		PlayerPrefs.SetString("SceneToLoad", "SCENES/MenuPrincipal");
        SceneManager.LoadScene("SCENES/LoadingScene");
		Debug.Log("quitter");
	}

	public void OnSelect(BaseEventData eventData){
		Debug.Log("OnSe;ect");
		var color=new Color32(180,180,180,255);
		GetComponentInChildren<UnityEngine.UI.Text>().color=color;
		Debug.Log(eventData.selectedObject.name);
		if(eventData.selectedObject.name=="Reprendre"){
			selected=1;
		}else if(eventData.selectedObject.name=="Quitter"){
			selected=2;
		}
	}

	public void OnDeselect(BaseEventData eventData){
		Debug.Log("OnDese;ect");
		var color=new Color32(255,255,255,255);
		GetComponentInChildren<UnityEngine.UI.Text>().color=color;
		selected=0;
	}
}
