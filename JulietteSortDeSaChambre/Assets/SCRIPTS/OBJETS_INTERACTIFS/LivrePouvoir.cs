using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivrePouvoir : ObjetEnvironnemental {

	// private bool active;
	public EnumPouvoir pouvoir;

	// Use this for initialization
	void Start () {
		// active = true;
	}

	// Update is called once per frame
	void Update () {

	}

	public override void Activation(){
		// if (active) {
			GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<PrincessePouvoirGlace> (true).isUnlocked=true;
			GameObject.FindGameObjectWithTag ("AffichagePouvoir").GetComponentInChildren<AffichagePouvoir> (true).setVisible(pouvoir);
			gameObject.SetActive (false);
			// active = false;
		// }
	}

	public override EnumIconeInterraction getIconeInteraction(){
		// if (active) {
			return EnumIconeInterraction.icone_default;
		// } else {
		// 	return EnumIconeInterraction.icone_null;
		// }
	}


}
