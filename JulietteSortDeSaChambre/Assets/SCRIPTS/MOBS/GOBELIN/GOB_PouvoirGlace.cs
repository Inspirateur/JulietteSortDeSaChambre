using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOB_PouvoirGlace : MonoBehaviour {
    private float icePowerDuration;

	// Use this for initialization
	void Start () {
        icePowerDuration = GameObject.FindGameObjectWithTag("PouvoirGlaceON").GetComponent<PrincessePouvoir>().icePowerDuration;
        Debug.Log(icePowerDuration);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PouvoirGlaceON")
        {
            Debug.Log("gobelin touche");
            StartCoroutine(WaitforIcePower());
        }
    }

    IEnumerator WaitforIcePower()
    {
        yield return new WaitForSeconds(icePowerDuration);
        Debug.Log("Gobelin libere de la glace");
    }

}
