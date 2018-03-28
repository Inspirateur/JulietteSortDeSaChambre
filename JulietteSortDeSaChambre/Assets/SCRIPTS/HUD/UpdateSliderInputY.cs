using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSliderInputY : MonoBehaviour {

	// Use this for initialization
	public UnityEngine.UI.Text text;
	private UnityEngine.UI.Slider slider;
	public Camera cam;
	private camera camScript;
	void Start () {
		slider=GetComponents<UnityEngine.UI.Slider>()[0];
		camScript=cam.GetComponent<camera>();
		slider.value=camScript.sensibiliteManetteY;
		text.text=camScript.sensibiliteManetteY.ToString("0.0");
		slider.onValueChanged.AddListener(delegate {UpdateInputY(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateInputY(){
		camScript.sensibiliteManetteY=slider.value;
		text.text=cam.GetComponent<camera>().sensibiliteManetteY.ToString("0.0");
	}
}
