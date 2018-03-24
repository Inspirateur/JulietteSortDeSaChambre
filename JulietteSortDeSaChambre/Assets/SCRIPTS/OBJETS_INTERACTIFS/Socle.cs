using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socle : ObjetEnvironnemental {

	public int BarreNumberToOpen;
	public GameObject SocleEvenement;
	public bool utilise;
	private PrincesseObjetProgression juliette;
	public EnumObjetProgression obj;

	void Start () {
		juliette= GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseObjetProgression>();
		utilise = false;
	}
	public override void Activation(){
		if (!utilise) {
			if(isActivable()){
				this.gameObject.transform.GetChild (0).gameObject.SetActive (true);
				SocleEvenement.GetComponent<barreMetalManager> ().StartOpenBarre(BarreNumberToOpen);
				utilise = true;
				juliette.removeItem (obj);
			}
		}
	}


	public bool isActivable(){
		return juliette.listObjet.ContainsKey(obj);
	}

	override
	public EnumIconeInterraction getIconeInteraction(){

		if (utilise) {
			return EnumIconeInterraction.icone_null;
		}
		if(isActivable()){
			return EnumIconeInterraction.icone_default;
		}else{
			return EnumIconeInterraction.icone_non_default;
		}


	}


}
