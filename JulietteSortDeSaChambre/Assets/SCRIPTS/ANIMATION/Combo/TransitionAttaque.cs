using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionAttaque : StateMachineBehaviour {

	private GameObject princesse;
	private PrincesseDeplacement deplacement;
	private PrincesseArme arme;
	private GameObject cam;
	private Animator anim;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		princesse = GameObject.FindGameObjectWithTag("Player");
		cam = GameObject.FindGameObjectWithTag("MainCamera");
		deplacement = princesse.GetComponent<PrincesseDeplacement>();
		arme = princesse.GetComponent<PrincesseArme>();
		anim = princesse.GetComponent<Animator>();

		deplacement.attaqueBegin = true;

		anim.SetBool("AttaqueContinu", false);

		arme.attaqueCorpsACorpsEnCours = !arme.attaqueCorpsACorpsEnCours;

		//float moveHorizontal = InputManager.GetKeyAxis("Horizontal");
		//float moveVertical = InputManager.GetKeyAxis("Vertical");
		//princesse.transform.localEulerAngles = new Vector3(0, Mathf.Atan2(moveVertical, -moveHorizontal) * Mathf.Rad2Deg, 0);
		princesse.transform.localEulerAngles = new Vector3(0, cam.transform.localEulerAngles.y, 0);
		princesse.GetComponent<Rigidbody>().AddForce(princesse.transform.rotation * Vector3.forward * 20, ForceMode.Impulse);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (InputManager.GetButtonDown("AttaqueSimple")) {
			anim.SetBool("AttaqueContinu", true);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		arme.listeMobsTouches.Clear();
		arme.attaqueCorpsACorpsEnCours = !arme.attaqueCorpsACorpsEnCours;
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
