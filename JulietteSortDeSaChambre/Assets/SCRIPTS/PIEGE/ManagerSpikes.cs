using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpikes : MonoBehaviour {

    [Header("Pics GameObject :")]
    public GameObject[] Pics;

    private Animator[] anim;

    [Range(0.1f, 5)]
    [Tooltip("multiplication de la vitesse de l'animation, pour une vitesse normale : 1")]
    public float SpeedMultiplier = 1f;

    [Header("Temps de démarage tous le même temps actif :")]
    public bool TimeStartAllActive = true;

    [Header("Temps avant la premiere action (pour tout les pics) :")]
    public float TimeBeforeStartAll = 0.4f;

    [Header("Temps avant la premiere action (pour un seul pics) :")]
    public float[] TimeBeforeStartOne;

    [Header("Temps de repos tous le même temps actif :")]
    public bool TimeReposAllActive = true;

    [Header("Temps entre chaque action (pour tout les pics) :")]
    public float TimeReposAll = 4;

    [Header("Temps entre chaque action (pour un seul pics) :")]
    public float[] TimeReposOne;

    private bool[] StopSpike;
    private bool[] BeginStopSpike;
    private bool[] CheckCall;

    private int TailleNombrePicsEnregistrer;

    // Use this for initialization
    void Start () {
        TailleNombrePicsEnregistrer = Pics.Length;

        anim = new Animator[TailleNombrePicsEnregistrer];
        TimeBeforeStartOne = new float[TailleNombrePicsEnregistrer];
        TimeReposOne = new float[TailleNombrePicsEnregistrer];
        StopSpike = new bool[TailleNombrePicsEnregistrer];
        BeginStopSpike = new bool[TailleNombrePicsEnregistrer];
        CheckCall = new bool[TailleNombrePicsEnregistrer];

        int TailleTableauPics = Pics.Length;
        for (int i = 0; i < TailleTableauPics; i++)
        {
            BeginStopSpike[i] = false;
            StopSpike[i] = false;
            CheckCall[i] = true;
            anim[i] = Pics[i].GetComponent<Animator>();
            anim[i].speed = anim[i].speed * SpeedMultiplier;
        }

        StartCoroutine(WaitBeforeStart(0));
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < TailleNombrePicsEnregistrer; i++)
        {
            if (anim[i].GetCurrentAnimatorStateInfo(0).IsName("BeginRepos") && !StopSpike[i] && CheckCall[i])
            {
                CheckCall[i] = false;
                StartCoroutine(WaitBeforeUp(i));
            }
        }
    }

    public void SetSpeedAnimationOneSpikes(int Number, float speedNew)
    {
        anim[Number].speed = speedNew;
    }

    public void SetSpeedAnimationAllSpikes(float speedNew)
    {
        for (int i = 0; i < TailleNombrePicsEnregistrer; i++)
        {
            anim[i].speed = speedNew;
        }
    }

    IEnumerator WaitBeforeStart(int Number)
    {
        yield return new WaitForSeconds((TimeStartAllActive)? TimeBeforeStartAll : TimeBeforeStartOne[Number]);
        anim[Number].SetBool("CanUp", true);
        Number++;
        if (Number < TailleNombrePicsEnregistrer)
        {
            StartCoroutine(WaitBeforeStart(Number));
        }
    }

    IEnumerator WaitBeforeUp(int Number)
    {
        anim[Number].SetBool("CanUp", false);
        yield return new WaitForSeconds((TimeReposAllActive)? TimeReposAll : TimeReposOne[Number]);
        Pics[Number].GetComponent<Spikes>().SonPicPrepareReplay = true;
        Pics[Number].GetComponent<Spikes>().SonPicSortieReplay = true;
        Pics[Number].GetComponent<Spikes>().SonPicRangeReplay = true;
        anim[Number].SetBool("CanUp", true);
        CheckCall[Number] = true;
    }

    void StartAllSpikes()
    {
        int NumbreRestantPicsStart = 0;
        for (int i = 0; i < TailleNombrePicsEnregistrer; i++)
        {
            if (!BeginStopSpike[i])
            {
                StopSpike[i] = false;
            }
            else
            {
                NumbreRestantPicsStart++;
            }
        }

        if (NumbreRestantPicsStart != 0)
        {
            Invoke("StartAllSpikes", 0f);
        }
    }

    IEnumerator StartOneSpike(int Number)
    {
        yield return new WaitForSeconds(0);
        if (!BeginStopSpike[Number])
        {
            StopSpike[Number] = false;
        } else
        {
            StartCoroutine(StartOneSpike(Number));
        }
    }

    public void BeginStopAllSpikes()
    {
        for (int i = 0; i < TailleNombrePicsEnregistrer; i++)
        {
            BeginStopSpike[i] = true;
        }
        StopAllSpikes();
    }

    public void StopAllSpikes()
    {
        int NumbreRestantPicsStop = 0;
        for (int i = 0; i < TailleNombrePicsEnregistrer; i++)
        {
            if (anim[i].GetCurrentAnimatorStateInfo(0).IsName("BeginRepos") && BeginStopSpike[i])
            {
                anim[i].SetBool("CanUp", false);
                StopSpike[i] = true;
                BeginStopSpike[i] = false;
            } else
            {
                NumbreRestantPicsStop++;
            }
        }

        if (NumbreRestantPicsStop != 0)
        {
            Invoke("StopAllSpikes", 0f);
        }
    }

    IEnumerator StopOneSpikes(int Number)
    {
        yield return new WaitForSeconds(0);
        BeginStopSpike[Number] = true;
        if (anim[Number].GetCurrentAnimatorStateInfo(0).IsName("BeginRepos"))
        {
            anim[Number].SetBool("CanUp", false);
            StopSpike[Number] = true;
            BeginStopSpike[Number] = false;
        }
        else
        {
            StartCoroutine(StopOneSpikes(Number));
        }
    }
}
