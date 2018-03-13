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

    [Header("Temps avant la premiere action :")]
	public float TimeBeforeStart;

	[Header("Temps entre chaque action :")]
	public float TimeRepos;

	private bool StopSpike;
	private bool BeginStopSpike;
	private bool CheckCall;

    [Header("Quel son sont actif pour ce pic :")]
    public bool SonPicPrepare = true;
    public bool SonPicSortie = true;
    public bool SonPicRange = true;

    private bool SonPicPrepareReplay;
    private bool SonPicSortieReplay;
    private bool SonPicRangeReplay;

    [Range(0.1f, 5)]
	[Tooltip("multiplication de la vitesse de l'animation, pour une vitesse normale : 1")]
	public float SpeedMultiplier = 1f;

	// Use this for initialization
	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        SonPicPrepareReplay = SonPicPrepare;
        SonPicSortieReplay = SonPicSortie;
        SonPicRangeReplay = SonPicRange;

        CheckCall = true;
		StopSpike = false;
		BeginStopSpike = false;
        StartCoroutine(WaitBeforeStart());
		anim = gameObject.GetComponent<Animator> ();
		anim.speed = anim.speed * SpeedMultiplier;
	}

	void Update() {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("BeginRepos") && !StopSpike && CheckCall) {
			CheckCall = false;
			StartCoroutine (WaitBeforeUp ());
		}
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

	public void StopSpikes() {
		BeginStopSpike = true;
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("BeginRepos"))
		{
			anim.SetBool ("CanUp", false);
			StopSpike = true;
			BeginStopSpike = false;
		} else {
			Invoke("StopSpikes",0f);
		}	
	}

	void StartSpikes() {
		if (!BeginStopSpike)
		{
			StopSpike = false;
		}
	}

	IEnumerator WaitBeforeStart()
	{
		yield return new WaitForSeconds(TimeBeforeStart);
		anim.SetBool ("CanUp", true);
	}

	IEnumerator WaitBeforeUp()
	{
		anim.SetBool ("CanUp", false);
		yield return new WaitForSeconds(TimeRepos);
        SonPicPrepareReplay = true;
        SonPicSortieReplay = true;
        SonPicRangeReplay = true;
        anim.SetBool ("CanUp", true);
		CheckCall = true;
	}
}

