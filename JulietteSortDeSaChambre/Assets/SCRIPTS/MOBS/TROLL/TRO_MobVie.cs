using System;
using UnityEngine;

public class TRO_MobVie : IA_MobVie {
	void Start () {
		base.init ();
	}

	protected override void apresPriseDegat(int degatSubit){
		agent.immuniteDouleur = vieCourante >= vieMax / 2 || vieCourante + degatSubit < vieMax / 2;
	}

}


