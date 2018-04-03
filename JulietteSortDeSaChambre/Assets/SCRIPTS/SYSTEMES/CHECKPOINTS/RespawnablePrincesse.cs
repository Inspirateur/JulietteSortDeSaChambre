using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnablePrincesse : RespawnableEntity {

	private PrincesseVie princesseVie;
    private PrincesseArme princesseArme;
    private PrincesseObjetProgression princesseObjetProgression;
    private EnumArmes armeActive;
    private camera cam;
    private Dictionary<EnumObjetProgression,int> listObjet;

	void Awake() {
        princesseVie = GetComponent<PrincesseVie>();
        princesseArme = GetComponent<PrincesseArme>();
        princesseObjetProgression = GetComponent<PrincesseObjetProgression>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();
    }

    public override void setInitialState()
    {
        Debug.Log(gameObject.ToString() + " : setInitialState");
        this.armeActive = this.princesseArme.armeActive;
        this.listObjet = new Dictionary<EnumObjetProgression, int>(this.princesseObjetProgression.listObjet);
    }

    public override void onRespawn()
    {
        Debug.Log(gameObject.ToString() + " : OnRespawn");
		princesseVie.fullSoigner();
        this.princesseArme.SetArmeActive(this.armeActive);
        this.princesseObjetProgression.listObjet = new Dictionary<EnumObjetProgression, int>(this.listObjet);
        AffichageInventaire.getInstance().recreateDicoInventaireFromPrincesse();
        cam.centrerCamera();
    }
}
