using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public bool ami;
	public float vitesse;
	public int degats;
	public float recul;
	public float dureeDeVie;
	public GameObject effetCollision;

	private float timerVie;
	private Vector3 destination;

	// Use this for initialization
	void Start () {
		this.timerVie = Time.time + this.dureeDeVie;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(this.destination * this.vitesse * Time.deltaTime);
		if(Time.time >= this.timerVie){
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		
		if(this.decorTouche(other)){
			this.detruireCollision();
		}
		else if(ami){
			if (other.tag.Equals ("Mob")){
				IA_Agent mobTouche = other.gameObject.GetComponent<IA_Agent> ();
				if(mobTouche.estEnVie()){
					Vector3 hitPoint = other.ClosestPoint (this.transform.position);
					mobTouche.subirDegats (degats, hitPoint);
					this.detruireCollision();
				}
			}
		}
		else {
			if (other.tag.Equals ("Player")){
				GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseVie>().blesser (degats, this.gameObject, recul);
				this.detruireCollision();
			}
		}
	}

	public void setDestination(Vector3 positionDestination){
		destination = positionDestination - this.transform.position;
		destination.Normalize();
	}

	private bool decorTouche(Collider other){
		return other.tag.Equals ("sol") || other.tag.Equals ("wall") || other.tag.Equals ("Decor");
	}

	private void detruireCollision(){
		Instantiate(this.effetCollision, this.transform.position, this.transform.rotation);
		Destroy(this.gameObject);
	}
}
