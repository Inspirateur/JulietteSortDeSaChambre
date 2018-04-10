using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AffichagePause : MonoBehaviour {

	private Transform[] affiche_Pause;
	public bool etat;
    private PrincesseVie princesseVie;
	public Slider activeSlider;
	private int indexSlider;
	private float incValue;


    // Use this for initialization
    void Start () {
        princesseVie = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseVie>();
        affiche_Pause = gameObject.GetComponentsInChildren<Transform>(true);
		//sliders = gameObject.GetComponentsInChildren<Slider>(true);
		indexSlider=0;
		//activeSlider=sliders[0];
		activeSlider=gameObject.GetComponentInChildren<Slider>(true);
		//activeSlider.GetComponent<UpdateSliderVolume>().selectThis();
		etat = true;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Pause") && princesseVie.enVie()) {
			if (etat) {
				Pause ();
			} else {
				finPause ();
			}
		}
	}

	public void Pause(){
		Time.timeScale = 0;
		for (int i = 1; i < affiche_Pause.Length; i++) {
			affiche_Pause[i].gameObject.SetActive (true);
		}
		activeSlider.Select();
		etat = false;
		Cursor.visible=true;
	}

	public void finPause(){
		Time.timeScale = 1;
		activeSlider.Select();
		for (int i = 1; i < affiche_Pause.Length; i++) {
			affiche_Pause[i].gameObject.SetActive (false);
		}
		etat = true;
		Cursor.visible=false;
	}
}
