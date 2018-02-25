using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socle : ObjetEnvironnemental {

	public int BarreNumberToOpen;
	public GameObject SocleEvenement;
	private bool active;
	public bool isReactivable;

	void Start () {
		active = true;
	}
	public override void Activation(){
		if (active) {
			this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
			SocleEvenement.GetComponent<barreMetalManager> ().DownBarre (BarreNumberToOpen);
			active = false;
		}
	}

	override
	public EnumIconeInterraction getIconeInteraction(){

		if (!active && !isReactivable) {
			return EnumIconeInterraction.icone_null;
		}

		return EnumIconeInterraction.icone_default;
	}
}
