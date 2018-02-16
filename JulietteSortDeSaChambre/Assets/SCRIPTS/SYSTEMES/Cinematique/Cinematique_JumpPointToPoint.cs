using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematique_JumpPointToPoint : Cinematique {

	[Header("Paramètres spécifique de la cinématique")]

	public float dureeArretAChaquePoint;

    private float timer;

    public override void entrer()
    {
        this.placer(this.listeCinematiquePointOfView[0]);
        timer = Time.time + this.dureeArretAChaquePoint;
    }

    public override void mettreAJour()
    {
        if(Time.time >= timer){

            this.numPOVActuel++;

            if(this.numPOVActuel == this.listeCinematiquePointOfView.Length){

                this.terminer();
            }
            else{

                this.placer(this.listeCinematiquePointOfView[this.numPOVActuel]);
                timer = Time.time + this.dureeArretAChaquePoint;
            }
        }
    }

    public override void sortir()
    {
        this.replacerPositionAvantLancementCinematique();
    }
}
