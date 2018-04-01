using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateSliderVolume : MonoBehaviour {

	// Use this for initialization
	public UnityEngine.UI.Text text;
	private UnityEngine.UI.Slider slider;

	public SoundManager soundManager;

	public SoundEntity[] soundEntitys;

	
	void Start () {
		slider=GetComponents<UnityEngine.UI.Slider>()[0];
		slider.value=soundManager.volumeGeneral;
		text.text=slider.value.ToString("0");
		slider.onValueChanged.AddListener(delegate {UpdateText(); });
		//selectThis();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateText(){
		soundManager.volumeGeneral=(int)slider.value;
		for (int i = 0; i < soundEntitys.Length; i++)
		{
			soundEntitys[i].volumeGeneral=(int)slider.value;
		}
		PlayerPrefs.SetInt("volumeGeneral",soundManager.volumeGeneral);
		text.text=slider.value.ToString("0");
	}

	public void selectThis(){
		slider.Select();
	}

	void OnSceneLoaded(){
		Debug.Log("Scene loaded");
		soundEntitys=FindObjectsOfType<SoundEntity>();
	}
}
