using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	private Animator anim;

    [Range(0.1f, 5)]
    [Tooltip("multiplication de la vitesse de l'animation, pour une vitesse normale : 1")]
    public float SpeedMultiplier = 1f;

    private AudioSource audioSource;

    [Header("Temps de demarage :")]
    public float TimeBegin = 0;

    [Header("Temps de repos avant de remonter :")]
    public float TimeRepos = 4;

    [Header("Est-ce que le pic est activé :")]
    public bool Activated = true;

    private float ActivationTime;

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
        anim.SetBool("CanUp", false);
        anim.speed *= SpeedMultiplier;

        ActivationTime = Time.time + TimeRepos + TimeBegin;
    }

	void Update() {
        CheckAnimationToPlaySound();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("BeginRepos") && anim.GetBool("CanUp"))
        {
            SonPicPrepareReplay = true;
            SonPicSortieReplay = true;
            SonPicRangeReplay = true;
            anim.SetBool("CanUp", false);
            ActivationTime = Time.time + TimeRepos;
        }

        if (Activated && Time.time > ActivationTime)
        {
             anim.SetBool("CanUp", true);
        }
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

    public void StartSpike()
    {
        Activated = true;
    }

    public void StopSpike()
    {
        Activated = false;
    }
}

