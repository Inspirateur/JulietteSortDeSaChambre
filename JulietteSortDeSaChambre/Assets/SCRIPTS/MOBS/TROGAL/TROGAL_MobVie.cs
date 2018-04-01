using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TROGAL_MobVie : IA_MobVie {

	void Start () {
		base.init ();
	}

	protected override void apresPriseDegat(int degatSubit){
		agent.immuniteDouleur = vieCourante >= vieMax / 2 || vieCourante + degatSubit < vieMax / 2;
	}
}
