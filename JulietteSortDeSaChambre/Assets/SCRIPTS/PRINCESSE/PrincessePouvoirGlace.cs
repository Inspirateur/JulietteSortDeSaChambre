using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessePouvoirGlace : PrincessePouvoir
{
    private bool canPower;

    private SphereCollider sphereCollider;
    //public float icePowerDuration;

    // Use this for initialization
    void Start()
    {
        //this.duration = icePowerDuration;
        //this.duration = 3f;
        this.Nom = "Pouvoir de la glace";
        this.Description = "Gèle les ennemis devant vous pendant " + duration + " secondes";
        sphereCollider = GetComponent<SphereCollider>();
        canPower = true;
    }

    // Update is called once per frame
    void Update()
    {
        if ((InputManager.GetButtonDown("pouvoirGlace") || Input.GetAxis("pouvoirGlace")<-0.75f)&& canPower)
        {
            sphereCollider.enabled = true;
            canPower = false;
            StartCoroutine(WaitforIcePower());
            StartCoroutine();
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

}
