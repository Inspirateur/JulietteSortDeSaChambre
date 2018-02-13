using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_PouvoirGlace : MonoBehaviour {
    private float icePowerDuration;
    private bool isIced;

	// Use this for initialization
	void Start () {
        icePowerDuration = GameObject.FindGameObjectWithTag("PouvoirGlace").GetComponent<PrincessePouvoirGlace>().duration;
        isIced = false;
        Debug.Log(icePowerDuration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PouvoirGlace" && !isIced)
        {
            Debug.Log("gobelin touche");
            isIced = true;
            StartCoroutine(WaitforIcePower());
        }
    }

    IEnumerator WaitforIcePower()
    {
        yield return new WaitForSeconds(icePowerDuration);
        Debug.Log("Gobelin libere de la glace");
    }

}
