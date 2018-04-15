using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Mourir : IA_Etat {

	public float delaiAvantDisparition;
	public float delaiAvantEvent;
	public AudioClip sonMort;
	public Transform poofEffect;

	private float actualDelai;
	private float actualDelaiEvent;
	private bool sonJoue;

	// Use this for initialization
	void Start() {
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat() {
		anim.Play(TRO_Animations.MOURIR);
		rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		actualDelai = Time.time + delaiAvantDisparition;
		actualDelaiEvent = Time.time + delaiAvantEvent;
		sonJoue = false;
	}

	public override void faireEtat() {
		if (!agent.getSoundEntity().isPlaying() && !sonJoue){
			agent.getSoundEntity().playOneShot(sonMort,1.0f);
			sonJoue = true;
		}
		/*if(Time.time >= actualDelaiEvent){
			actualDelaiEvent += delaiAvantDisparition;
			EventManager em = GetComponent<EventManager>();
			if(em != null){
				em.activation();
			}
		}*/
		if (Time.time >= actualDelai){
			Instantiate (poofEffect, this.transform.position, poofEffect.transform.rotation);
			EventManager em = GetComponent<EventManager>();
			if(em != null){
				em.activation();
			}
			this.gameObject.SetActive (false);
		}
	}

	public override void sortirEtat() {

	}
}
