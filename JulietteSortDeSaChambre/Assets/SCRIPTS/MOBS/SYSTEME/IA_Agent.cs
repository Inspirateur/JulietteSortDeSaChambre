using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Agent : MonoBehaviour {

    private NavMeshAgent nav;
    private Animator anim;
	private Rigidbody rb;
	private GameObject princesse;
	private PrincesseVie princesseVie;
	private PrincesseArme princesseArme;
    private IA_PointInteret[] pointsInteret;
	private IA_MobVie mobVie;
	private Vector3 destination;
	private SoundEntity se;
	private MobManager me;
	private IA_Perception perception;
    
	public IA_Etat etatInitial;
	private IA_Etat etatCourant;
	public float vitesseAngulaire;

//	public float distanceCombatOptimale;
//	public float distanceRepousse;

	public IA_Etat etatMort;
	public IA_Etat etatEtreBlesseDefaut;

	public BruiteurPas bruiteurPas;
	private float timerStep;

    void Awake() {
        nav = this.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody> ();
		princesse = GameObject.FindGameObjectWithTag("Player");
		princesseVie = princesse.GetComponent<PrincesseVie>();
		princesseArme = princesse.GetComponent<PrincesseArme>();
        pointsInteret = GameObject.FindObjectsOfType<IA_PointInteret>();
		mobVie = GetComponent<IA_MobVie> ();
		se = GetComponent<SoundEntity> ();
		me = GameObject.FindGameObjectWithTag("MobManager").GetComponent<MobManager> ();
		perception = GetComponent<IA_Perception> ();
    }

    // Use this for initialization
    void Start () {
		me.AjouterAgent (this);
		etatCourant = etatInitial;
		etatCourant.entrerEtat();
    }
	
	// Update is called once per frame
	void Update () {

        etatCourant.faireEtat();
		if (timerStep <= Time.time && nav.velocity.magnitude != 0.0f) {
			bruiteurPas.pas();
			timerStep = Time.time + (Random.Range (0.9f, 1.0f) * Mathf.Min(1.0f / nav.velocity.magnitude, 1.0f));
		}
	}

	public NavMeshAgent getNav(){
		return nav;
	}

	public Animator getAnimator(){
		return anim;
	}

	public Rigidbody getRigidbody(){
		return rb;
	}

	public GameObject getPrincesse(){
		return princesse;
	}

	public PrincesseVie getPrincesseVie(){
		return princesseVie;
	}

	public PrincesseArme getPrincesseArme(){
		return princesseArme;
	}

	public IA_PointInteret[] getPointsInteret(){
		return pointsInteret;
	}

	public IA_MobVie getMobVie(){
		return mobVie;
	}

	public SoundEntity getSoundEntity(){
		return se;
	}

	public MobManager getMobManager(){
		return me;
	}

	public IA_Perception getPerception(){
		return perception;
	}

	//Utilisé par le MobManager
	public IA_Etat getEtatCourant(){
		return etatCourant;
	}


	/// <summary>
	/// Dans le cadre de la poursuite de la princesse, définit la destination en prenant en compte les autres mobs (avec MobManager.
	/// </summary>
	public void definirDestinationStrat() {
		this.definirDestination(me.getDestination(this));
	}

	/// <summary>
	/// Définit la position de la destination actuelle de l'agent.
	/// </summary>
	public void definirDestination(Vector3 positionDestination){
		destination = positionDestination;
		nav.SetDestination(positionDestination);
	}

	/// <summary>
	/// Définit le point d'interet de destination actuel de l'agent.
	/// </summary>
	public void definirDestination(IA_PointInteret pi)
	{
		definirDestination(pi.transform.position);
	}

    /// <summary>
    /// Définit le nom du point d'interet de destination actuel de l'agent.
    /// </summary>
    public void definirDestination(string nomDestination)
    {
		foreach (IA_PointInteret pi in pointsInteret)
        {
			if (pi.name.Equals(nomDestination))
            {
				definirDestination(pi);
                return;
            }
        }
    }

    /// <summary>
    /// Permet de savoir si l'agent a atteint sa destination.
    /// </summary>
    public bool destinationCouranteAtteinte()
    {
		Vector3 v = this.transform.position - destination;
		v.y = 0.0f;

        return v.magnitude <= 0.2f;
	}

	/// <summary>
	/// Tourne l'agent vers la position en fonction de sa vitesse angulaire maximale.
	/// Retourne false si la rotation est finie.
	/// </summary>
	public bool seTournerVersPosition(Vector3 positionCible) {

		Vector3 v = positionCible - this.transform.position;

		return seTournerEnDirectionDe (v);
	}

	/// <summary>
	/// Tourne l'agent vers la direction en fonction de sa vitesse angulaire maximale.
	/// Retourne false si la rotation est finie.
	/// </summary>
	public bool seTournerEnDirectionDe(Vector3 forward) {

		Quaternion q = new Quaternion ();
		q.SetLookRotation (forward);

		return seTourner (q);
	}

	/// <summary>
	/// Tourne l'agent vers l'orientation en fonction de sa vitesse angulaire maximale.
	/// Retourne false si la rotation est finie.
	/// </summary>
	public bool seTournerDansOrientationDe(GameObject obj) {

		return seTourner (obj.transform.rotation);
	}

	private bool seTourner(Quaternion q) {

		float difRotation = q.eulerAngles.y - this.transform.rotation.eulerAngles.y;

		float rotation;

		if (difRotation > 180.0f)
		{
			difRotation -= 360.0f;
		}

		if (difRotation < -180.0f)
		{
			difRotation += 360.0f;
		}

		rotation = Mathf.Clamp(difRotation, -vitesseAngulaire, vitesseAngulaire);

		this.transform.Rotate(0.0f, rotation, 0.0f);

		return Mathf.Abs(difRotation) > vitesseAngulaire ;
	}

    public void setAnimation(string nomAnimation)
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            anim.SetBool(parameter.name, false);
        }

		anim.SetBool(nomAnimation, true);
    }

	public bool isActualAnimation(string nomAnimation){
		return anim.GetCurrentAnimatorStateInfo (0).IsName (anim.GetLayerName(0) + "." + nomAnimation);
	}

    /// <summary>
    /// Permet de sortir de l'état courant puis d'entrer dans le nouvel état.
    /// </summary>
	public void changerEtat(IA_Etat nouvelEtat) {
		etatCourant.sortirEtat();
		etatCourant = nouvelEtat;
		Debug.Log (this.gameObject.name + " entre dans l'état " + etatCourant.ToString());
		etatCourant.entrerEtat();
	}

	public Vector3 directionToPrincesseDansPlanY0() {

		Vector3 dir = princesse.transform.position - this.transform.position;
		dir.y = 0.0f;
		return dir.normalized;
	}

	public float distanceToPrincesse() {
		return (princesse.transform.position - this.transform.position).magnitude;
	}

	public void subirDegats(int valeurDegats, Vector3 hitPoint) {
		etatCourant.subirDegats(valeurDegats, hitPoint);
	}

	public bool estEnVie() {
		return mobVie.estEnVie ();
	}

	public void mourir() {
		Debug.Log ("je rentre dans l'etat de mort, si ça ne se voit pas tu peux crier");
		changerEtat (etatMort);
	}

	public bool estAuSol(){

		RaycastHit hitInfo;

		Physics.Raycast (this.transform.position, -this.transform.up, out hitInfo);

		return hitInfo.distance <= 0.065f;
	}
}
