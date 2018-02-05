using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageVie : MonoBehaviour {

	private PrincesseVie juliette;
	private UnityEngine.UI.Text textVie;

	// Use this for initialization
	void Start () {
		juliette = GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseVie> ();
		textVie = GetComponentInChildren<UnityEngine.UI.Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		textVie.text = juliette.getVieCourante ()+"";
	}
}
