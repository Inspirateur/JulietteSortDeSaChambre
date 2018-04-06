using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PushObjects : ObjetEnvironnemental {
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
    private Vector3 pointplacement;
    private float time;
    private Rigidbody rb;
    private Vector3 position;
    private Vector3 temp;
    private float x;
    private float y;
    private float z;
    private bool atposition;

    void Start()
    {
        princesseAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        princesse = GameObject.FindGameObjectWithTag("Player");
        deplacement = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>(); 
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        activate=false;
        CollisionCount = 0;

    }

    	public override void Activation(){

            if(!princesseAnimator.GetBool("PushIdle") && !princesseAnimator.GetBool("isPushing") && !princesseAnimator.GetBool("IsJumping") && !activate )
            {
                float angleright = Vector3.Angle(this.transform.right, new Vector3(princesse.transform.position.x - this.transform.position.x, princesse.transform.position.y,princesse.transform.position.z - this.transform.position.z ));
                float anglemoinsright = Vector3.Angle(-this.transform.right, new Vector3(princesse.transform.position.x - this.transform.position.x, princesse.transform.position.y,princesse.transform.position.z - this.transform.position.z ));
                float angleforward = Vector3.Angle(this.transform.forward, new Vector3(princesse.transform.position.x - this.transform.position.x, princesse.transform.position.y,princesse.transform.position.z - this.transform.position.z ));
                float anglemoinsforward = Vector3.Angle(-this.transform.forward, new Vector3(princesse.transform.position.x - this.transform.position.x, princesse.transform.position.y,princesse.transform.position.z - this.transform.position.z ));

                if(angleright < 45){
                    deplacement.canMove = false;
                    pointplacement = new Vector3(this.transform.position.x + GetComponent<Renderer>().bounds.size.x/2 +1f, princesse.transform.position.y, this.transform.position.z );
                    princesse.transform.LookAt(pointplacement);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                    time = Time.time + 2f;
                    position = rb.position;
                    temp = pointplacement - position;
                    x = temp.x / (time - Time.time);
                    y = temp.y / (time - Time.time);
                    z = temp.z / (time - Time.time);
                    
                }

                if(anglemoinsright < 45){
                    deplacement.canMove = false;
                     pointplacement  = new Vector3(this.transform.position.x - GetComponent<Renderer>().bounds.size.x/2 - 1f , princesse.transform.position.y, this.transform.position.z );
                     princesse.transform.LookAt(pointplacement);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                    time = Time.time + 2f;
                    position = rb.position;
                    temp = pointplacement - position;
                    x = temp.x / (time - Time.time);
                    y = temp.y / (time - Time.time);
                    z = temp.z / (time - Time.time);
                }

                if(angleforward < 45){
                    deplacement.canMove = false;
                     pointplacement  = new Vector3(this.transform.position.x, princesse.transform.position.y, this.transform.position.z + GetComponent<Renderer>().bounds.size.z/2 +1);
                  princesse.transform.LookAt(pointplacement);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                    time = Time.time + 2f;
                    position = rb.position;
                    temp = pointplacement - position;
                    x = temp.x / (time - Time.time);
                    y = temp.y / (time - Time.time);
                    z = temp.z / (time - Time.time);
                }

                if(anglemoinsforward < 45){
                    deplacement.canMove = false;
                   pointplacement  =  new Vector3(this.transform.position.x, princesse.transform.position.y, this.transform.position.z - GetComponent<Renderer>().bounds.size.z/2 -1);
                     princesse.transform.LookAt(pointplacement);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                    time = Time.time + 2f;
                    position = rb.position;
                    temp = pointplacement - position;
                    x = temp.x / (time - Time.time);
                    y = temp.y / (time - Time.time);
                    z = temp.z / (time - Time.time);
            }

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

            if(time > Time.time && !princesseAnimator.GetBool("PushIdle") && !princesseAnimator.GetBool("isPushing") && activate){
                                Debug.Log("je passe ici");

                temppos = new Vector3(rb.position.x + x * Time.deltaTime, rb.position.y + y * Time.deltaTime, rb.position.z + z * Time.deltaTime);
                rb.MovePosition(temppos);
            }

                
    
  

            if(time < Time.time && !princesseAnimator.GetBool("PushIdle") && !princesseAnimator.GetBool("isPushing") && activate){
                princesse.transform.LookAt(this.transform);
                    princessetemp = princesse.transform.forward;
                    princessetemp.y = 0;
                    princesse.transform.forward = princessetemp;
                  //  Debug.Log(this.transform.forward);
                    this.transform.parent = princesse.transform;
                    princesseAnimator.Play("idle1");
                    deplacement.gererAnim("PushIdle");
                    deplacement.canMove = true;
                    activate = true;
            
            }

            if(activate){
                float disttemp =  Vector3.Distance(princesse.transform.position,this.transform.position);
                distance = Vector3.Distance(princesse.transform.position,this.transform.position);
              //  Debug.Log(disttemp);
              //  Debug.Log(distance);
             if(disttemp != distance){
                this.transform.parent = null;
                princesse.transform.position = (princesse.transform.position - this.transform.position).normalized * distance + this.transform.position;
                this.transform.parent = princesse.transform;
            }
        }

  
}

}