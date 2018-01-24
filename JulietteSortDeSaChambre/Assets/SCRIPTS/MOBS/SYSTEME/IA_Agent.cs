﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_Agent : MonoBehaviour {

    private NavMeshAgent nav;
    private Animator anim;
	private Rigidbody rb;
	private GameObject princesse;
//	private princesse_vie princesseVie;
//	private princesse_arme princesseArme;
    private IA_PointInteret[] pointsInteret;
	private IA_MobVie mobVie;
	private Vector3 destination;
	private SoundEntity se;
    
	public IA_Etat etatCourant;
	public float vitesseAngulaire;

//	public float distanceCombatOptimale;
//	public float distanceRepousse;

	public IA_Etat etatMort;
//	public IA_Etat etatDegatsRecu;

//	public AudioClip[] bruitsPas;
//	public float minPitch;
//	public float maxPitch;
//	public float minVolume;
//	public float maxVolume;
//	private float timerStep;

    void Awake()
    {
        nav = this.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody> ();
		princesse = GameObject.FindGameObjectWithTag("Player");
//		princesseVie = princesse.GetComponent<princesse_vie>();
//		princesseArme = princesse.GetComponent<princesse_arme>();
        pointsInteret = GameObject.FindObjectsOfType<IA_PointInteret>();
		mobVie = GetComponent<IA_MobVie> ();
		se = GetComponent<SoundEntity> ();
    }

    // Use this for initialization
    void Start () {
		
        etatCourant.entrerEtat();
    }
	
	// Update is called once per frame
	void Update () {

        etatCourant.faireEtat();
//		if (timerStep <= Time.time && nav.velocity.magnitude != 0.0f) {
//			int indice = Random.Range (0, this.bruitsPas.Length);
//			float volume = Random.Range (minVolume, maxVolume);
//			float pitch = Random.Range (minPitch, maxPitch);
//			se.playOneShot(this.bruitsPas[indice], volume, pitch);
//			timerStep = Time.time + (Random.Range (0.9f, 1.0f) * (1.0f / nav.velocity.magnitude) * 0.8f);
//		}
	}

	public NavMeshAgent getNav()
	{
		return nav;
	}

	public Animator getAnimator(){
		return anim;
	}

	public Rigidbody getRigidbody()
	{
		return rb;
	}

	public GameObject getPrincesse()
	{
		return princesse;
	}

//	public princesse_vie getPrincesse_Vie()
//	{
//		return princesseVie;
//	}
//
//	public princesse_arme getPrincesse_Arme()
//	{
//		return princesseArme;
//	}

	public IA_PointInteret[] getPointsInteret()
	{
		return pointsInteret;
	}

	public IA_MobVie getMobVie()
	{
		return mobVie;
	}

	public SoundEntity getSoundEntity(){
		return se;
	}

	/// <summary>
	/// Définit la position de la destination actuel de l'agent.
	/// </summary>
	public void definirDestination(Vector3 positionDestination)
	{
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
    public void changerEtat(IA_Etat nouvelEtat)
    {
        etatCourant.sortirEtat();
        etatCourant = nouvelEtat;
//		Debug.Log (this.gameObject.name + " entre dans l'état " + etatCourant.ToString());
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

//	public void recevoirDegat(int valeurDegats, Vector3 hitPoint) {
//
//		if (etatDegatsRecu != null) {
//			changerEtat (etatDegatsRecu);
//		}
//		mobVie.blesser (valeurDegats, hitPoint);
//	}
//
//	public void recevoirDegat(int valeurDegats) {
//
//		if (etatDegatsRecu != null) {
//			changerEtat (etatDegatsRecu);
//		}
//		mobVie.blesser (valeurDegats);
//	}

	public bool estEnVie() {
		return mobVie.estEnVie ();
	}

	public void mourir() {
		changerEtat (etatMort);
	}

	public bool estAuSol(){

		RaycastHit hitInfo;

		Physics.Raycast (this.transform.position, -this.transform.up, out hitInfo);

		return hitInfo.distance <= 0.065f;
	}
}
