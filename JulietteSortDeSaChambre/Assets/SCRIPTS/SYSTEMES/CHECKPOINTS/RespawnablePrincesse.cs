using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnablePrincesse : RespawnableEntity {

	private PrincesseVie princesseVie;
    private PrincesseArme princesseArme;
    private PrincesseDeplacement princesseDeplacement;
    private PrincessePouvoirGlace princessePouvoirGlace;
    private PrincesseObjetProgression princesseObjetProgression;
    private EnumArmes armeActive;
    private camera cam;
    private Dictionary<EnumObjetProgression,int> listObjet;
    private bool unlockPouvoirGlace;

	public override void onAwake() {
        princesseVie = GetComponent<PrincesseVie>();
        princesseArme = GetComponent<PrincesseArme>();
        princesseDeplacement = GetComponent<PrincesseDeplacement>();
        princessePouvoirGlace = GetComponentInChildren<PrincessePouvoirGlace> (true);
        princesseObjetProgression = GetComponent<PrincesseObjetProgression>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();
    }

    public override void setInitialState()
    {
        // Debug.Log(gameObject.ToString() + " : setInitialState");
        this.armeActive = this.princesseArme.armeActive;
        this.listObjet = new Dictionary<EnumObjetProgression, int>(this.princesseObjetProgression.listObjet);
        this.unlockPouvoirGlace = princessePouvoirGlace.isUnlocked;
    }

    public override void onRespawn()
    {
        // Debug.Log(gameObject.ToString() + " : OnRespawn");
		princesseVie.fullSoigner();
        this.princesseArme.SetArmeActive(this.armeActive);
        this.princesseObjetProgression.listObjet = new Dictionary<EnumObjetProgression, int>(this.listObjet);
        AffichageInventaire.getInstance().recreateDicoInventaireFromPrincesse();
        cam.centrerCamera();
        princesseDeplacement.canMove = true;
        princesseDeplacement.gererAnim("IsIdle");

        if(!this.unlockPouvoirGlace){
            princessePouvoirGlace.isUnlocked = this.unlockPouvoirGlace;
            GameObject.FindGameObjectWithTag ("AffichagePouvoir").GetComponentInChildren<AffichagePouvoir> (true).setHidden(EnumPouvoir.pouvoirGlace);
        }
    }
}
