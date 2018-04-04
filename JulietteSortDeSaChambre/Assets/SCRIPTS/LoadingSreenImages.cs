using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadingSreenImages : MonoBehaviour {
public Sprite[] LoadingImagesArray;
public Image LoadingImage;

	private float timer;
	public float timeToWait;


	// Use this for initialization
	void Start () {
		LoadingImage.sprite = LoadingImagesArray[Random.Range(0,LoadingImagesArray.Length)];
		ResetTimer();
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time > timer){
			LoadingImage.sprite = LoadingImagesArray[Random.Range(0,LoadingImagesArray.Length)];
			ResetTimer();
		}
	}
		private void ResetTimer() {
		timer = Time.time + timeToWait;
	}
}
