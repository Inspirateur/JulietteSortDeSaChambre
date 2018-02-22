using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public class EvenementItemList : ScriptableObject {


	public List<EvenementItem> listEvenement;

	public void activerEvenement(){
		foreach (EvenementItem e in listEvenement) {
			Type t = e.objet.GetComponent<ObjetEvenementiel> ().GetType ();
			MethodBase m = t.GetMethod (e.listMethod [e.t]);
			m.Invoke (e.objet.GetComponent<ObjetEvenementiel> (), e.listParam.ToArray());
		}
	}
}
