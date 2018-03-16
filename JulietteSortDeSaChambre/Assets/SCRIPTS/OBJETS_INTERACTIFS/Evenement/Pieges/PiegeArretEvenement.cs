using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiegeArretEvenement : Evenement {

	override
	public void activation(){
		GetComponent<ManagerSpikes> ().StopAllSpikes();
	}

    override
    public void desactivation()
    {
        GetComponent<ManagerSpikes>().StartAllSpikes();
    }

}
