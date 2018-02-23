using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Dormir : IA_Etat {
	//Entre 0 et 1
	public float attentionSommeil;
	public float reveilMin;
	public float reveilMax;
	private float reveil;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		setAnimation(TRO_Animations.ENDORMIR);
		perception.estAveugle = true;
		reveil = Time.time + 7.3f + reveilMin + (reveilMax - reveilMin) * Random.value;
	}

	// Update is called once per frame
	public override void faireEtat () {
		if (perception.aRepere (princesse, attentionSommeil) || Time.time >= reveil) {
			Debug.Log ("JEJEJ");
			changerEtat (this.GetComponent<TRO_E_Reveil> ());
		}
	}

	public override void sortirEtat() {
		perception.estAveugle = false;
	}

}
