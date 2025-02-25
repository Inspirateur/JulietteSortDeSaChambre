﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQL_E_EtreBlesse : IA_Etat {

	public float forceReculeVertical;
	public float forceReculeHorizontal;
	public AudioClip[] sonsDegat;
	public IA_Etat etatApresBlessure;

	private float facteurRecule;

	private float timer;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		nav.enabled = false;
		facteurRecule = princesseArme.getFacteurReculeArmeActuelle();
		anim.Play(SQL_Animations.ETRE_BLESSE);
		agent.getSoundEntity().stop();
		agent.getSoundEntity().playOneShot(sonsDegat[Random.Range(0, sonsDegat.Length)],1.0f);

		Vector3 directionRecule = (this.transform.position - princesse.transform.position).normalized;

		rb.velocity = Vector3.zero;
		rb.AddForce ((directionRecule * (forceReculeHorizontal * facteurRecule)) + (this.transform.up * (forceReculeVertical * facteurRecule)));
		timer = Time.time + 1.0f;
	}

	public override void faireEtat()
	{
		if (Time.time > timer) {
			changerEtat (etatApresBlessure);
		}
	}

	public override void sortirEtat()
	{

	}
}
