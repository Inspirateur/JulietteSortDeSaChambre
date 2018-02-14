using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public bool ami;
	public float vitesse;
	public int degats;
	public float dureeDeVie;

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

	public void setDestination(Vector3 positionDestination){
		destination = positionDestination - this.transform.position;
		destination.Normalize();
	}
}
