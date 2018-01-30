using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichageInterraction : MonoBehaviour {

	private Dictionary<EnumIconeInterraction,GameObject> dico;
	private HashSet<EnumIconeInterraction> listObjetInteractif;

	// Use this for initialization
	void Start () {
		listObjetInteractif = new HashSet<EnumIconeInterraction> ();
		dico = new Dictionary<EnumIconeInterraction, GameObject> ();
		foreach(IconeInteraction enu in GetComponentsInChildren<IconeInteraction>(true)){
			dico.Add (enu.typeIcone, enu.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (listObjetInteractif.Count > 0) {
			var enu = listObjetInteractif.GetEnumerator ();
			enu.MoveNext ();
			var a = enu.Current;
			afficheObjet (a);
		}
	}

	public void activeAffichageInteractionObjet(ObjetInteractifs objet){
		listObjetInteractif.Add(objet.getIconeInteraction ());

	}

	public void afficheObjet(EnumIconeInterraction enu){
		if (dico.ContainsKey (enu)) {
			dico [enu].SetActive (true);
		} 
	}

}

public enum EnumIconeInterraction{
	icone_default,
}
