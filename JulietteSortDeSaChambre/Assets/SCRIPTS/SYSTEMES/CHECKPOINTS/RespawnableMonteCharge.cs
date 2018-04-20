using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnableMonteCharge : RespawnableEntity {

    private MovingObject plateforme;
    private MonteChargeMouvante monteChargeMouvante;

    private int Etat;
    private float TimeWaitForCheck;
    private float OriginalResetTime;
    private bool isStop;
    private Vector3 NouvellePosition;
    private bool DeplacementRetour;
    private int PostionToCheck;
    private bool actif;

	public override void onAwake() {
        plateforme = GetComponent<MovingObject>();
        monteChargeMouvante = GetComponent<MonteChargeMouvante>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        Etat = plateforme.Etat;
        TimeWaitForCheck = plateforme.TimeWaitForCheck;
        OriginalResetTime = plateforme.OriginalResetTime;
        isStop = plateforme.isStop;
        NouvellePosition = plateforme.NouvellePosition;
        DeplacementRetour = plateforme.DeplacementRetour;
        PostionToCheck = plateforme.PostionToCheck;
        actif = monteChargeMouvante.actif;
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
        plateforme.CancelInvoke();
        plateforme.Etat = Etat;
        plateforme.TimeWaitForCheck = TimeWaitForCheck;
        plateforme.OriginalResetTime = OriginalResetTime;
        plateforme.isStop = isStop;
        plateforme.NouvellePosition = NouvellePosition;
        plateforme.DeplacementRetour = DeplacementRetour;
        plateforme.PostionToCheck = PostionToCheck;
        if(actif != monteChargeMouvante.actif){
            monteChargeMouvante.stopSound();
        }
    }
}
