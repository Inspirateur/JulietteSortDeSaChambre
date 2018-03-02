using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQL_E_TourellePrincesse : IA_Etat {

	public int degats;
	public float forceRecule;
	public AudioClip sonAttaque;
	public GameObject projectile;
	public GameObject positionDepartProjectile;

	private Transform cible;
	private float timerFinAttaque;
	private float timerChargement;
	private bool projectileDejaCree;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		this.cible = GameObject.FindGameObjectWithTag("Player").transform;
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat()
	{
		Transform t = this.transform;
	}

	public override void faireEtat()
	{
		agent.seTournerVersPosition(cible.position);
		if(Time.time >= this.timerChargement && !projectileDejaCree){
			this.lancerProjectile();
		}
		else if (Time.time >= this.timerFinAttaque){
			this.restartAttaque();
		}
	}

	public override void sortirEtat()
	{
		
	}

	private void restartAttaque(){
		// setAnimation (SQL_Animations.ATTAQUER);
		projectileDejaCree = false;
		timerChargement = Time.time + 1.0f;
		timerFinAttaque = timerChargement + 1.0f;
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
