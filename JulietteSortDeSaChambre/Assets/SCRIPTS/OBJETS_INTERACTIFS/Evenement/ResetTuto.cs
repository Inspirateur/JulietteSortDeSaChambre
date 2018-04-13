using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTuto : Evenement {

	public void resetBanniere(){
		GetComponent<Tuto> ().active = true;
	}
}
