using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQL_E_Attaquer : IA_Etat {

	public int degats;
	public float forceRecule;
	public AudioClip sonCoteArrachee;
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
	void Start() {
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		this.cible = GameObject.FindGameObjectWithTag("Player").transform;
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat() {
		setAnimation (SQL_Animations.ATTAQUER);
		projectileDejaCree = false;
		timerChargement = Time.time + 2.0f;
		timerFinAttaque = timerChargement + 1.0f;
		timerApparitionCote = Time.time + 0.8f;
		modelCoteActif = false;
	}

	public override void faireEtat() {
		agent.seTournerVersPosition(cible.position);
		if(Time.time >= this.timerApparitionCote && !modelCoteActif){
			agent.getSoundEntity().playOneShot(sonCoteArrachee, 0.3f);
			modelCoteMain.SetActive(true);
			modelCoteActif = true;
		} else if(Time.time >= this.timerChargement && !projectileDejaCree){
			this.lancerProjectile();
			setAnimation (SQL_Animations.GARDER);
			modelCoteMain.SetActive(false);
		}
		else if (Time.time >= this.timerFinAttaque){
			changerEtat(GetComponent<SQL_E_Combattre>());
		}
	}

	public override void sortirEtat()
	{
		modelCoteMain.SetActive(false);
	}

	private void lancerProjectile(){

		projectileDejaCree = true;

		Projectile projectile = Instantiate(this.projectile, positionDepartProjectile.transform.position, Quaternion.identity).GetComponent<Projectile>();

		projectile.setDestination(cible.position + cible.up * 1.4f);
		projectile.degats = this.degats;
		projectile.recul = this.forceRecule;
	}
}
