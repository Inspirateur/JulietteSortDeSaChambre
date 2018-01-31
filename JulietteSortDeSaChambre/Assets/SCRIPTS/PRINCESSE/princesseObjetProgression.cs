using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincesseObjetProgression : MonoBehaviour {

	public List<EnumObjetProgression> listObjet;

	// Use this for initialization
	void Start () {
		listObjet = new List<EnumObjetProgression> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void addItem(EnumObjetProgression objetProgression){
		listObjet.Add (objetProgression);
	}

	public void removeItem(EnumObjetProgression objetProgression){
		listObjet.Remove (objetProgression);
	}
}

public enum EnumObjetProgression
{
	caisse,
}