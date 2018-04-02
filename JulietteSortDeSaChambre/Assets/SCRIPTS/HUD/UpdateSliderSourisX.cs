using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSliderSourisX : MonoBehaviour {

	// Use this for initialization
	public UnityEngine.UI.Text text;
	private UnityEngine.UI.Slider slider;
	public Camera cam;
	private camera camScript;
	void Start () {
		slider=GetComponents<UnityEngine.UI.Slider>()[0];
		camScript=cam.GetComponent<camera>();
		slider.value=camScript.sensibiliteSourisX;
		text.text=camScript.sensibiliteSourisX.ToString("0.0");
		slider.onValueChanged.AddListener(delegate {UpdateInputX(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateInputX(){
		camScript.sensibiliteSourisX=slider.value;
		PlayerPrefs.SetFloat("sensibiliteSourisX",camScript.sensibiliteSourisX);
		text.text=cam.GetComponent<camera>().sensibiliteSourisX.ToString("0.0");
	}
}
