using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjetInteractifs : MonoBehaviour {

	public string nomObjet;
	public string descriptionObjet;

    [HideInInspector]
    public SoundEntity soundGenerator;	

    [HideInInspector]
    public SoundManager sm;

    public AudioClip RamasseObjet;


    void Awake () 
    {
        sm = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager>();
    }

	// Use this for initialization
	void Start () {
		        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public abstract void Activation();

	public virtual EnumIconeInterraction getIconeInteraction(){
		return EnumIconeInterraction.icone_default;
	}


	public void supprimerObjet(){
		this.gameObject.SetActive (false);
	}


    public void AddSurbrillance()
    {
        Renderer rend;
        rend = GetComponent<Renderer>();

        Material[] materials;
        materials = rend.materials;

        foreach (Material mat in materials)
        {
            mat.SetColor("_Color", Color.white);
        }
    }

    public void removeSurbrillance()
    {
        Renderer rend;
        rend = GetComponent<Renderer>();

        Material[] materials;
        materials = rend.materials;

        foreach (Material mat in materials)
        {
            mat.SetColor("_Color", Color.grey);
        }
    }
}
