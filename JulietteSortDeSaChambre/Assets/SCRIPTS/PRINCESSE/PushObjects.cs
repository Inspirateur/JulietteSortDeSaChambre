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
    private float distance;
    private PrincesseDeplacement deplacement;
    void Start()
    {
        princesseAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        princesse = GameObject.FindGameObjectWithTag("Player");
        deplacement = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>(); 
        activate=false;
        CollisionCount = 0;

    }

    	public override void Activation(){

            if(!activate && !princesseAnimator.GetBool("IsJumping"))
            {    
                
                float angleright = Vector3.Angle(this.transform.right, new Vector3(princesse.transform.position.x - this.transform.position.x, princesse.transform.position.y,princesse.transform.position.z - this.transform.position.z ));
                float anglemoinsright = Vector3.Angle(-this.transform.right, new Vector3(princesse.transform.position.x - this.transform.position.x, princesse.transform.position.y,princesse.transform.position.z - this.transform.position.z ));
                float angleforward = Vector3.Angle(this.transform.forward, new Vector3(princesse.transform.position.x - this.transform.position.x, princesse.transform.position.y,princesse.transform.position.z - this.transform.position.z ));
                float anglemoinsforward = Vector3.Angle(-this.transform.forward, new Vector3(princesse.transform.position.x - this.transform.position.x, princesse.transform.position.y,princesse.transform.position.z - this.transform.position.z ));

                if(angleright < 45){
                    princesse.transform.position = new Vector3(this.transform.position.x + GetComponent<Renderer>().bounds.size.x/2 +1f, princesse.transform.position.y, this.transform.position.z );
                    princesse.transform.LookAt(this.transform);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                    Debug.Log(this.transform.forward);
                    this.transform.parent = princesse.transform;
                    deplacement.canMove = false;
                    princesseAnimator.Play("idle1");
                    deplacement.gererAnim("PushIdle");
                    deplacement.canMove = true;
            
                }

                if(anglemoinsright < 45){
                    princesse.transform.position = new Vector3(this.transform.position.x - GetComponent<Renderer>().bounds.size.x/2 - 1f , princesse.transform.position.y, this.transform.position.z );
                    princesse.transform.LookAt(this.transform);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                    this.transform.parent = princesse.transform;
                    deplacement.canMove = false;
                    princesseAnimator.Play("idle1");
                    deplacement.gererAnim("PushIdle");
                    deplacement.canMove = true;
                    
                }

                if(angleforward < 45){
                    princesse.transform.position = new Vector3(this.transform.position.x, princesse.transform.position.y, this.transform.position.z + GetComponent<Renderer>().bounds.size.z/2 +1);
                    princesse.transform.LookAt(this.transform);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                    this.transform.parent = princesse.transform;
                    deplacement.canMove = false;
                    princesseAnimator.Play("idle1");
                    deplacement.gererAnim("PushIdle");
                    deplacement.canMove = true;
                
                }

                if(anglemoinsforward < 45){
                    princesse.transform.position = new Vector3(this.transform.position.x, princesse.transform.position.y, this.transform.position.z - GetComponent<Renderer>().bounds.size.z/2 -1);
                    princesse.transform.LookAt(this.transform);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                    this.transform.parent = princesse.transform;
                    deplacement.canMove = false;
                    princesseAnimator.Play("idle1");
                    deplacement.gererAnim("PushIdle");
                    deplacement.canMove = true;
                    
                }
                distance = Vector3.Distance(princesse.transform.position,this.transform.position);
                activate=true;

            }
            else if(activate)
            {
                this.transform.parent = null;
                deplacement.gererAnim("IsIdle");
                activate=false;
            }


        }

        void Update()
        {
            if(activate){
                float disttemp =  Vector3.Distance(princesse.transform.position,this.transform.position);
              //  Debug.Log(disttemp);
              //  Debug.Log(distance);
             if(disttemp != distance)
                this.transform.parent = null;
                princesse.transform.position = (princesse.transform.position - this.transform.position).normalized * distance + this.transform.position;
                this.transform.parent = princesse.transform;
            }
        }

  
}
