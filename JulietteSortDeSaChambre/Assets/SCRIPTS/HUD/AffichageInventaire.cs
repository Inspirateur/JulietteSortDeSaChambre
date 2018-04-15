using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AffichageInventaire : MonoBehaviour {

	[HideInInspector]
	public List<ObjetInventaire> listObjet;
	private Dictionary<EnumObjetProgression,GameObject> dicoInventaire;
	public GameObject objetInventaire;
	public GameObject sac;
	public int tempsAffichage;
	private PrincesseObjetProgression juliette;
	private PrincesseDeplacement deplacement;

	private bool affiche;
	private float temps;
	private static AffichageInventaire instance;

	// Use this for initialization
	void Start () {
		deplacement = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>();
		juliette = GameObject.FindGameObjectWithTag ("Player").GetComponent<PrincesseObjetProgression> ();
		dicoInventaire = new Dictionary<EnumObjetProgression, GameObject> ();
		// dicoNbObjet = new Dictionary<EnumObjetProgression, int> ();
		affiche = false;
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (!affiche && InputManager.GetButtonDown ("Select") && deplacement.canOpenInventory) {
				afficherToutObjet ();
				temps = Time.time;
				affiche = true;
		}

		if (affiche && temps+tempsAffichage<=Time.time) {
			affiche = false;
			desafficherToutObjet ();
		}
	}

	public static AffichageInventaire getInstance(){
		return instance;
	}

	private void afficherToutObjet(){
		
		foreach (GameObject go in dicoInventaire.Values) {
			go.SetActive (true);
		}
	}

	private void desafficherToutObjet(){

		foreach (GameObject go in dicoInventaire.Values) {
			go.SetActive (false);
		}
	}


	public void objetRamasse(EnumObjetProgression enu){
		sac.SetActive (true);
		foreach(ObjetInventaire objet in listObjet){
			if(objet.objet.Equals(enu)){
				createObjet (objet, enu);
			}
		}
	}

	public void objetPerdu(EnumObjetProgression enu){
		desafficherToutObjet ();
		if(!juliette.hasItem()){
			sac.SetActive (false);
		}
		deleteObjet (enu);
	}

	private void createObjet(ObjetInventaire objet,EnumObjetProgression enu){
		GameObject temp= Instantiate(objetInventaire);
		temp.transform.parent = gameObject.transform.GetChild (0);
		temp.SetActive (true);
		temp.name = string.Concat (temp.name, "(", objet.objet.ToString(), ")");
		temp.GetComponent<RectTransform> ().localScale = objet.scale;
		temp.GetComponent<RectTransform> ().sizeDelta = objet.size;
		temp.GetComponent<UnityEngine.UI.Image> ().sprite = objet.image;
		temp.SetActive (false);
		dicoInventaire.Add (enu, temp);

	}


	private void deleteObjet(EnumObjetProgression enu){
		dicoInventaire.Remove (enu);
		Destroy( dicoInventaire [enu]);
	}

	public void recreateDicoInventaireFromPrincesse(){
		dicoInventaire = new Dictionary<EnumObjetProgression, GameObject> ();
		sac.SetActive (false);

	}
}
