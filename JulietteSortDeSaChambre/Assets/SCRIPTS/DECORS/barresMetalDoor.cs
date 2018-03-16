using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barresMetalDoor : MonoBehaviour {

	public bool CanDown;
	public float speed;
	private float StartYpos;
	public float DistanceToMove;

    private AudioSource audioSource;

    public AudioClip BarreMecanisme;
    private bool LanceSon = false;

	void Start(){
		StartYpos = this.gameObject.transform.position.y;
        audioSource = GetComponent<AudioSource>();
    }

	void Update() {
		if (CanDown) {
			if (gameObject.transform.position.y >= StartYpos - DistanceToMove) {
				this.gameObject.transform.Translate (Vector3.back * speed * Time.deltaTime);
                if (!LanceSon)
                {
                    audioSource.PlayOneShot(BarreMecanisme);
                    LanceSon = true;
                }
            } else {
				CanDown = false;
			}
		}
	}


}
