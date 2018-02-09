using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private GameObject cam;
    private bool CanDash;
    private Rigidbody rb;
    private bool isPushing;
    private PrincesseArme princesseArme;
    private GameObject pushableCube;
    private float timerStep;
    private SoundManager sm;
    public BruiteurPas bruiteurPas;
    public float forceDash;


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

    }

    void Update()
    {

        bool toucheDebug = Input.GetKeyDown(KeyCode.K);

        if (toucheDebug)
        {

        }

        float moveHorizontal = InputManager.GetKeyAxis("Horizontal");
        float moveVertical = InputManager.GetKeyAxis("Vertical");

        if (moveHorizontal != 0.0f || moveVertical != 0.0f)
        {
            if (InputManager.GetButtonDown("Dash")  && moveHorizontal!=0f)
            {
                if (CanDash == true && isGrounded == true)
                {
                    //anim.Play("fwdash");
                    anim.Play("leftdash");
                   // gererAnim("IsDashing");
                    rb.AddForce(transform.rotation * new Vector3(moveHorizontal, 0f, 0f).normalized * forceDash, ForceMode.Impulse);
                    StartCoroutine(WaitForVelocityZero());
                    CanDash = false;
                    StartCoroutine(WaitBeforDash());
                }
            }
            else
            {
                GererDeplacement(moveHorizontal, moveVertical);
                if (!anim.GetBool("IsJumping") && isGrounded)
                {
                    if ((moveHorizontal != 0.0f && moveVertical == 0.0f) && (!anim.GetBool("IsSidewalk")))
                    {
                        gererAnim("IsSidewalk");
                    }
                    else if (moveVertical < 0.0f && moveHorizontal == 0.0f)
                    {
                        gererAnim("IsBackwalk");
                    }
                    else if (moveVertical > 0.0f)
                    {
                        gererAnim("IsRunning");
                    }
                }
                else if (isGrounded)
                {
                    anim.SetBool("IsJumping", false);
                }

                else
                {
                    gererAnim("IsJumping");

                }
            }
        }
        else
        {
	        if (isGrounded && anim.GetBool("IsJumping"))
	        {
		        //gererAnim("IsIdle");
	        }else if(isGrounded){
		        gererAnim ("IsIdle");
	        }
	        else
	        {
		        gererAnim();
	        }
        }


        bool saut = InputManager.GetButtonDown("Jump");
        if (saut && isGrounded && CanDash)
        {
	        rb.AddForce(new Vector3(0.0f, forceSaut, 0.0f));
	        gererAnim("IsJumping");
	        isGrounded = false;
        }

        //Gestion de l attaque standard
        bool toucheAttack1 = InputManager.GetButtonDown("AttaqueSimple");
        if (toucheAttack1)
        {
	        if (anim.GetBool("IsIdle") && !anim.GetBool("IsJumping"))
	        {
		        anim.Play("attack1");
                Debug.Log("Attaque idle");
                princesseArme.lancerAttaque();
            }
	        else if (anim.GetBool("IsJumping"))
	        {
		        anim.Play("attack_jump");
		        rb.AddForce(transform.forward * 500f);
		        rb.AddForce(new Vector3(0.0f, -1000f, 0.0f));
                Debug.Log("Attaque saute");
		        princesseArme.lancerAttaque();
	        }
	        else if (anim.GetBool("IsRunning") == true)
	        {
                Debug.Log("Attaque Run");
                anim.Play("attack_run");
		        princesseArme.lancerAttaque();
	        }
	        else if (anim.GetBool("IsSidewalk") == true)
	        {
                Debug.Log("Attaque RUn");
                anim.Play("attack_run");
                princesseArme.lancerAttaque();
            }else if (anim.GetBool("IsBackwalk"))
            {
                anim.Play("attack_backwalk");
                Debug.Log("Attaque backwalk");
                princesseArme.lancerAttaque();
            }
        }


    }

private void gererAnim(string stringToTrue)
{
	//Met tous les anim.setBool a false sauf celui du stringToTrue
	gererAnim();

	anim.SetBool(stringToTrue, true);

}

private void gererAnim()
{
	//Met tous les anim.setBool a false sauf celui du stringToTrue
	anim.SetBool("IsRunning", false);
	anim.SetBool("IsBackwalk", false);
	anim.SetBool("IsSidewalk", false);
	anim.SetBool("IsIdle", false);
	//anim.SetBool("isPushing", false);
	anim.SetBool("IsJumping", false);
    //anim.SetBool("IsDashing", false);

}



IEnumerator WaitForVelocityZero()
{
	rb.velocity = Vector3.zero;
	yield return new WaitForSeconds(0.3f);
}


private void GererDeplacement(float moveHorizontal, float moveVertical)
{

	if (!anim.GetCurrentAnimatorStateInfo(0).IsName(anim.GetLayerName(0) + ".hurt"))
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

		if (isPushing == false)
		{
			this.transform.position += mouvement * vitesse * Time.deltaTime;
		}
		else
		{
			this.transform.position += mouvement * vitesse / 2 * Time.deltaTime;
			//pushableCube.transform.position += mouvement * vitesse/2 * Time.deltaTime;
		}

		if (timerStep <= Time.time && isGrounded && CanDash)
		{
            bruiteurPas.pas();
			timerStep = Time.time + (Random.Range(0.9f, 1.0f) * (1.0f / mouvement.magnitude) * 0.3f);
		}
	}


}

void FixedUpdate()
{
	// Vector3 fwd = transform.TransformDirection(Vector3.down);
	// if (Physics.Raycast(transform.position, fwd, feetDist))
	// {
	// 	//gererAnim();
	// 	anim.SetBool("IsJumping", false);
	// }
	// else
	// {

	// 	anim.SetBool("IsJumping", true);
	// }
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

private void OnCollisionExit(Collision collision){
    if(collision.collider.tag == "sol"){
        isGrounded=false;
        gererAnim("IsJumping");
    }

     if(collision.collider.tag == "Decor"){
        isGrounded=false;
        
    }

}

}



