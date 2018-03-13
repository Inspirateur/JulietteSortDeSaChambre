using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class PushObjects : ObjetEnvironnemental {
    public AudioClip soundClip;
    public float ObMass = 300;
    public float PushAtMass = 100;
    public float PushingTime;
    public float ForceToPush;
    Rigidbody rb;
    public float vel;
    private GameObject princesse;

    private Animator princesseAnimator;
    AudioSource AudioPlayer;
    Vector3 dir;
    private int CollisionCount;
    private bool activate;

    Vector3 lastPos ;
    float _PushingTime =0;

    private Vector3 princessetemp;

    private Vector3 sizetemp;

    private Vector3 princessepostemp;
    private Vector3 temppos;
    


    //For setup thing please watch the Demo 
    //https://www.youtube.com/watch?v=VB9rb5fN97I
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null) return;

        AudioPlayer = GetComponent<AudioSource>();
        if (soundClip != null)
        {           
            AudioPlayer.clip = soundClip;
            AudioPlayer.Stop();
        }
        AudioPlayer.volume = 0;
        AudioPlayer.pitch = 0.5f;
        rb.mass = ObMass;
        princesseAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        princesse = GameObject.FindGameObjectWithTag("Player");
        activate=false;
        CollisionCount = 0;

    }

    bool IsMoving()
    {
        if (rb.velocity.magnitude > 0.06f)
        {
            return true;
        }
        return false;

    }

    	public override void Activation(){

                Vector3 targetdir = this.transform.position - princesse.transform.position;
                float angleforward = Vector3.Angle(targetdir, this.transform.forward);
                Debug.Log(angleforward);
                float anglemoinsforward = Vector3.Angle(targetdir, -this.transform.forward);
                Debug.Log(anglemoinsforward);
                float angledroite = Vector3.Angle(targetdir, this.transform.right);
                Debug.Log(angledroite);
                float anglemoinsdroite = Vector3.Angle(targetdir, -this.transform.right);
                Debug.Log(anglemoinsdroite);
           /* if(!activate)
            {
                princesseAnimator.SetBool("isPushing", true);
            // Debug.Log(princessetemp.position.y);
                princesse.transform.LookAt(this.transform);
                princessetemp = princesse.transform.forward;
                princessetemp.y = 0;
                princesse.transform.forward = princessetemp;
                activate=true;
                rb.isKinematic = true;
                   /* if (soundClip != null)
                    {
                        AudioPlayer.Stop();
                    }
                AudioPlayer.volume = 0f;
                AudioPlayer.pitch = 0.2f;
                }
            else{
                activate=false;
            }*/
        }

     private void Update()
    {
        //F key to Push
        vel = rb.velocity.magnitude;
       /* if (InputManager.GetKeyDown(KeyCode.F))
        {
            
        }*/

        if (rb.isKinematic==false)
        {
            _PushingTime += Time.deltaTime;
            if (_PushingTime >= PushingTime)
            {
                _PushingTime = PushingTime;
            }

            rb.mass = Mathf.Lerp(ObMass, PushAtMass, _PushingTime / PushingTime);
           // rb.AddForce(dir * ForceToPush, ForceMode.Force);
        }
        else
        {
            rb.mass = ObMass;
            _PushingTime = 0;
           
        }

        if (IsMoving() == true && rb.isKinematic == false)
        {
            if (AudioPlayer.isPlaying == false)
            {
                AudioPlayer.Play();
            }

           StartCoroutine( SoundChangeHigh());
        }
        else
        {
            StartCoroutine(SoundChangeLow());
        }

        if(activate==true){ 
           if(CollisionCount == 0){
               /* princesse.transform.Translate(Vector3.forward * Time.deltaTime * 5);
                sizetemp=princesse.GetComponent<BoxCollider>().size;
                sizetemp.z = 2.3f; 
                princesse.GetComponent<BoxCollider>().size = sizetemp;
                princessepostemp = princesse.transform.position;*/
                
           }
           else if(CollisionCount > 0){
               Debug.Log("je passe ici");
              this.transform.position = princesse.transform.position + temppos;
           }
        }
        
    }

    private void FixedUpdate(){

    }



    private void OnCollisionEnter(Collision collision)
    {
       

        if (collision.collider.tag == "Player")
                {
   
                CollisionCount++;
                rb.isKinematic = false;
                temppos = this.transform.position - princesse.transform.position;
                // - position entre caisse et princesse
                //
                dir = princesse.transform.forward;
                /*dir = collision.contacts[0].point - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;*/
                Debug.Log("COLLIDER");
         
        }

    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
                {
                    CollisionCount--;
                   // Debug.Log(CollisionCount);
                               // activate = false;
                }

    }


    IEnumerator SoundChangeHigh()
    {
        if (Input.GetKey(KeyCode.F))
        {
            AudioPlayer.volume = Mathf.Lerp(0, 0.5f, PushAtMass / rb.mass);
            AudioPlayer.pitch = Mathf.Lerp(0.2f, 1f, PushAtMass / rb.mass);
        }
        yield return new WaitForSeconds(0.1f);

    }
    IEnumerator SoundChangeLow()
    {
        if (Input.GetKey(KeyCode.F))
        {
            AudioPlayer.volume =1- Mathf.Lerp(0F, 0.5f, Time.deltaTime);
            AudioPlayer.pitch = 1- Mathf.Lerp(0.2f, 1f, Time.deltaTime);
        }

        yield return new WaitForSeconds(0.1f);
    }
  
}
