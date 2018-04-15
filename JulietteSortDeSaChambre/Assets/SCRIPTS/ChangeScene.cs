using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeScene : Evenement {

	    public string NomDeLaScene;
		public string NomDeLaSceneChargement;


	public void changeScene(){
		/*GameControl.control.Save ();
		PlayerPrefs.SetString("SceneToLoad", NomDeLaScene);
		SceneManager.LoadScene(NomDeLaSceneChargement);	*/
	}
	/*
	void OnTriggerEnter(Collider other){
		GameControl.control.Save ();
		PlayerPrefs.SetString("SceneToLoad", NomDeLaScene);
        SceneManager.LoadScene(NomDeLaSceneChargement);	
	}*/
}
