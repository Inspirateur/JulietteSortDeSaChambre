using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincesseObjetProgression : MonoBehaviour {

	public Dictionary<EnumObjetProgression,int> listObjet;

	// Use this for initialization
	void Start () {
		listObjet = new Dictionary<EnumObjetProgression,int>();
	}

	// Update is called once per frame
	void Update () {
	}
	//modif pour sourcetree
	public void addItem(EnumObjetProgression objetProgression){
		if (listObjet.ContainsKey (objetProgression)) {
			listObjet [objetProgression]++;
		} else {
			listObjet.Add (objetProgression,1);
		}

	}

	public void removeItem(EnumObjetProgression objetProgression){
		
		if (listObjet [objetProgression] == 1) {
			listObjet.Remove (objetProgression);
		} else {
			listObjet [objetProgression]--;
		}
	}
}

public enum EnumObjetProgression
{
	caisse,
	key,
}