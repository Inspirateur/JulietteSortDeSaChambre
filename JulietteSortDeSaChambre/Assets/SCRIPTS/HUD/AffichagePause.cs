using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichagePause : MonoBehaviour {

	private Transform[] affiche_Pause;
	public bool etat;
    private PrincesseVie princesseVie;


    // Use this for initialization
    void Start () {
        princesseVie = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseVie>();
        affiche_Pause = gameObject.GetComponentsInChildren<Transform>(true);
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
		etat = false;
	}

	public void finPause(){
		Time.timeScale = 1;
		for (int i = 1; i < affiche_Pause.Length; i++) {
			affiche_Pause[i].gameObject.SetActive (false);
		}
		etat = true;
	}
}
