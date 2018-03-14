using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpikes : MonoBehaviour {

    [Header("Pics GameObject :")]
    public GameObject[] Pics;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void StartAllSpikes()
    {
        for (int i = 0; i < Pics.Length; i++)
        {
            Pics[i].GetComponent<Spikes>().StartSpike();
        }
    }

    public void StopAllSpikes()
    {
        for (int i = 0; i < Pics.Length; i++)
        {
            Pics[i].GetComponent<Spikes>().StopSpike();
        }
    }
}
