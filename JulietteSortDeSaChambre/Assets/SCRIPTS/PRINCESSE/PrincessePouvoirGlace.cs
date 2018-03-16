using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessePouvoirGlace : PrincessePouvoir
{
    [Header("Pouvoir")]
    public bool canPower;
    public float delay;


    [Header("Animation")]
    public GameObject prefab;

    public int nbFrameMax;
    public float increment;

    public Vector3 deplacementGlacon;
    private SphereCollider sphereCollider;
    private ParticleSystem visuel;
    private GlaceSol glaceSol;

    private GameObject clone;
    //public float icePowerDuration;

    private List<IA_Agent> listeAgentGlaces;
    public int degats;

    private Animator animator;


    //ex script glace sol
    private bool running;
    private int compteurFrame;

    // Use this for initialization
    void Start()
    {
        //this.duration = icePowerDuration;
        //this.duration = 3f;
        this.Nom = "Pouvoir de la glace";
        this.Description = "Gèle les ennemis devant vous pendant " + duration + " secondes";
        animator=GameObject.Find("Juliette").GetComponent<Animator>();
        sphereCollider = GetComponent<SphereCollider>();
        compteurFrame=0;
        running=false;
        canPower = true;
        listeAgentGlaces = new List<IA_Agent>();
//        Debug.Log(duration);
    }

    // Update is called once per frame
    void Update()
    {
        if ((InputManager.GetButtonDown("pouvoirGlace") || Input.GetAxisRaw("pouvoirGlace")<-0.75f)&& canPower)
        {
            animator.Play("IcePower");
            canPower=false;
            StartCoroutine(WaitForIceAnim());
        }

        if(running){
            var tmp = glaceSol.transform.position;
            Debug.Log(tmp.ToString());
			compteurFrame++;
			
			tmp.y+=increment;
			tmp+=(increment*deplacementGlacon);
			Debug.Log(tmp.y);
			if(compteurFrame >= nbFrameMax){
				running=false;
				//Add song explosion des glacons 
				//+ anim destruction ?? 
			}else{
				glaceSol.transform.position=tmp;
			}
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
        running=true;
        var visuPos = sphereCollider.GetComponent<Transform>();
        clone = Instantiate<GameObject>(prefab,visuPos.transform.position+visuPos.forward*2,visuPos.transform.rotation);
        glaceSol=clone.GetComponentInChildren<GlaceSol>();
        Destroy(clone,duration);
        clone.GetComponent<AudioSource>().Play();
        clone.GetComponent<ParticleSystem>().Play();
        //visuel.Play();
        //audioSource.Play();
        // var visuPos = sphereCollider.transform;
        // visuel.transform.position=visuPos.position+(visuPos.forward*2);
        // visuel.transform.rotation=visuPos.rotation;
        // glaceSol.LaunchAnim();
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
        compteurFrame=0;
        Destroy(clone);
        clone=null;
        Debug.Log("Destroy instatiate");
    }

    IEnumerator WaitForIceAnim(){
        yield return new WaitForSeconds(delay);
        usePower();
    }

}
