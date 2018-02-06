using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichageInterraction : MonoBehaviour {

	private Dictionary<EnumIconeInterraction,GameObject> dico;
	private HashSet<ObjetInteractifs> listObjetInteractif;

	// Use this for initialization
	void Start () {
		listObjetInteractif = new HashSet<ObjetInteractifs> ();
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
			if (!(a.getIconeInteraction().Equals(EnumIconeInterraction.icone_null))) {
				
				afficheObjet (a.getIconeInteraction());
			}

		} else {
			desafficheObjet ();
		}
	}

	public void activeAffichageInteractionObjet(ObjetInteractifs objet){
		listObjetInteractif.Add(objet);

	}

	public void desactiveAffichageInteractionObjet(ObjetInteractifs objet){
		if(listObjetInteractif.Contains(objet)){
			listObjetInteractif.Remove (objet);
		}
	}


	private void afficheObjet(EnumIconeInterraction enu){
		if (dico.ContainsKey (enu)) {
			dico [enu].SetActive (true);
		} 
	}

	private void desafficheObjet(){
		foreach (GameObject game in dico.Values) {
			game.SetActive (false);
		}
	}

}

public enum EnumIconeInterraction{
	icone_null,
	icone_default,
	icone_non_default,
	icone_pied_de_lit,
}
