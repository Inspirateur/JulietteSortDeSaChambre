using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;// Required when using Event data.

public class UpdateSliderSourisX : MonoBehaviour, ISelectHandler, IDeselectHandler {

	// Use this for initialization
	public UnityEngine.UI.Text textVolume;
	public UnityEngine.UI.Text textGeneral;
	public UnityEngine.UI.Image fillArea;
	private UnityEngine.UI.Slider slider;
	public Camera cam;
	private camera camScript;
	void Start () {
		slider=GetComponents<UnityEngine.UI.Slider>()[0];
		camScript=cam.GetComponent<camera>();
		slider.value=camScript.sensibiliteSourisX;
		textVolume.text=camScript.sensibiliteSourisX.ToString("0.0");
		slider.onValueChanged.AddListener(delegate {UpdateInputX(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateInputX(){
		camScript.sensibiliteSourisX=slider.value;
		PlayerPrefs.SetFloat("sensibiliteSourisX",camScript.sensibiliteSourisX);
		textVolume.text=cam.GetComponent<camera>().sensibiliteSourisX.ToString("0.0");
	}

	public void OnSelect(BaseEventData eventData){
		Debug.Log("OnSe;ect");
		var color=new Color32(180,180,180,255);
		textGeneral.color=color;
		//textVolume.color=color;
		fillArea.color=new Color32(180,180,180,255);
	}

	public void OnDeselect(BaseEventData eventData){
		Debug.Log("OnDese;ect");
		var color=new Color32(255,255,255,255);
		textGeneral.color=color;
		textVolume.color=color;
		fillArea.color=color;
	}
}
