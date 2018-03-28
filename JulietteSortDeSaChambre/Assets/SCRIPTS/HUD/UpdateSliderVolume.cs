using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSliderVolume : MonoBehaviour {

	// Use this for initialization
	public UnityEngine.UI.Text text;
	private UnityEngine.UI.Slider slider;

	public SoundManager soundManager;
	public SoundEntity soundEntity;

	
	void Start () {
		slider=GetComponents<UnityEngine.UI.Slider>()[0];
		slider.value=soundManager.volumeGeneral;
		text.text=slider.value.ToString("0");
		slider.onValueChanged.AddListener(delegate {UpdateText(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateText(){
		soundManager.volumeGeneral=(int)slider.value;
		soundEntity.volumeGeneral=(int)slider.value;
		text.text=slider.value.ToString("0");
	}
}
