using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class MenuPrincipal : MonoBehaviour, ISelectHandler, IDeselectHandler {

	[Header("Bouton du menu principal :")]
    public Button[] BoutonMenuPrincipal;

    [Header("Nom de la fonction qu'activera chaque bouton :")]
    public string[] NomFonction;

    public string NomDeLaSceneTuto;
    public string NomDeLaSceneCredit;
    public string NomDeLaSceneChargement;
    private int BoutonSelectionner;

    [Header("Pannel Menu :")]
    public GameObject AffichePanelMenuPrincipal;

    [Header("Pannel Controle :")]
    public GameObject AffichePanelControle;
    private bool AfficheControleEnCour;
    [HideInInspector]
    public SoundEntity se;

    [Header("Son :")]
    public AudioClip TicSound;
    public GameObject Princesse;

    public GameObject[] Soulignage;
	public AudioClip PageSound;

	void Awake() {
        se = GetComponent<SoundEntity>();
        BoutonSelectionner = 0;
        AfficheControleEnCour = false;
        AffichePanelControle.SetActive(false);
        AffichePanelMenuPrincipal.SetActive(true);
        for(int i=0; i<Soulignage.Length; i++)
            Soulignage[i].SetActive(false);

		BoutonMenuPrincipal[0].Select();
		PlayerPrefs.SetString("SceneToLoad", null);
    }

    void Update() {
		/*
        if (!AfficheControleEnCour) {
            DeplacementBouton();
            if (Input.GetButtonDown("Interagir") || Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Return))
            {
                ActiveBouton(BoutonSelectionner);
            }
        } else {
            if (Input.GetButtonDown("Interagir") || Input.GetButtonDown("Submit") || Input.GetKeyDown(KeyCode.Return))
            {
                Princesse.GetComponent<Animator>().SetBool("Controle", false);
                Princesse.GetComponent<Animator>().SetTrigger("RangeBook");
                RetourMenuPrincipal();
            }
        }*/
    }

	public void OnSelect(BaseEventData eventData) {
		se.playOneShot(TicSound);
		GetComponentInChildren<Image>().enabled = true;
	}

	public void OnDeselect(BaseEventData eventData) {
		GetComponentInChildren<Image>().enabled = false;
	}

	private void DeplacementBouton() {
        if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") > 0 || Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") < 0) {
            BoutonSelectionner++;
            BoutonSelectionner = CheckConteur(BoutonSelectionner);
            // Jous le son "Tic"
            se.playOneShot(TicSound);
        }
        else if (Input.GetButtonDown("Horizontal") && Input.GetAxisRaw("Horizontal") < 0 || Input.GetButtonDown("Vertical") && Input.GetAxisRaw("Vertical") > 0) {
            BoutonSelectionner--;
            BoutonSelectionner = CheckConteur(BoutonSelectionner);
            // Jous le son "Tic"
            se.playOneShot(TicSound);
        }
        // Check quel bouton doit être activé et les autre se désactive
        CheckBoutonSelectionner(BoutonSelectionner);
    }

    private void ActiveBouton(int compteur)
    {
        // Jous le son "Tic"
        se.playOneShot(TicSound);

        int Taille = BoutonMenuPrincipal.Length;

        for (int i = 0; i < Taille; i++)
        {
            if (i == compteur)
            {
                Invoke(NomFonction[i], 0f);
            }
        }
    }

    private int CheckConteur(int compteur) {
        int Taille = BoutonMenuPrincipal.Length;
        int ReelMaxTaille = BoutonMenuPrincipal.Length - 1;
        if (compteur < 0) {
            compteur = Taille - 1;
        }
        else if (compteur > ReelMaxTaille) {
            compteur = 0;
        }

        return compteur;
    }

    private void CheckBoutonSelectionner(int compteur) {
        int Taille = BoutonMenuPrincipal.Length;

        for (int i = 0; i < Taille; i++) {
            if (i == compteur) {
                Soulignage[i].SetActive(true);
                BoutonMenuPrincipal[i].Select();
            } else {
                Soulignage[i].SetActive(false);
            }
        }
    }

    private void RetourMenuPrincipal()
    {
        // Jous le son "Tic"
        se.playOneShot(TicSound);
        AfficheControleEnCour = false;
        AffichePanelMenuPrincipal.SetActive(true);
        AffichePanelControle.SetActive(false);
        BoutonSelectionner = 1;
    }

    public void LancerPartie() {
        AffichePanelMenuPrincipal.SetActive(false);
        PlayerPrefs.SetString("SceneToLoad", NomDeLaSceneTuto);
        SceneManager.LoadScene(NomDeLaSceneChargement);
    }

    public void LanceCredit() {
        PlayerPrefs.SetString("SceneToLoad", NomDeLaSceneCredit);
        SceneManager.LoadScene(NomDeLaSceneChargement);
    }

    public void LanceControle() {
        Princesse.GetComponent<Animator>().SetBool("Controle", true);
        Princesse.GetComponent<Animator>().SetTrigger("IsBook");
        AfficheControleEnCour = true;
		se.playOneShot(PageSound);
		AffichePanelMenuPrincipal.SetActive(false);
        AffichePanelControle.SetActive(true);
    }

    public void QuiterJeu() {
        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

}

