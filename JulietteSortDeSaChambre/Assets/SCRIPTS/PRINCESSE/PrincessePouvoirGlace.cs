using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessePouvoirGlace : PrincessePouvoir
{
    private bool canPower;

    private SphereCollider sphereCollider;
    private ParticleSystem visuel;
    private AudioSource audioSource;
    //public float icePowerDuration;

    // Use this for initialization
    void Start()
    {
        //this.duration = icePowerDuration;
        //this.duration = 3f;
        this.Nom = "Pouvoir de la glace";
        this.Description = "Gèle les ennemis devant vous pendant " + duration + " secondes";
        sphereCollider = GetComponent<SphereCollider>();
        visuel=GameObject.Find("VisuelPouvoir").GetComponent<ParticleSystem>();
        audioSource=GameObject.Find("VisuelPouvoir").GetComponent<AudioSource>();
        visuel.Clear();
        visuel.Pause();
        canPower = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((InputManager.GetButtonDown("pouvoirGlace") || Input.GetAxis("pouvoirGlace")<-0.75f)&& canPower)
        {
            sphereCollider.enabled = true;
            canPower = false;
            visuel.Play();
            audioSource.Play();
            var visuPos = sphereCollider.transform;
            visuel.transform.position=visuPos.position+(visuPos.forward*2);
            visuel.transform.rotation=visuPos.rotation;
            StartCoroutine(WaitforIcePower());
            StartCoroutine(WaitforUseIcePower());
        }
    }

    IEnumerator WaitforIcePower()
    {
        yield return new WaitForSeconds(0.05f);
        sphereCollider.enabled = false;

    }

    IEnumerator WaitforUseIcePower()
    {
        yield return new WaitForSeconds(cooldown);
        canPower = true;

    }

    IEnumerator WaitforIcePowerVisual(){
        yield return new WaitForSeconds(duration);
        visuel.Clear();
    }

}
