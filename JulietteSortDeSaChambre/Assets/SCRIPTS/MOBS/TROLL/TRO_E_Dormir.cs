using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Dormir : IA_Etat {
	//Entre 0 et 1
	public float attentionSommeil;
	public float reveilMin;
	public float reveilMax;
	private float reveil;
	private float tpsAnim;

	// Use this for initialization
	void Start(){
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
	}

	public override void entrerEtat(){
		setAnimation(TRO_Animations.ENDORMIR);
		tpsAnim = Time.time + 7.3f;
		reveil = tpsAnim + reveilMin + (reveilMax - reveilMin) * Random.value;
	}

	// Update is called once per frame
	public override void faireEtat () {
		if (Time.time >= tpsAnim) {
			perception.estAveugle = true;
		}
		if (Time.time < tpsAnim && perception.aRepere (princesse, attentionSommeil)) {
			changerEtat (this.GetComponent<TRO_E_Poursuivre> ());
		} else if (perception.aRepere (princesse, attentionSommeil) || Time.time >= reveil) {
			changerEtat (this.GetComponent<TRO_E_Reveil> ());
		}
	}

	public override void sortirEtat() {
		perception.estAveugle = false;
	}

}
