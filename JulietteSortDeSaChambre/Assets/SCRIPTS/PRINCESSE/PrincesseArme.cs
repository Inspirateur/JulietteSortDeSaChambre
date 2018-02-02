using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincesseArme : MonoBehaviour {

    public EnumArmes armeActive;
    public List<EnumArmes> listArmeTenu;

    private GameObject actualHandArme;

	public int degatsPoele;
	public int degatsPain;
	public int degatsPiedLit;
	public int degatsChandelier;
	public int degatsPelle;
	public int degatsBaguetteMagique;

	public float facteurReculePoele;
	public float facteurReculePain;
	public float facteurReculePiedLit;
	public float facteurReculeChandelier;
	public float facteurReculePelle;
	public float facteurReculeBaguetteMagique;

    public GameObject handPoele;
    public GameObject handPain;
    public GameObject handPiedLit;
    public GameObject handChandelier;
	public GameObject handPelle;
	public GameObject handBaguetteMagique;

	public GameObject prefabPoele;
	public GameObject prefabPain;
	public GameObject prefabPiedLit;
	public GameObject prefabChandelier;
	public GameObject prefabPelle;
	public GameObject prefabBaguetteMagique;

	private bool attaqueEnCours;
	private Animator anim;
	private List<IA_Agent> listeMobsTouches;

	private int degatsArmeActuelle;
	private float facteurReculeArmeActuelle;

    // Use this for initialization
    void Start () {
		
		attaqueEnCours = false;
		anim = GetComponent<Animator> ();
		listeMobsTouches = new List<IA_Agent> ();

        SetArmeActive (GameControl.control.ArmeCourante, CreerUneArmeDepuisLEnum (GameControl.control.ArmeCourante));

        listArmeTenu = new List<EnumArmes> ();
        listArmeTenu = GameControl.control.listArmeTenu;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (attaqueEnCours && anim.GetCurrentAnimatorStateInfo (0).IsName (anim.GetLayerName (0) + ".idle1")) {
			
			attaqueEnCours = false;
			listeMobsTouches.Clear ();

		}
	}

	void OnTriggerEnter(Collider other){
        if (attaqueEnCours) {
            if (other.tag.Equals ("Mob")) {
				
				IA_Agent mobTouche = other.gameObject.GetComponent<IA_Agent> ();

				if (!listeMobsTouches.Contains (mobTouche) && mobTouche.estEnVie()) {
					
					listeMobsTouches.Add (mobTouche);

					Vector3 hitPoint = other.ClosestPoint (this.transform.position);

					mobTouche.subirDegats (degatsArmeActuelle, hitPoint);

                    bool MobTouch = false;

                    gameObject.GetComponent<ArmesParticulesEffect>().ParticulePlay(GameControl.control.ArmeCourante, hitPoint, MobTouch);
				}
			}
            if (other.tag.Equals("Wall"))
            {
                Vector3 hitPoint = other.ClosestPoint(this.transform.position);
                bool MobTouch = false;
                gameObject.GetComponent<ArmesParticulesEffect>().ParticulePlay(GameControl.control.ArmeCourante, hitPoint, MobTouch);
            }
        }
	}

	public void SetArmeActive(EnumArmes typeArme, GameObject armeRamasse)
    {
        poserArme();
        armeActive = typeArme;
		defineActualsArmes(armeRamasse);
        activerArme();
    }

	public void lancerAttaque() {
        Debug.Log("lancerAttaque");
		attaqueEnCours = true;
		listeMobsTouches.Clear ();
	}

	public bool isAttaqueEnCours() {
		return attaqueEnCours;
	}

	private void defineActualsArmes(GameObject armeRamasse)
    {
        //actualWorldArme = armeRamasse;
        GameControl.control.ArmeCourante = armeActive;
		switch (armeActive)
		{
		case EnumArmes.VIDE:

			actualHandArme = null;
//			actualWorldArme = null;
			degatsArmeActuelle = 0;
			facteurReculeArmeActuelle = 0.0f;
			break;

		case EnumArmes.POELE:

			actualHandArme = handPoele;
			degatsArmeActuelle = degatsPoele;
			facteurReculeArmeActuelle = facteurReculePoele;
			break;

		case EnumArmes.PAIN:

			actualHandArme = handPain;
			degatsArmeActuelle = degatsPain;
			facteurReculeArmeActuelle = facteurReculePain;
			break;

		case EnumArmes.PIED_LIT:

			actualHandArme = handPiedLit;
			degatsArmeActuelle = degatsPiedLit;
			facteurReculeArmeActuelle = facteurReculePiedLit;
			break;

		case EnumArmes.CHANDELIER:

			actualHandArme = handChandelier;
			degatsArmeActuelle = degatsChandelier;
			facteurReculeArmeActuelle = facteurReculeChandelier;
			break;

		case EnumArmes.PELLE:

			actualHandArme = handPelle;
			degatsArmeActuelle = degatsPelle;
			facteurReculeArmeActuelle = facteurReculePelle;
			break;

		case EnumArmes.BAGUETTE_MAGIQUE:

			actualHandArme = handBaguetteMagique;
			degatsArmeActuelle = degatsBaguetteMagique;
			facteurReculeArmeActuelle = facteurReculeBaguetteMagique;
			break;
		}

		Destroy (armeRamasse);
    }

    private void poserArme()
    {
        if(armeActive != EnumArmes.VIDE)
        {
            actualHandArme.SetActive(false);
//            actualWorldArme.transform.SetPositionAndRotation(this.transform.position + this.transform.forward + this.transform.up, new Quaternion());
//            actualWorldArme.SetActive(true);

			GameObject prefab = getPrefabArmeActuel ();

			Instantiate (prefab, this.transform.position + this.transform.forward * 1.5f + this.transform.up * 1.0f, prefab.transform.rotation);
        }
    }

    private void activerArme()
    {
        if(armeActive != EnumArmes.VIDE)
        {
//            actualWorldArme.SetActive(false);

            actualHandArme.SetActive(true);
        }
    }

	public float getFacteurReculeArmeActuelle(){
		return facteurReculeArmeActuelle;
	}

	public GameObject CreerUneArmeDepuisLEnum(EnumArmes arme)
	{
		GameObject template = null;
		switch (arme) {
		case EnumArmes.PIED_LIT:
			template = handPiedLit;
			break;
		case EnumArmes.POELE:
			template = handPoele;
			break;
		case EnumArmes.VIDE:
			template = null;
			break;
		case EnumArmes.PAIN:
			template = handPain;
			break;
		case EnumArmes.CHANDELIER:
			template = handChandelier;
			break;
		case EnumArmes.BAGUETTE_MAGIQUE:
			template = handBaguetteMagique;
			break;
		case EnumArmes.PELLE:
			template = handPelle;
			break;
		}

		if (template == null)
			return null;
		return GameObject.Instantiate (template);
	}

	private GameObject getPrefabArmeActuel(){
		switch (armeActive)
		{
		case EnumArmes.VIDE:
			return null;

		case EnumArmes.POELE:
			return prefabPoele;

		case EnumArmes.PAIN:
			return prefabPain;

		case EnumArmes.PIED_LIT:
			return prefabPiedLit;

		case EnumArmes.CHANDELIER:
			return prefabChandelier;

		case EnumArmes.PELLE:
			return prefabPelle;

		case EnumArmes.BAGUETTE_MAGIQUE:
			return prefabBaguetteMagique;
		}

		return null;
	}
}

public enum EnumArmes
{
    VIDE,
    POELE,
    PAIN,
    PIED_LIT,
    CHANDELIER,
    PELLE,
	BAGUETTE_MAGIQUE
}