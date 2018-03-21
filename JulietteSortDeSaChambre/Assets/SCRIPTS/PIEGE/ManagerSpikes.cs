using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpikes : Evenement {

	private Spikes[] Pics;

    // Use this for initialization
    void Start () {
		Pics = GetComponentsInChildren<Spikes> ();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void StartAllSpikes()
    {
		foreach(Spikes pic in Pics)
		{
			pic.StartSpike();
		}
    }

    public void StopAllSpikes()
    {
		foreach(Spikes pic in Pics)
		{
			pic.StopSpike();
		}
    }
}
