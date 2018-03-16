using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAttaque1 : StateMachineBehaviour {

	private PrincesseDeplacement deplacement;
	private Animator anim;
	private SoundManager sm;
	public int SonJouer;
	public bool DernierAnim;


	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		deplacement = GameObject.FindGameObjectWithTag("Player").GetComponent<PrincesseDeplacement>();
		sm = GameObject.FindGameObjectWithTag("SoundManager").GetComponent<SoundManager>();
		anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        anim.SetBool("AttaqueContinu", false);
        sm.playOneShot (deplacement.ComboSound[SonJouer]);
		Debug.Log("Combo"+SonJouer);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (InputManager.GetButtonDown("AttaqueSimple") && !DernierAnim)
		{
            anim.SetBool("AttaqueContinu", true);
            Debug.Log("YES");
        }
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //Debug.Log(deplacement.attaqueBegin);
        Debug.Log(anim.GetBool("AttaqueContinu"));
        if (DernierAnim || !anim.GetBool("AttaqueContinu"))
		{
            deplacement.attaqueBegin = false;
        }
        //Debug.Log(deplacement.attaqueBegin);
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
