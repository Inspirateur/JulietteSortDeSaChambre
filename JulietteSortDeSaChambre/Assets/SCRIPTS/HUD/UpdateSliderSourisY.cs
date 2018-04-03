using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSliderSourisY : MonoBehaviour {

	// Use this for initialization
	public UnityEngine.UI.Text text;
	private UnityEngine.UI.Slider slider;
	public Camera cam;
	private camera camScript;
	void Start () {
		slider=GetComponents<UnityEngine.UI.Slider>()[0];
		camScript=cam.GetComponent<camera>();
		slider.value=camScript.sensibiliteSourisY;
		text.text=camScript.sensibiliteSourisY.ToString("0.0");
		slider.onValueChanged.AddListener(delegate {UpdateInputX(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateInputX(){
		camScript.sensibiliteSourisY=slider.value;
		PlayerPrefs.SetFloat("sensibiliteSourisY",camScript.sensibiliteSourisY);
		text.text=cam.GetComponent<camera>().sensibiliteSourisY.ToString("0.0");
	}
}
