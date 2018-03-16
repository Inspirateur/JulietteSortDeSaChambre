using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQL_E_TourellePrincesse : IA_Etat {

	public int degats;
	public float forceRecule;
	public AudioClip sonAttaque;
	public GameObject projectile;
	public GameObject positionDepartProjectile;
	public GameObject modelCoteMain;

	private Transform cible;
	private float timerFinAttaque;
	private float timerChargement;
	private float timerApparitionCote;
	private bool projectileDejaCree;
	private bool modelCoteActif;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		this.cible = GameObject.FindGameObjectWithTag("Player").transform;
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		this.restartAttaque();
	}

	public override void faireEtat()
	{
		agent.seTournerVersPosition(cible.position);
		if(Time.time >= this.timerApparitionCote && !modelCoteActif){
			modelCoteMain.SetActive(true);
			modelCoteActif = true;
		} else if(Time.time >= this.timerChargement && !projectileDejaCree){
			this.lancerProjectile();
			setAnimation (SQL_Animations.GARDER);
			modelCoteMain.SetActive(false);
		}
		else if (Time.time >= this.timerFinAttaque){
			this.restartAttaque();
		}
	}

	public override void sortirEtat()
	{
		modelCoteMain.SetActive(false);
	}

	private void restartAttaque(){
		setAnimation (SQL_Animations.ATTAQUER);
		projectileDejaCree = false;
		timerChargement = Time.time + 2.0f;
		timerFinAttaque = timerChargement + 1.0f;
		timerApparitionCote = Time.time + 1.0f;
		modelCoteActif = false;
	}

	private void lancerProjectile(){

		agent.getSoundEntity().playOneShot(sonAttaque, 1.0f);

		projectileDejaCree = true;

		Projectile projectile = Instantiate(this.projectile, positionDepartProjectile.transform.position, Quaternion.identity).GetComponent<Projectile>();

		projectile.setDestination(cible.position + cible.up * 1.4f);
		projectile.degats = this.degats;
		projectile.recul = this.forceRecule;
	}
}
