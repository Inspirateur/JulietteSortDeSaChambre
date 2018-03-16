using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiegeArretEvenement : Evenement {

	override
	public void activation(){
		GetComponent<Spikes> ().StopSpikes ();
	}

	public void test(int t){
		Debug.Log (t);
	}
}
