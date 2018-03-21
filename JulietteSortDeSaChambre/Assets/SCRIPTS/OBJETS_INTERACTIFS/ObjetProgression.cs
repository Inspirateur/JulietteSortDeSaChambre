using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetProgression : ObjetInteractifs {

	private PrincesseObjetProgression juliette;
	public EnumObjetProgression objetProgression;
	private AffichageObjetRamasser affichageObjetRamasser;
	private AffichageInventaire affichageobjetActuel;

	public AudioClip RamasseObjet;
	public bool evenenement;

	// Use this for initialization
	void Start () {
		juliette= GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseObjetProgression>();
		affichageObjetRamasser = GameObject.FindGameObjectWithTag ("HUDAffichageObjetRamasser").GetComponent<AffichageObjetRamasser> ();
		affichageobjetActuel =GameObject.FindGameObjectWithTag ("HUDAffichageInventaire").GetComponent<AffichageInventaire> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation(){
		sm.playOneShot(RamasseObjet);
		affichageObjetRamasser.activeObjet (this);
		juliette.addItem (this.objetProgression);
		affichageobjetActuel.objetRamasse (this.objetProgression);
		if (evenenement) {
			GetComponent<EventManager> ().activation ();
		}
		supprimerObjet ();
	}
}
public enum EnumObjetProgression 
{
	caisse,
	key,
	cristal_bleue,
	cristal_rouge,
	cristal_vert,
}