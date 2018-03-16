using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiegeArretEvenement : Evenement {

	public override void activation(){
		GetComponent<Spikes> ().StopSpikes ();
	}
}
