using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

	[Header("Position de la plateforme :")]
	public Transform PositionDepart;
	public Transform PositionArrive;

	private Vector3 NouvellePosition;
	private string Etat;

	[Header("Vitesse :")]
	public float Vitesse;

	[Header("Temps avant chaque déplacement :")]
	public float ResetTime;

	// Use this for initialization
	void Start () {
		Etat = "Direction Position de Départ";
		ChangeNouvellePosition();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.transform.position = Vector3.Lerp(this.transform.position, NouvellePosition, Vitesse);
	}

	void ChangeNouvellePosition() {
		if (Etat == "Direction Position de Départ")
		{
			Etat = "Direction Position d'arriver";
			NouvellePosition = PositionArrive.position;
		} else if (Etat == "Direction Position d'arriver")
		{
			Etat = "Direction Position de Départ";
			NouvellePosition = PositionDepart.position;
		}
		Invoke("ChangeNouvellePosition", ResetTime);
	}
}
