using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessePouvoirGlace : PrincessePouvoir
{
    public bool canPower;
    public float delay;

    private SphereCollider sphereCollider;
    private ParticleSystem visuel;
    private AudioSource audioSource;
    private GlaceSol glaceSol;
    //public float icePowerDuration;

    private List<IA_Agent> listeAgentGlaces;
    public int degats;

    private Animator animator;

    // Use this for initialization
    void Start()
    {
        //this.duration = icePowerDuration;
        //this.duration = 3f;
        this.Nom = "Pouvoir de la glace";
        this.Description = "Gèle les ennemis devant vous pendant " + duration + " secondes";
        animator=GameObject.Find("Juliette").GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        visuel=GameObject.Find("VisuelPouvoir").GetComponent<ParticleSystem>();
        audioSource=GameObject.Find("VisuelPouvoir").GetComponent<AudioSource>();
        glaceSol=GameObject.Find("GlaceSol").GetComponent<GlaceSol>();
        //glaceSol.SetActive(false);
        visuel.Clear();
        visuel.Pause();
        canPower = true;
        listeAgentGlaces = new List<IA_Agent>();
        Debug.Log(duration);
    }

    // Update is called once per frame
    void Update()
    {
        if ((InputManager.GetButtonDown("pouvoirGlace") || Input.GetAxis("pouvoirGlace")<-0.75f)&& canPower)
        {
            animator.Play("IcePower");
            StartCoroutine(WaitForIceAnim());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mob")
        {
            IA_Agent mobTouche = other.gameObject.GetComponent<IA_Agent> ();

            if (!listeAgentGlaces.Contains (mobTouche) && mobTouche.estEnVie()) {
                
                listeAgentGlaces.Add (mobTouche);

                Vector3 hitPoint = other.ClosestPoint (this.transform.position);

                mobTouche.subirDegats (degats, hitPoint, EnumEffet.GLACER);
            }
        }
    }

    private void usePower(){
         listeAgentGlaces.Clear();
	        sphereCollider.enabled = true;
	        canPower = false;
	        visuel.Play();
	        audioSource.Play();
	        var visuPos = sphereCollider.transform;
	        visuel.transform.position=visuPos.position+(visuPos.forward*2);
	        visuel.transform.rotation=visuPos.rotation;
	        glaceSol.LaunchAnim();
	        StartCoroutine(WaitforIcePower());
	        StartCoroutine(WaitforUseIcePower());
	        StartCoroutine(WaitforIcePowerVisual());
    }

    IEnumerator WaitforIcePower()
    {
        yield return new WaitForSeconds(0.05f);
        sphereCollider.enabled = false;
        Debug.Log("reset collider");
    }

    IEnumerator WaitforUseIcePower()
    {
        yield return new WaitForSeconds(cooldown);
        canPower = true;
        Debug.Log("CD pouvoir glace");
    }

    IEnumerator WaitforIcePowerVisual(){
        yield return new WaitForSeconds(duration);
        visuel.Clear();
    }

    IEnumerator WaitForIceAnim(){
        yield return new WaitForSeconds(delay);
        usePower();
    }

}
