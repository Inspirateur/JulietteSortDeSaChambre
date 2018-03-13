using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	private Animator anim;

    private AudioSource audioSource;

    [Header("Son des pics qui se préparent à sortir :")]
    public AudioClip Preparation;

    [Header("Son des pics qui sortent :")]
    public AudioClip Sortie;

    [Header("Son du rangement des pics :")]
    public AudioClip Ranger;

    [Header("Quel son sont actif pour ce pic :")]
    public bool SonPicPrepare = true;
    public bool SonPicSortie = true;
    public bool SonPicRange = true;

    [HideInInspector]
    public bool SonPicPrepareReplay;
    [HideInInspector]
    public bool SonPicSortieReplay;
    [HideInInspector]
    public bool SonPicRangeReplay;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        SonPicPrepareReplay = SonPicPrepare;
        SonPicSortieReplay = SonPicSortie;
        SonPicRangeReplay = SonPicRange;

		anim = gameObject.GetComponent<Animator> ();
	}

	void Update() {
        CheckAnimationToPlaySound();
    }

    private void CheckAnimationToPlaySound()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SpikesUp1") && SonPicPrepare && SonPicPrepareReplay)
        {
            SonPicPrepareReplay = false;
            audioSource.PlayOneShot(Preparation);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SpikesUp2") && SonPicSortie && SonPicSortieReplay)
        {
            SonPicSortieReplay = false;
            audioSource.PlayOneShot(Sortie);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SpikesDown") && SonPicRange && SonPicRangeReplay)
        {
            SonPicRangeReplay = false;
            audioSource.PlayOneShot(Ranger);
        }
    }
}

