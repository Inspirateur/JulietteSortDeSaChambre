using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_E_EtreGlace : IA_Etat {

	public float multiplicateurDureeGlace;
	private int degatsGlace;
	public Color colorGlace;
	public ParticleSystem iceSpawner;
	public Renderer rendererGobelin;

	private float timer;
	private Color[] listeInitialColors;
	private float dureeGlace;

	// Use this for initialization
	void Start()
	{
		base.init(); // permet d'initialiser l'état, ne pas l'oublier !

		// ne pas initialiser vos autres variables ici, utiliser plutôt la méthode entrerEtat()
		this.setInitialColors();
		this.degatsGlace = ((PrincessePouvoirGlace)GameObject.FindGameObjectWithTag("PouvoirGlace").GetComponent<PrincessePouvoirGlace>()).degats;
		this.dureeGlace = ((PrincessePouvoirGlace)GameObject.FindGameObjectWithTag("PouvoirGlace").GetComponent<PrincessePouvoirGlace>()).duration * multiplicateurDureeGlace;
	}

	public override void entrerEtat()
	{
		this.nav.enabled = false;
		this.anim.enabled = false;
		this.addEffetGlace();
		this.timer = Time.time + this.dureeGlace;
	}

	public override void faireEtat()
	{
		if(Time.time >= this.timer){
			agent.subirDegats(this.degatsGlace, agent.transform.position);
		}
	}

	public override void sortirEtat()
	{
		this.removeEffetGlace();
		this.anim.enabled = true;
	}

	private void addEffetGlace(){

        foreach (Material mat in rendererGobelin.materials)
        {
            mat.SetColor("_Color", this.colorGlace);
        }
		this.iceSpawner.gameObject.SetActive(true);
		this.iceSpawner.Play();
    }

    private void removeEffetGlace(){

		int i=0;

        foreach (Material mat in rendererGobelin.materials)
        {
            mat.SetColor("_Color", this.listeInitialColors[i++]);
        }

		this.iceSpawner.gameObject.SetActive(false);
    }

	private void setInitialColors(){

		this.listeInitialColors = new Color[rendererGobelin.materials.Length];
		int i=0;

        foreach (Material mat in rendererGobelin.materials)
        {
            this.listeInitialColors[i++] = mat.GetColor("_Color");
        }
	}
}
