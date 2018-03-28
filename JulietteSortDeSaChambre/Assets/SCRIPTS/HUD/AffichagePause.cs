using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichagePause : MonoBehaviour {

	private Transform[] affiche_Pause;
	public bool etat;

	public Slider[] sliders;
	public Slider activeSlider;
	private int indexSlider;
	private float incValue;


	// Use this for initialization
	void Start () {
		affiche_Pause = gameObject.GetComponentsInChildren<Transform>(true);
		sliders = gameObject.GetComponentsInChildren<Slider>(true);
		indexSlider=0;
		activeSlider=sliders[0];
		etat = true;
		Cursor.visible=true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			if (etat) {
				Pause ();
			} else {
				finPause ();
			}
		}
		float moveHorizontal = InputManager.GetKeyAxis("Horizontal");
        float moveVertical = InputManager.GetKeyAxis("Vertical");
		Debug.Log(moveHorizontal);
		if(moveVertical > 0.0f && !etat){
			indexSlider++;
			if(indexSlider>=sliders.Length){
				indexSlider=sliders.Length-1;
			}
			activeSlider=sliders[indexSlider];
			incValue=activeSlider.maxValue/20;
		}

		if(moveVertical < 0.0f && !etat){
			indexSlider--;
			if(indexSlider<0){
				indexSlider=0;
			}
			activeSlider=sliders[indexSlider];
			incValue=activeSlider.maxValue/20;
		}

		if(moveHorizontal > 0.0f && !etat){
			activeSlider.value+=incValue;
		}

		if(moveHorizontal < 0.0f && !etat){
			activeSlider.value-=incValue;
		}
	}

	public void Pause(){
		Time.timeScale = 0;
		for (int i = 1; i < affiche_Pause.Length; i++) {
			affiche_Pause[i].gameObject.SetActive (true);
		}
		etat = false;
		Cursor.visible=true;
	}

	public void finPause(){
		Time.timeScale = 1;
		for (int i = 1; i < affiche_Pause.Length; i++) {
			affiche_Pause[i].gameObject.SetActive (false);
		}
		etat = true;
		Cursor.visible=false;
	}
}
