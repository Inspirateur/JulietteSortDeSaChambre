using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour {

	[Header("Positions de la plateforme :")]
	public Transform[] Position;

	[Header("Position de depart :")]
	public int Etat;
	
	[Header("Vitesse :")]
	public float Vitesse;

	[Header("Temps avant de check la position avant déplacement :")]
	public float TimeWaitForCheck;
	private float OriginalResetTime;

	[Header("Déplacement cyclique :")]
	public bool Cycle;

	[Header("Arreté au depart")]
	public bool isStop;

	private Vector3 NouvellePosition;
	private bool DeplacementRetour;
	private int PostionToCheck;

	// Use this for initialization
	void Start () {
		this.transform.position = Position[Etat].position;
		if (!isStop) {
			StartPlateformeBeggening ();
		}
		//StartCoroutine (Wait());
	}

	public void StartPlateformeBeggening() {
		isStop = false;
		if (Cycle)
		{
			ChangeNouvellePositionLineaire();
		} else if (!Cycle)
		{
			OriginalResetTime = TimeWaitForCheck;
			DeplacementRetour = false;
			ChangeNouvellePositionNonLineaire();
		}
	}

	public void StotPlateforme() {
		isStop = true;
	}

	public void RestartPlateforme() {
		isStop = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isStop) {
			this.transform.position = Vector3.MoveTowards(this.transform.position, NouvellePosition,  Vitesse * Time.deltaTime);
		}
	}

	void ChangeNouvellePositionLineaire() {
		for (int i = 0; i < Position.Length; i++ )
		{
			if (Etat == i)
			{
				if (i+1 >= Position.Length)
				{
					Etat = 0;
					NouvellePosition = Position[0].position;
					PostionToCheck = 0;
					CheckPositionLineaire();
				} else {
					Etat = i+1;
					NouvellePosition = Position[i+1].position;
					PostionToCheck = i+1;
					CheckPositionLineaire();
				}
				i = Position.Length;
			}
		}
	}

	void CheckPositionLineaire() {
		if (this.transform.position == Position[PostionToCheck].position)
		{
			ChangeNouvellePositionLineaire();
		} else 
		{
			Invoke("CheckPositionLineaire", TimeWaitForCheck);
		}
	}

	void ChangeNouvellePositionNonLineaire() {

		TimeWaitForCheck = OriginalResetTime;

		if (!DeplacementRetour)
		{
			for (int i = 0; i < Position.Length; i++ )
			{
				if (Etat == i)
				{
					if (i+1 >= Position.Length)
					{
						TimeWaitForCheck = 0;
						DeplacementRetour = true;
						ChangeNouvellePositionNonLineaire();
					} else {
						Etat = i+1;
						NouvellePosition = Position[i+1].position;
						PostionToCheck = i+1;
						CheckPositionNonLineaire();
					}
					i = Position.Length;
				}
			}
		} else if (DeplacementRetour)
		{
			for (int i = Position.Length-1; i >= 0; i-- )
			{
				if (Etat == i)
				{
					if (i-1 < 0)
					{
						TimeWaitForCheck = 0;
						DeplacementRetour = false;
						ChangeNouvellePositionNonLineaire();
					} else {
						Etat = i-1;
						NouvellePosition = Position[i-1].position;
						PostionToCheck = i-1;
						CheckPositionNonLineaire();
					}
					i = -1;
				}
			}
		}	
		//Invoke("ChangeNouvellePositionNonLineaire", ResetTime);
	}

	void CheckPositionNonLineaire() {
		if (this.transform.position == Position[PostionToCheck].position)
		{
			ChangeNouvellePositionNonLineaire();
		} else 
		{
			Invoke("CheckPositionNonLineaire", TimeWaitForCheck);
		}
	}

	IEnumerator Wait(){
		yield return new WaitForSeconds(4f);
		StartPlateformeBeggening ();
	}

}
