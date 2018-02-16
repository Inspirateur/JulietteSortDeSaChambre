using System;
using UnityEngine;

public class TRO_MobVie : IA_MobVie {
	//Entre 0 et 1
	public float seuilAnimation;
	private int ancienneVie;

	void Start () {
		base.init ();
		ancienneVie = vieCourante;
	}

	protected override bool conditionEtatBlesse() {
		if (ancienneVie >= vieMax * seuilAnimation && vieCourante < vieMax * seuilAnimation) {
			ancienneVie = vieCourante;
			return true;
		} else {
			ancienneVie = vieCourante;
			return false;
		}
	}
}


