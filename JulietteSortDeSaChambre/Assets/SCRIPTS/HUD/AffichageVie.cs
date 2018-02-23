using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageVie : MonoBehaviour {

	private PrincesseVie juliette;
//	private UnityEngine.UI.Text textVie;

	[HideInInspector]
	public List<Vie> listVie;

	private List<GameObject> listCoeur= new List<GameObject> ();


	// Use this for initialization
	void Start () {
		juliette = GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseVie> ();
		//textVie = GetComponentInChildren<UnityEngine.UI.Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		//textVie.text = juliette.getVieCourante ()+"/"+juliette.vie_max;
	}

	public void setAffichageVie(int vie_courante,int vie_max){
	//	Debug.Log (vie_courante + "/" + vie_max);
		int coeurCompletTotal=vie_max/4;
		int particoeurSupTotal = vie_max % 4 ;
	

		int coeurComplet=vie_courante/4;
		int partiCoeurSup = vie_courante % 4 ;

	//	Debug.Log ("coeurComplet : "+coeurComplet);
	//	Debug.Log ("partiCoeurSup :"+partiCoeurSup);

//		Debug.Log (particoeurSupTotal);
		int nbCoeur=coeurCompletTotal;
		if (particoeurSupTotal != 0) {
			
			nbCoeur++;
		}

		if (listCoeur.Count != nbCoeur) {
			generateCoeurVide (nbCoeur);
		}

		updateCoeur (coeurComplet,partiCoeurSup);

	}

	private void updateCoeur(int coeurPlein,int particoeurSup){
		int step = 1;

		foreach (GameObject go in listCoeur) {
			if (coeurPlein >= (step)) {
				go.GetComponent<UnityEngine.UI.Image> ().sprite = searchSprite (EnumVieEtat.vie_plein);
			}else { 
				if (coeurPlein == step - 1) {
					switch (particoeurSup) {
					case 0:
						go.GetComponent<UnityEngine.UI.Image> ().sprite = searchSprite (EnumVieEtat.vie_vide);
						break;
					case 1:
						go.GetComponent<UnityEngine.UI.Image> ().sprite = searchSprite (EnumVieEtat.vie_1quart);
						break;
					case 2:
						go.GetComponent<UnityEngine.UI.Image> ().sprite = searchSprite (EnumVieEtat.vie_moitie);
						break;
					case 3:
						go.GetComponent<UnityEngine.UI.Image> ().sprite = searchSprite (EnumVieEtat.vie_3quart);
						break;
					}
				} else {
					go.GetComponent<UnityEngine.UI.Image> ().sprite = searchSprite (EnumVieEtat.vie_vide);
				}
			}
			step++;
		}
	}

	private Sprite searchSprite(EnumVieEtat enu){
		foreach (Vie vie in listVie) {
			if(vie.enu.Equals(enu)){
				return vie.image;
			}
		}
		return null;
	}

	private void generateCoeurVide(int nb){
		for (int i = 0; i < nb; i++) {
			generateCoeur (EnumVieEtat.vie_vide);
		}
	}
		
	private void generateCoeur(EnumVieEtat enu){
		foreach (Vie vie in listVie) {
			if (vie.enu.Equals (enu)) {
				float decalX = 0.0f;
				GameObject temp = new GameObject();
				if (listCoeur.Count > 0) {
					decalX = listCoeur [listCoeur.Count - 1].GetComponent<RectTransform> ().anchoredPosition[0] +45.0f;
				}
				temp.AddComponent<UnityEngine.UI.Image> ();
				temp.GetComponent<UnityEngine.UI.Image> ().sprite = vie.image;
				temp.transform.parent = gameObject.transform;
				temp.GetComponent<RectTransform> ().anchorMin = new Vector2 (0.5f, 0.5f);
				temp.GetComponent<RectTransform> ().anchorMax = new Vector2 (0.5f, 0.5f);
				temp.GetComponent<RectTransform> ().anchoredPosition = new Vector3 (decalX, 0.0f, 0.0f);
				temp.GetComponent<RectTransform> ().localScale = new Vector3 (0.4f, 0.4f, 0.4f);
				//Instantiate(temp);
				listCoeur.Add (temp);
				break;
			}
		}
	}

}

public enum EnumVieEtat{

	vie_plein,
	vie_1quart,
	vie_moitie,
	vie_3quart,
	vie_vide,
}
