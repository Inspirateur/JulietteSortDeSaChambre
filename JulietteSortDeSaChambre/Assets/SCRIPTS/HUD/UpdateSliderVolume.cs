using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class UpdateSliderVolume : MonoBehaviour, ISelectHandler, IDeselectHandler {

	// Use this for initialization
	public UnityEngine.UI.Text textVolume;
	public UnityEngine.UI.Text textGeneral;

	public UnityEngine.UI.Image fillArea;
	private UnityEngine.UI.Slider slider;

	public SoundManager soundManager;

	//public SoundEntity[] soundEntitys;

	
	void Start () {
		slider=GetComponents<UnityEngine.UI.Slider>()[0];
		slider.value=soundManager.volumeGeneral;
		textVolume.text=slider.value.ToString("0");
		slider.onValueChanged.AddListener(delegate {UpdateText(); });
		//soundEntitys=FindObjectsOfType<SoundEntity>();
		//selectThis();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateText(){
		soundManager.volumeGeneral=(int)slider.value;
		soundManager.notifVolumeChange();
		// for (int i = 0; i < soundEntitys.Length; i++)
		// {
		// 	soundEntitys[i].volumeGeneral=(int)slider.value;
		// }
		PlayerPrefs.SetInt("volumeGeneral",soundManager.volumeGeneral);
		textVolume.text=slider.value.ToString("0");
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
