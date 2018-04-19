using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevierEvenement : Evenement {

	public void bloquerLevier(bool bloque){
		GetComponent<Levier> ().setBloque (bloque);
	}
}
