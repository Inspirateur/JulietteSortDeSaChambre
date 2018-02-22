using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]  
public class EvenementItem {

	public string name = "Nouvelle Evenement";
	[HideInInspector]
	public GameObject objet ;
	[HideInInspector]
	public List<string> listMethod = new List<string>();
	[HideInInspector]
	public int t=-1;
	[HideInInspector]
	public List<object> listParam = new List<object>();

}
