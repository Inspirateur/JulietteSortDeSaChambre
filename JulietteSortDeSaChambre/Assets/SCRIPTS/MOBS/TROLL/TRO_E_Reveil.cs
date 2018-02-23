using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Reveil : IA_Etat {
	private float timer;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		setAnimation(TRO_Animations.REVEIL);
		timer = Time.time + 1.39f;
	}

	// Update is called once per frame
	public override void faireEtat () {
		if (Time.time >= timer) {
			changerEtat (this.GetComponent<TRO_E_Garder> ());
		}
	}

	public override void sortirEtat() {
	}
}
