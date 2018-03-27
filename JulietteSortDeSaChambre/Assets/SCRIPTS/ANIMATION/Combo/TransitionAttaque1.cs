using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAttaque1 : StateMachineBehaviour {

	private PrincesseDeplacement deplacement;
    private GameObject princesse;
    private GameObject cam;
    private Animator anim;
	private SoundManager sm;
	public int SonJouer;
	public bool DernierAnim;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        princesse = GameObject.FindGameObjectWithTag("Player");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        deplacement = princesse.GetComponent<PrincesseDeplacement>();
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		anim = princesse.GetComponent<Animator>();
        anim.SetBool("AttaqueContinu", false);
        // float moveHorizontal = InputManager.GetKeyAxis("Horizontal");
        // float moveVertical = InputManager.GetKeyAxis("Vertical");
        // princesse.transform.localEulerAngles = new Vector3(0, Mathf.Atan2(moveVertical, -moveHorizontal) * Mathf.Rad2Deg, 0);
        princesse.transform.localEulerAngles = new Vector3(0, cam.transform.localEulerAngles.y, 0);
        princesse.GetComponent<Rigidbody>().AddForce(princesse.transform.rotation * Vector3.forward * 20, ForceMode.Impulse);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (InputManager.GetButtonDown("AttaqueSimple") && !DernierAnim)
		{
            anim.SetBool("AttaqueContinu", true);
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (DernierAnim || !anim.GetBool("AttaqueContinu"))
		{
            deplacement.attaqueBegin = false;
        }
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
