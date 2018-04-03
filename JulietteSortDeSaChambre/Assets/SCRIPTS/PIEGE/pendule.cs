using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendule : MonoBehaviour {

	[Header("Amplitude de l'oscillation :")]
	[Range(0,90)]
	public int amplitude = 80;

	[Header("Temps de demarage :")]
    [Range(0, (2*Mathf.PI))]
    public float TimeBegin = 0;

	[Header("Vitesse :")]
    [Range(0, 5)]
    public float speed = 2f;

    [Header("Vitesse remonté mécanique :")]
    [Range(1, 10)]
    public float speedMecanique = 5f;

    [Header("La lame peut s'arreter :")]
    public bool canStop = true;

    [Header("Est-il arrété ? :")]
    public bool stop = false;

    [Header("Sparkle de la lame :")]
    public GameObject sparkle;

    [Header("Remonté sciptée ? :")]
    public bool ArretScripte = false;

    private float time;

    private AudioSource audioSource;

    void Awake () {
        audioSource = GetComponentInChildren<AudioSource> ();
        time = TimeBegin;
    }

	void Update (){
        if (!stop) {
            time += Time.deltaTime;
            transform.Rotate(new Vector3(0, 0, 1), amplitude * Mathf.Cos(speed * time) - transform.rotation.eulerAngles.z);
        } else {
            if (ArretScripte) {
                if (transform.rotation.eulerAngles.z < amplitude || transform.rotation.eulerAngles.z > (amplitude+speedMecanique)) {
                    transform.Rotate(new Vector3(0, 0, 1), (speedMecanique * Time.deltaTime)%360);
                }
            } else {
                if (transform.rotation.eulerAngles.z < amplitude) {
                    transform.Rotate(new Vector3(0, 0, 1), speedMecanique * Time.deltaTime);
                } else if (transform.rotation.eulerAngles.z > (360 - amplitude)) {
                    transform.Rotate(new Vector3(0, 0, 1), -speedMecanique * Time.deltaTime);
                }
            }
        }
	}

    public void StopPendule() {
        audioSource.Stop();
		stop = canStop;
	}

	public void StartPendule() {
        audioSource.Play();
        time = TimeBegin;
        stop = false;
	}

    public void setTime(float time){
        this.time = time;
    }

    public float getTime(){
        return this.time;
    }

    public void StartSpikle() {
        sparkle.SetActive(true);
    }
}
