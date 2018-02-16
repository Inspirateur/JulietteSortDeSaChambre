using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : ObjetInteractifs {

	public EnumArmes typeArme;
	public EnumIconeInterraction iconeInterraction;
	private AffichageObjetRamasser affichageObjetRamasser;
	private PrincesseArme juliette;


	// Use this for initialization
	void Start () {
		affichageObjetRamasser = GameObject.FindGameObjectWithTag ("HUDAffichageObjetRamasser").GetComponent<AffichageObjetRamasser> ();
		juliette = GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseArme> ();
		soundGenerator = GetComponent<SoundEntity>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation(){

		if (!juliette.listArmeTenu.Contains (typeArme)) {
			affichageObjetRamasser.activeObjet (this);
			//juliette.listArmeTenu.Add (typeArme);
			GameControl.control.listArmeTenu.Add (typeArme);
		}	
		lanceSonRamasseObjet();
		juliette.SetArmeActive (typeArme, this.gameObject);
	}


	public override EnumIconeInterraction getIconeInteraction(){
		return iconeInterraction;
	}
}
