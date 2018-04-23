using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincesseDeplacement : MonoBehaviour
{
    static Animator anim;
    public float vitesse;
    public float forceSaut;
    public float vitesseAngulaire;
    public bool isGrounded;
    public float feetDist = 0.1f;
    public AudioClip[] bruitsPas;
    public float minPitch;
    public float maxPitch;
    public float minVolume;
    public float maxVolume;
    public BruiteurPas bruiteurPas;
    public float forceDash;

    private GameObject cam;
    private bool CanDash;
    private Rigidbody rb;
    private bool isPushing;
    private PrincesseArme princesseArme;
    private GameObject pushableCube;
    private float timerStep;
    private SoundManager sm;
    private float timer;
    private bool attackjump;
    private bool isCharging;

    private PrincessePouvoirGlace ppg;

    [HideInInspector]
    public bool attaqueBegin;

	public bool canMove;
	[HideInInspector]
	public bool canJump;
	[HideInInspector]
	public bool canAttack;
	[HideInInspector]
	public bool canUsePower;
	[HideInInspector]
	public bool canOpenInventory;

	public bool isFin;




	void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        isPushing = false;
        CanDash = true;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        princesseArme = GetComponent<PrincesseArme>();
        timerStep = 0.0f;
        sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
        ppg = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincessePouvoirGlace>();
        attackjump = false;
        isCharging = false;
        canMove = true;
		canJump = true;
		canAttack = true;
		canUsePower = true;
		canOpenInventory = true;
		// Gestion Combos
		attaqueBegin = false;
        anim.speed = 1;
		isFin = false;
    }

    void Update()
    {
        //Debug.Log(anim.speed);   
        /*
        bool toucheTriche = Input.GetKeyDown(KeyCode.K);
        if(toucheTriche){
            this.transform.position = new Vector3(-27.0f + 64.01028f + -32.14713f, 14.0f + 18.92948f + -28.89907f, -34.0f + 6.844484f + -30.98021f);
        }*/
        AnimatorClipInfo[] clipinfo = anim.GetCurrentAnimatorClipInfo(0);
        
        float moveHorizontal = InputManager.GetKeyAxis("Horizontal");
        float moveVertical = InputManager.GetKeyAxis("Vertical");

        if(anim.GetBool("isPushing")){
            moveHorizontal = 0;
        }
        
        if ((moveHorizontal != 0.0f || moveVertical != 0.0f) && !anim.GetCurrentAnimatorStateInfo(0).IsName("IcePower") && canMove)
        {
             //anim.enabled = true;
            
            if (InputManager.GetButtonDown("Dash")  && moveHorizontal!=0f)
            {
                if (CanDash == true && isGrounded == true)
                {
                    AttaqueInteromput();
                    anim.Play("leftdash");
                    rb.AddForce(transform.rotation * new Vector3(moveHorizontal, 0f, 0f).normalized * forceDash, ForceMode.Impulse);
                    StartCoroutine(WaitForVelocityZero());
                    CanDash = false;
                    StartCoroutine(WaitBeforDash());
                }
            }
            else
            {
                GererDeplacement(moveHorizontal, moveVertical);
                if (!anim.GetBool("IsJumping") && isGrounded && !attaqueBegin)
                {
                    attackjump = false;
                    if ((moveHorizontal != 0.0f && moveVertical == 0.0f))
                    {
                        gererAnim("IsSidewalk");
                    }
                    else if ((moveVertical < 0.0f && moveHorizontal != 0.0f) || moveVertical < 0.0f && !anim.GetBool("isPushing") && !anim.GetBool("IsClimbing"))
                    {
                        gererAnim("IsBackwalk");
                    }
                    else if (moveVertical > 0.0f && !anim.GetBool("isPushing") && !anim.GetBool("IsClimbing"))
                    {
                        gererAnim("IsRunning");
                    }
                    else if(moveVertical > 0.0f && anim.GetBool("isPushing"))
                    {
                        anim.Play("push");
                    }
                    else if(moveVertical > 0.0f && anim.GetBool("IsClimbing")){
                       
                        gererAnim("IsClimbing");
                    }
                  
                }
                else if (isGrounded)
                {       
                    attackjump = false;
                    anim.SetBool("IsJumping", false);
                }
                else if(!anim.GetBool("IsClimbing") && !anim.GetBool("EndClimbing"))
                {     
                    gererAnim("IsJumping");
                }
            }
        }
        else
        {
	        if (isGrounded && anim.GetBool("IsJumping"))
	        {
		        gererAnim("IsIdle");
                
			}else if(isGrounded && !anim.GetBool("IsIdle") && !anim.GetBool("EndClimbing") && !isFin){
                  
		        gererAnim ("IsIdle");
	        }
              else if(moveVertical == 0.0f && anim.GetBool("IsClimbing")){
                    if (anim.GetCurrentAnimatorStateInfo(0).IsName("grimper")){
                      //  Debug.Log("STOP");
                        anim.speed = 0;
                    }
                }
			else if(isFin){
				gererAnim ("IsRunning");
			}


        }

        Vector3 velocity = rb.velocity;  
        bool saut = InputManager.GetButtonDown("Jump");
        if (saut && isGrounded && CanDash && velocity.y < 0.8 && velocity.y > -0.8 && !anim.GetBool("isPushing") && !anim.GetBool("IsClimbing") && canJump)
        {
	        rb.AddForce(new Vector3(0.0f, forceSaut, 0.0f));
            AttaqueInteromput();
            gererAnim("IsJumping");
	        isGrounded = false;
        }
  
        //Gestion de l attaque standard
        bool toucheAttack1 = InputManager.GetButtonDown("AttaqueSimple");
        if (toucheAttack1 && !anim.GetBool("isPushing") && canAttack)
        {
	        if (anim.GetBool("IsIdle") && !anim.GetBool("IsJumping") || anim.GetBool("IsRunning") || anim.GetBool("IsSidewalk") || anim.GetBool("IsBackwalk"))
	        {
                if (!attaqueBegin)
                {
					transform.parent = null;
					playAttaque("combo1");
                }
            }
        }


        bool toucheAttackCharge = InputManager.GetButtonDown("AttaqueCharge");
        if(toucheAttackCharge && !anim.GetBool("isPushing") && canAttack && !attaqueBegin)
        {
            
            if (anim.GetBool("IsIdle") && !anim.GetBool("IsJumping"))
	        {
                playAttaqueCharge("ChargeAttaqueCharge");
            }
	        else if (anim.GetBool("IsRunning") == true)
	        {
                playAttaqueCharge("ChargeAttaqueCharge");
	        }
	        else if (anim.GetBool("IsSidewalk") == true)
	        {
                playAttaqueCharge("ChargeAttaqueCharge");
            }
            else if (anim.GetBool("IsBackwalk"))
            {
                playAttaqueCharge("ChargeAttaqueCharge");
            }
        }
    }

	public void LockPrincesse() {
		canMove = false;
		canJump = false;
		canAttack = false;
		canUsePower = false;
		canOpenInventory = false;
	}

	public void UnlockPrincesse() {
		canMove = true;
		canJump = true;
		canAttack = true;
		canUsePower = true;
		canOpenInventory = true;
	}

	private void playAttaque(string attaqueName){
        if(princesseArme.armeActive == EnumArmes.BAGUETTE_MAGIQUE && !anim.GetBool("isPushing")){
            anim.Play("attaqueBaguetteMagique");
        }else {
            anim.Play(attaqueName);
        }
        princesseArme.lancerAttaque();
    }

    private void playAttaqueCharge(string attaqueName){
        if(princesseArme.armeActive == EnumArmes.POELE && !anim.GetBool("isPushing")){
            anim.Play("attaqueReversPoele");
        }else {
            anim.Play(attaqueName);
        }
        princesseArme.lancerAttaqueCharge();
    }

    public void gererAnim(string stringToTrue)
    {
        //Met tous les anim.setBool a false sauf celui du stringToTrue
        gererAnim();

        anim.SetBool(stringToTrue, true);

    }

    public void gererAnim()
    {
        //Met tous les anim.setBool a false sauf celui du stringToTrue
        anim.SetBool("IsRunning", false);
        anim.SetBool("IsBackwalk", false);
        anim.SetBool("IsSidewalk", false);
        anim.SetBool("AttaqueContinu", false);
        anim.SetBool("IsIdle", false);
        anim.SetBool("IsJumping", false);
        anim.SetBool("isPushing", false);
        anim.SetBool("IsClimbing", false);
        anim.SetBool("EndClimbing", false);
    }

    IEnumerator WaitForVelocityZero()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.3f);
    }

    private void GererDeplacement(float moveHorizontal, float moveVertical)
    {

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName(anim.GetLayerName(0) + ".hurt") && !anim.GetBool("IsClimbing"))
        {
            float difRotation = cam.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y;

                float rotation;

                if (difRotation > 180.0f)
                {
                    difRotation -= 360.0f;
                }

                if (difRotation < -180.0f)
                {
                    difRotation += 360.0f;
                }

                rotation = Mathf.Clamp(difRotation, -vitesseAngulaire, vitesseAngulaire);

                this.transform.Rotate(0.0f, rotation, 0.0f);

            Vector3 mouvement = this.transform.forward * Mathf.Max(moveVertical, -0.5f);
            float norme = Mathf.Max(mouvement.magnitude, 0.5f);

            mouvement += this.transform.right * moveHorizontal * 0.5f;

            mouvement = (mouvement / mouvement.magnitude) * norme;
          
            this.transform.position += mouvement * vitesse * Time.deltaTime;
                        
            if (timerStep <= Time.time && isGrounded && CanDash && !anim.GetBool("isPushing"))
            {
                bruiteurPas.pas();
                timerStep = Time.time + (Random.Range(0.9f, 1.0f) * (1.0f / mouvement.magnitude) * 0.3f);
            }
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName(anim.GetLayerName(0) + ".hurt") && anim.GetBool("IsClimbing"))
        {
            //Debug.Log("je passe ici dans le truc pour grimper");

            Vector3 mouvement = this.transform.up * Mathf.Max(moveVertical, -0.5f);
            float norme = Mathf.Max(mouvement.magnitude, 0.5f);

          //  mouvement += this.transform.right * moveHorizontal * 0.5f;

            mouvement = (mouvement / mouvement.magnitude) * norme;
          
            this.transform.position += mouvement * Time.deltaTime;
        }


    }

    IEnumerator WaitBeforDash()
    {
        yield return new WaitForSeconds(1f);
        CanDash = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "sol" || collision.collider.tag == "Decor")
        {
           
            isGrounded = true;
        }
    }

    public void AttaqueInteromput() {
        attaqueBegin = false;
    }

    private void OnCollisionExit(Collision collision){
        if((collision.collider.tag == "sol" || collision.collider.tag == "Decor") && !anim.GetBool("IsClimbing")){
            
            isGrounded=false;
            gererAnim("IsJumping");
        }

        if((collision.collider.tag == "sol" || collision.collider.tag == "Decor") && anim.GetBool("IsClimbing")){
            
            isGrounded=false;
        }

    }
}