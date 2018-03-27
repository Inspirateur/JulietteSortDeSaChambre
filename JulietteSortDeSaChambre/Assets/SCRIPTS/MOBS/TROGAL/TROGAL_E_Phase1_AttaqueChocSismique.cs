using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_E_Phase1_AttaqueChocSismique : IA_Etat {

	public int degatsDirect;
	public int degatsOndeDeChoc;
	public float forceRecule;
	public float rayonOndeDeChoc;
	public Transform pointImpactMassueSurLeSol;
	public Transform effetOndeDeChoc;
	public AudioClip sonAttaque;

	private bool degatsAttaqueEffectues;
	private IA_TriggerArme colliderArme;
	private float timerFinAttaque;
	private float timerDebutDegatsMassue;
	private float timerFinDegatsMassue;
	private float timerDebutOndeDeChoc;
	private float timerFinOndeDeChoc;
	private bool ondeDeChocLancee;
	private bool degatsOndeDeChocEffectues;
	private Vector3 centreOndeDeChoc;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		colliderArme = GetComponent<IA_TriggerArme> ();

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		// agent.getSoundEntity().playOneShot(sonAttaque, 1.0f);
		degatsAttaqueEffectues = false;
		degatsOndeDeChocEffectues = false;
		ondeDeChocLancee = false;
		// setAnimation (GOB_Animations.ATTAQUER_HORIZONTALEMENT);
		timerDebutDegatsMassue = Time.time + 1.5f;
		timerDebutOndeDeChoc = timerDebutDegatsMassue + 0.25f;
		timerFinOndeDeChoc = timerDebutOndeDeChoc + 0.5f;
		timerFinDegatsMassue = timerFinOndeDeChoc + 0.25f;
		timerFinAttaque = timerFinDegatsMassue + 1.0f;
	}

	public override void faireEtat()
	{
		if(timerDebutOndeDeChoc < Time.time && !ondeDeChocLancee){
			centreOndeDeChoc = new Vector3(pointImpactMassueSurLeSol.position.x, pointImpactMassueSurLeSol.position.y, pointImpactMassueSurLeSol.position.z);
			ondeDeChocLancee = true;
			Instantiate (effetOndeDeChoc, centreOndeDeChoc, effetOndeDeChoc.transform.rotation);
		}

		if(timerFinOndeDeChoc > Time.time
		&& (princesse.transform.position - centreOndeDeChoc).magnitude <= rayonOndeDeChoc
		&& !degatsOndeDeChocEffectues
		&& ondeDeChocLancee){

			princesseVie.blesser (degatsOndeDeChoc, this.gameObject, forceRecule);
			degatsOndeDeChocEffectues = true;
		}

		if (timerDebutDegatsMassue < Time.time
		&& timerFinDegatsMassue > Time.time
		&& !degatsAttaqueEffectues
		&& colliderArme.IsPrincesseTouchee ()) {

			princesseVie.blesser (degatsDirect, this.gameObject, forceRecule);
			degatsAttaqueEffectues = true;
		}

		if (timerFinAttaque < Time.time) { // l'attaque est finie
			changerEtat(this.GetComponent<TROGAL_E_Phase1_Combattre>());
		}
	}

	public override void sortirEtat()
	{
		
	}
}
