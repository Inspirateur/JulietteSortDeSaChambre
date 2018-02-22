using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levier : ObjetEnvironnemental {

	private  Animator anim;
	private bool active;

	public List<Evenement> listEvenement;



	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		active = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Activation(){
		anim.SetBool("isUp", true);
		Debug.Log ("ok");
		foreach (Evenement e in listEvenement) {
			e.activation ();
		}
	}
}
