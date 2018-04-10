using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivrePouvoir : ObjetEnvironnemental {

	// private bool active;
	public EnumPouvoir pouvoir;

	private SoundManager sm;

	// Use this for initialization
	void Start () {
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
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
			sm.playOneShot(BesoinItemPourActivation);
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
