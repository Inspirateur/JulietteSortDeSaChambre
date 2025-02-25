﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObjet : MonoBehaviour {

	private GameObject juliette;


	public ObjetInteractifs objet;
	public float distanceActivation;
	public float demiAngleActivationFrontal;
    public float minDistanceSurbrillance;
    public bool EnableSurbrillance;

    private affichageInterraction hud_refractor;

	// Use this for initialization
	void Start () {
		juliette = GameObject.FindGameObjectWithTag ("Player");
		hud_refractor = GameObject.FindGameObjectWithTag ("HUDImageInteraction").GetComponent<affichageInterraction>();
	}


	
	// Update is called once per frame
	void Update () {
		Vector3 distance_princesse = this.transform.position - juliette.transform.position;

		bool action = InputManager.GetButtonDown("Interagir");

        if (EnableSurbrillance)
        {
            if (distance_princesse.magnitude < minDistanceSurbrillance)
            {
                objet.AddSurbrillance();
            }
            else
            {
                objet.removeSurbrillance();
            }
        }

		if (objet as Item==null) 
		{
			if (distance_princesse.magnitude < distanceActivation) {
				// dans la distance d'activation de l'objet

				float angle = Vector3.Angle (juliette.transform.forward, distance_princesse.normalized);

				if (angle <= demiAngleActivationFrontal) {
					hud_refractor.activeAffichageInteractionObjet (objet);
					//image_detection.enabled = true;
					if (action) {
						hud_refractor.desactiveAffichageInteractionObjet (objet);
						objet.Activation ();
					}

				} else {
					hud_refractor.desactiveAffichageInteractionObjet (objet);
					//image_detection.enabled = false;
				}
			} else {
				hud_refractor.desactiveAffichageInteractionObjet (objet);
				//image_detection.enabled = false;
			}
		}

        

	}

	void OnTriggerEnter(Collider collision){
		if (collision.tag == "Player" && (objet as Item!=null)){
			objet.Activation ();
		}


	}
}
