﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuverturePorte : Evenement {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

/*	public void StopMusic() {
		GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>().pauseAllMusic();
	}
*/
	public void ouvertureGrossePorte() {
		Porte p = this.GetComponent<Porte>();
		p.setOpenEndDoorCinematique(true);
	}

	override
	public void activation(){
		Porte p = this.GetComponent<Porte> ();
		p.OuverturePorte ();
	}

	public void desactivatePorte(){
		Porte p = this.GetComponent<Porte> ();
		p.isDecorative = true;
	}

	public void fermePorte(){
		Porte p = this.GetComponent<Porte> ();
		p.fermePorte ();
	}

}
