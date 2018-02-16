using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Dormir : IA_Etat {
	//Entre 0 et 1
	public float attentionSommeil;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		setAnimation(TRO_Animations.DORMIR);
		perception.estAveugle = true;
	}

	// Update is called once per frame
	public override void faireEtat () {
		if(perception.aRepere(princesse, attentionSommeil)){
			changerEtat(this.GetComponent<TRO_E_Poursuivre>());
		}
	}

	public override void sortirEtat() {
		perception.estAveugle = false;
	}

	public override void subirDegats(int valeurDegats, Vector3 hitPoint) {
		agent.getMobVie().blesserSansEtat (valeurDegats, hitPoint);
	}
}
