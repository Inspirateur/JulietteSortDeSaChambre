using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRO_E_Patrouiller : IA_Etat {

    public float vitesse;
	public float delaisAChaqueArret;
	public float niveauAttentionEnMarche;
	public float niveauAttentionArret;

	public IA_PointInteret[] chemin;

	public AudioClip sonArret;
	public AudioClip sonPoursuite;

    private int indiceCheminActuel;
	private float delaisActuel;
	private bool enChemin;
	private int indiceDernierPointRejoint;

    // Use this for initialization
    void Start () {
        base.init(); // permet d'initialiser l'état, ne pas l'oublier !
		this.delaisActuel = 0.0f;
    }

    public override void entrerEtat()
	{
		//On cherche le point le plus proche
		float minDist = float.MaxValue;
		float minId = 0;
		float dist;
		for (int i = 0; i < chemin.Length; ++i) {
			dist = (agent.transform.position - chemin [i].transform.position).magnitude;
			if (dist < minDist) {
				minId = i;
				minDist = dist;
			}
		}
		indiceDernierPointRejoint = (int)minId - 1;
		setAnimation(GOB_Animations.MARCHER);
		indiceCheminActuel = indiceDernierPointRejoint;
		nav.enabled = true;
        suivreChemin();
		this.enChemin = true;
    }

    public override void faireEtat()
    {
		if (enChemin) {

			if(perception.aRepere(princesse, niveauAttentionEnMarche)) {
				changerEtat(this.GetComponent<GOB_E_Poursuivre>());
			}
			else if (agent.destinationCouranteAtteinte ()) {

				indiceDernierPointRejoint = indiceCheminActuel;
				setAnimation(GOB_Animations.CHERCHER);
				enChemin = false;
				this.delaisActuel = Time.time + this.delaisAChaqueArret;
				agent.getSoundEntity().playOneShot(sonArret, 1.0f);
			}
		} else if (Time.time > this.delaisActuel) {

			setAnimation(GOB_Animations.MARCHER);
			suivreChemin ();
			enChemin = true;

		} else if(perception.aRepere(princesse, niveauAttentionArret)) {
			changerEtat(this.GetComponent<GOB_E_Poursuivre>());
		}
    }

    public override void sortirEtat()
    {
		agent.getSoundEntity ().playOneShot (sonPoursuite);
    }

    private void suivreChemin()
	{
		indiceCheminActuel = (indiceCheminActuel + 1) % chemin.Length;
		agent.definirDestination(chemin[indiceCheminActuel].transform.position);
        nav.speed = vitesse;
    }
}
