using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socle : ObjetEnvironnemental {




	public override void Activation(){
		this.gameObject.transform.GetChild (0).gameObject.SetActive(true);
	}
}
