using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageInventaire : MonoBehaviour {

	[HideInInspector]
	public List<ObjetInventaire> listObjet;
	public GameObject objetInventaire;
	public GameObject sac;

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void objetRamasse(EnumObjetProgression enu){

		foreach(ObjetInventaire objet in listObjet){
			if(objet.objet.Equals(enu)){
				createObjet (objet);
			}
		}
	}

	private void createObjet(ObjetInventaire objet){
		Debug.Log (objet.image);
		GameObject temp= Instantiate(objetInventaire);
		temp.transform.parent = gameObject.transform.GetChild (0);
		temp.SetActive (true);
		temp.name = string.Concat (temp.name, "(", objet.objet.ToString(), ")");
		temp.GetComponent<RectTransform> ().localScale = objet.scale;
		temp.GetComponent<RectTransform> ().sizeDelta = objet.size;
		temp.GetComponent<UnityEngine.UI.Image> ().sprite = objet.image;

	}
}
