using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematique_Traveling : Cinematique {

	[Header("Paramètres spécifique de la cinématique")]

	public float vitesseTraveling;
	public bool commencerDirectementAuPremierPoint;
    public bool arretAChaquePOV;
    public float dureeArretAChaquePOV;
    public bool arretAuPointDeDemarrage;
    public float distanceApprochePourChangementPOV;
    public bool finirEnTravelingVersJuliette;

	private Vector3 velocity = Vector3.zero;
	private Vector3 velocityLookAt = Vector3.zero;
    private float smooth;
    private float timer;
    private bool enArretPOV;
    private Transform destination;
    private Vector3 forwardInitial;
    private float distanceInitiale;
    private bool enTravelingFinalVersJuliette;

    public override void entrer()
    {
        velocity = Vector3.zero;
        velocityLookAt = Vector3.zero;
        this.enTravelingFinalVersJuliette = false;

        if(this.commencerDirectementAuPremierPoint){
            this.placer(this.listeCinematiquePointOfView[0]);
            if(!this.arretAuPointDeDemarrage){
                this.numPOVActuel++;
            }
        }

        this.commencerDeplacementVers(this.listeCinematiquePointOfView[this.numPOVActuel].transform);

        this.smooth = 0.15f;
        this.timer = 0.0f;
        this.enArretPOV = false;
    }

    public override void mettreAJour()
    {
        if(this.arretAChaquePOV){

            // on est arrivé au POV
            if(this.cameraArriverAuPOVDeDestination()){
                
                // si un temps d'arret est prévu à chaque POV
                if(this.dureeArretAChaquePOV > 0.0f){

                    if(!enArretPOV && !enTravelingFinalVersJuliette){

                        enArretPOV = true;

                        timer = Time.time + dureeArretAChaquePOV;
                    }
                    else if(Time.time >= timer || enTravelingFinalVersJuliette){

                        enArretPOV = false;

                        this.passerAuPOVSuivant();
                    }
                }
                else{
                    this.passerAuPOVSuivant();
                }
            }
        }
        else {
            if( this.numPOVActuel < this.listeCinematiquePointOfView.Length - 1 &&
                this.calculerDistanceEntrePositionActuelleCameraEtPOVDesire() <= distanceApprochePourChangementPOV
                ||
                this.numPOVActuel == this.listeCinematiquePointOfView.Length - 1 &&
                this.cameraArriverAuPOVDeDestination()
                ||
                this.enTravelingFinalVersJuliette &&
                this.cameraArriverAuPOVDeDestination()){

                this.passerAuPOVSuivant();
            }
        }

        if(this.active){
            Camera.main.transform.position = Vector3.SmoothDamp(Camera.main.transform.position, this.destination.position, ref velocity, this.smooth, this.vitesseTraveling);

            float ratio = this.calculerDistanceEntrePositionActuelleCameraEtPOVDesire() / this.distanceInitiale;
            Camera.main.transform.forward = this.forwardInitial * ratio + this.destination.forward * (1.0f - ratio);
        }
    }

    public override void sortir()
    {
        this.replacerPositionAvantLancementCinematique();
    }

    private float calculerDistanceEntrePositionActuelleCameraEtPOVDesire(){
        return (this.destination.position - Camera.main.transform.position).magnitude;
    }

    private bool cameraArriverAuPOVDeDestination(){
        return this.calculerDistanceEntrePositionActuelleCameraEtPOVDesire() <= 0.01f;
    }

    private void commencerDeplacementVers(Transform destination){
        this.forwardInitial = Camera.main.transform.forward;
        this.distanceInitiale = (destination.position - Camera.main.transform.position).magnitude;
        this.destination = destination;
    }

    private void passerAuPOVSuivant(){

        this.numPOVActuel++;
        
        if(this.enTravelingFinalVersJuliette){
            this.terminer();
        }
        else if(this.numPOVActuel == this.listeCinematiquePointOfView.Length){

            if(this.finirEnTravelingVersJuliette){
                this.forwardInitial = Camera.main.transform.forward;
                this.distanceInitiale = (this.initialPositionCam - Camera.main.transform.position).magnitude;
                this.destination.position = this.initialPositionCam;
                this.destination.forward = this.initialForwardCam;
                this.enTravelingFinalVersJuliette = true;
            }
            else{
                this.terminer();
            }
        }
        else {
            
            this.commencerDeplacementVers(this.listeCinematiquePointOfView[this.numPOVActuel].transform);
        }
    }
}
