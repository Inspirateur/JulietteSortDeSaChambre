using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincesseArme : MonoBehaviour {

    public EnumArmes armeActive;
	public float distanceLacherArme;
	public float hauteurLacherArme;
    public List<EnumArmes> listArmeTenu;
	public float delaisAvantApparitionProjetile;

    private GameObject actualHandArme;

	[Header("Dégâts des armes")]

	public int degatsPoele;
	public int degatschargePoele;
	public int degatsPain;
	public int degatschargePain;
	public int degatsPiedLit;
	public int degatschargePiedLit;
	public int degatsChandelier;
	public int degatschargeChandelier;
	public int degatsPelle;
	public int degatschargePelle;
	public int degatsBaguetteMagique;
	public int degatschargeBaguetteMagique;

	[Header("Recul des armes")]

	public float facteurReculPoele;
	public float facteurReculPain;
	public float facteurReculPiedLit;
	public float facteurReculChandelier;
	public float facteurReculPelle;
	public float facteurReculBaguetteMagique;

	[Header("Armes dans la main de Juliette")]

    public GameObject handPoele;
    public GameObject handPain;
    public GameObject handPiedLit;
    public GameObject handChandelier;
	public GameObject handPelle;
	public GameObject handBaguetteMagique;

	[Header("Prefab des armes à placer dans le monde")]

	public GameObject prefabPoele;
	public GameObject prefabPain;
	public GameObject prefabPiedLit;
	public GameObject prefabChandelier;
	public GameObject prefabPelle;
	public GameObject prefabBaguetteMagique;

	[Header("Conditionnement des projectiles")]

	public GameObject positionDepartProjectile;
	public float distanceMaxViseProjectile;

	[Header("Prefab des projectiles")]

	public GameObject projectileBaguetteMagique;

	private bool attaqueCorpsACorpsEnCours;
	private bool attaqueDistanceEnCours;

	private bool attaqueChargeEnCours;
	private float timerApparitionProjectile;
	private bool projectileDejaCree;
	private GameObject projectileActuel;
	private Animator anim;
	private List<IA_Agent> listeMobsTouches;

	private int degatsArmeActuelle;

	private int degatschargeArmeActuelle;
	private float facteurReculArmeActuelle;
	private float timerAttaque;
	private bool zoom;
	private camera cam;

    // Use this for initialization
    void Start () {
		
		attaqueCorpsACorpsEnCours = false;
		attaqueDistanceEnCours = false;
		attaqueChargeEnCours = false;
		anim = GetComponent<Animator> ();
		listeMobsTouches = new List<IA_Agent> ();

        SetArmeActive (GameControl.control.ArmeCourante, CreerUneArmeDepuisLEnum (GameControl.control.ArmeCourante));

        listArmeTenu = new List<EnumArmes> ();
        listArmeTenu = GameControl.control.listArmeTenu;

		zoom = false;

		this.cam = Camera.main.GetComponent<camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
		if ((attaqueCorpsACorpsEnCours || attaqueDistanceEnCours || attaqueChargeEnCours)
		&& anim.GetCurrentAnimatorStateInfo (0).IsName (anim.GetLayerName (0) + ".idle1")
		&& Time.time >= timerAttaque) {
			
			attaqueCorpsACorpsEnCours = false;
			attaqueDistanceEnCours = false;
			attaqueChargeEnCours = false;
			listeMobsTouches.Clear ();

		}

		if(attaqueDistanceEnCours && Time.time >= timerApparitionProjectile && !projectileDejaCree){
			this.lancerProjectile();
		}

		if(this.armeActive.Equals(EnumArmes.BAGUETTE_MAGIQUE)){

			if(InputManager.GetButtonDown("Zoom")){

				if(this.zoom){
					this.zoom = false;
					this.cam.dezoomer();
				}
				else{
					this.zoom = true;
					this.cam.zoomer();
				}
			}

		} else if(this.zoom) {
			this.zoom = false;
			this.cam.dezoomer();
		}

		if(this.zoom){

			Vector3 temp = this.cam.transform.forward;

			temp.y = 0.0f;

			temp.Normalize();

			this.transform.forward = temp;
		}
	}

	public GameObject getHandArme(){
	return actualHandArme;
	}

	void OnTriggerEnter(Collider other){
        if (attaqueCorpsACorpsEnCours) {
            if (other.tag.Equals ("Mob")) {
				
				IA_Agent mobTouche = other.gameObject.GetComponent<IA_Agent> ();

				if (!listeMobsTouches.Contains (mobTouche) && mobTouche.estEnVie()) {
					
					listeMobsTouches.Add (mobTouche);

					Vector3 hitPoint = other.ClosestPoint (this.transform.position);

					mobTouche.subirDegats (degatsArmeActuelle, hitPoint);

                    bool MobTouch = true;

                    gameObject.GetComponent<ArmesParticulesEffect>().ParticulePlay(GameControl.control.ArmeCourante, hitPoint, MobTouch);
				}
			}
            if (other.tag.Equals("wall"))
            {
                Vector3 hitPoint = other.ClosestPoint(this.transform.position);
                bool MobTouch = false;
                gameObject.GetComponent<ArmesParticulesEffect>().ParticulePlay(GameControl.control.ArmeCourante, hitPoint, MobTouch);
            }
        }

		if (attaqueChargeEnCours) {
            if (other.tag.Equals ("Mob")) {
				
				IA_Agent mobTouche = other.gameObject.GetComponent<IA_Agent> ();

				if (!listeMobsTouches.Contains (mobTouche) && mobTouche.estEnVie()) {
					
					listeMobsTouches.Add (mobTouche);

					Vector3 hitPoint = other.ClosestPoint (this.transform.position);

					mobTouche.subirDegats (degatschargeArmeActuelle, hitPoint, EnumEffet.ETOURDIR);

                    bool MobTouch = true;

                    gameObject.GetComponent<ArmesParticulesEffect>().ParticulePlay(GameControl.control.ArmeCourante, hitPoint, MobTouch);
				}
			}
            if (other.tag.Equals("wall"))
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

		timerAttaque = Time.time + 0.1f;
		if(this.armeActive.Equals(EnumArmes.BAGUETTE_MAGIQUE)) {
			attaqueCorpsACorpsEnCours = false;
			attaqueChargeEnCours = false;
			attaqueDistanceEnCours = true;
			timerApparitionProjectile = Time.time + this.delaisAvantApparitionProjetile;
			projectileDejaCree = false;
		} else {
			attaqueCorpsACorpsEnCours = true;
			attaqueDistanceEnCours = false;
			attaqueChargeEnCours = false;
		}

		listeMobsTouches.Clear ();
	}


	public void lancerAttaqueCharge(){

		switch(armeActive)
		{
			case EnumArmes.VIDE:
		attaqueChargeEnCours = true;
		attaqueCorpsACorpsEnCours = false;
		attaqueDistanceEnCours = false;
	
			break;

			case EnumArmes.PIED_LIT:
		attaqueChargeEnCours = true;
		attaqueCorpsACorpsEnCours = false;
		attaqueDistanceEnCours = false;

			break;

		}
	}

	private void lancerProjectile(){
		
		projectileDejaCree = true;

		GameObject cam = GameObject.FindGameObjectWithTag("MainCamera");

		Vector3 dirCamera = cam.transform.forward;

		RaycastHit[] hitInfos = Physics.RaycastAll(cam.transform.position, dirCamera, this.distanceMaxViseProjectile);

		float distance = this.distanceMaxViseProjectile;

		foreach (RaycastHit info in hitInfos){
			if(!info.collider.gameObject.Equals(this.gameObject)){
				distance = Mathf.Min(info.distance, distance);
			}
		}

		Vector3 impactPoint = cam.transform.position + (dirCamera * distance);

		Projectile projectile = Instantiate(this.projectileActuel, positionDepartProjectile.transform.position, Quaternion.identity).GetComponent<Projectile>();

		projectile.setDestination(impactPoint);
		projectile.degats = degatsArmeActuelle;
		projectile.recul = facteurReculArmeActuelle;
	}

	public bool isAttaqueEnCours() {
		return attaqueCorpsACorpsEnCours || attaqueDistanceEnCours || attaqueChargeEnCours;
	}

	private void defineActualsArmes(GameObject armeRamasse)
    {
        GameControl.control.ArmeCourante = armeActive;
		switch (armeActive)
		{
		case EnumArmes.VIDE:

			actualHandArme = null;
			degatsArmeActuelle = 0;
			degatschargeArmeActuelle = 0;
			facteurReculArmeActuelle = 0.0f;
			break;

		case EnumArmes.POELE:

			actualHandArme = handPoele;
			degatsArmeActuelle = degatsPoele;
			degatschargeArmeActuelle = degatschargePoele;
			facteurReculArmeActuelle = facteurReculPoele;
			break;

		case EnumArmes.PAIN:

			actualHandArme = handPain;
			degatsArmeActuelle = degatsPain;
			degatschargeArmeActuelle = degatschargePain;
			facteurReculArmeActuelle = facteurReculPain;
			break;

		case EnumArmes.PIED_LIT:

			actualHandArme = handPiedLit;
			degatsArmeActuelle = degatsPiedLit;
			degatschargeArmeActuelle = degatschargePiedLit;
			facteurReculArmeActuelle = facteurReculPiedLit;
			break;

		case EnumArmes.CHANDELIER:

			actualHandArme = handChandelier;
			degatsArmeActuelle = degatsChandelier;
			degatschargeArmeActuelle = degatschargeChandelier;
			facteurReculArmeActuelle = facteurReculChandelier;
			break;

		case EnumArmes.PELLE:

			actualHandArme = handPelle;
			degatsArmeActuelle = degatsPelle;
			degatschargeArmeActuelle = degatschargePelle;
			facteurReculArmeActuelle = facteurReculPelle;
			break;

		case EnumArmes.BAGUETTE_MAGIQUE:

			actualHandArme = handBaguetteMagique;
			degatsArmeActuelle = degatsBaguetteMagique;
			degatschargeArmeActuelle = degatschargeBaguetteMagique;
			facteurReculArmeActuelle = facteurReculBaguetteMagique;
			projectileActuel = projectileBaguetteMagique;
			break;
		}

		Destroy (armeRamasse);
    }

    private void poserArme()
    {
        if(armeActive != EnumArmes.VIDE)
        {
            actualHandArme.SetActive(false);

			GameObject prefab = getPrefabArmeActuel ();

			Instantiate (prefab, this.transform.position + this.transform.forward * this.distanceLacherArme + this.transform.up * this.hauteurLacherArme, prefab.transform.rotation);
        }
    }

    private void activerArme()
    {
        if(armeActive != EnumArmes.VIDE)
        {
            actualHandArme.SetActive(true);
        }
    }

	public float getFacteurReculeArmeActuelle(){
		return facteurReculArmeActuelle;
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