using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class IA_Etat : MonoBehaviour
{

    protected IA_Agent agent;

	protected NavMeshAgent nav;
	protected Animator anim;
	protected Rigidbody rb;
    protected GameObject princesse;
	protected PrincesseVie princesseVie;
	protected PrincesseArme princesseArme;
	protected IA_PointInteret[] pointsInteret;
	protected IA_Perception perception;

    // Use this for initialization
    void Awake()
    {
        init();
    }

    protected void init()
    {
		agent = this.GetComponent<IA_Agent>();
        nav = agent.getNav();
		anim = agent.getAnimator ();
		rb = agent.getRigidbody ();
        princesse = agent.getPrincesse();
		princesseVie = agent.getPrincesseVie();
		princesseArme = agent.getPrincesseArme ();
        pointsInteret = agent.getPointsInteret();
		perception = agent.getPerception();
    }

    public abstract void entrerEtat();
    public abstract void faireEtat();
    public abstract void sortirEtat();

	public virtual void subirDegats(int valeurDegats, Vector3 hitPoint) {
		
		changerEtat (agent.etatEtreBlesseDefaut);
		agent.getMobVie().blesser (valeurDegats, hitPoint);
	}

	public virtual void subirDegats(int valeurDegats) {

		changerEtat (agent.etatEtreBlesseDefaut);
		agent.getMobVie().blesser (valeurDegats);
	}

	public virtual float getDistanceEntreeCombat(){
		return 0;
	}

	protected void changerEtat(IA_Etat nouvelEtat)
    {
        agent.changerEtat(nouvelEtat);
    }

    protected void setAnimation(string nomAnimation)
    {
        agent.setAnimation(nomAnimation);
    }
}
