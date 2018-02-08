using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincesseVie : MonoBehaviour {
	static Animator anim;

	public int vie_max;
	public float reculeVertical;
	public float reculeHorizontal;
    public GameObject ParticleBlood;

	private int vie_courante;
	private Rigidbody rb;
	private bool gameover;
	private Scene scene;

	/*void Awake(){
		vie_courante = vie_max;
		Debug.Log (vie_courante);

	}*/


	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene ();
		if (scene.name == "TEST") {
			vie_courante = vie_max;
			GameControl.control.vie = vie_courante;
		} else {
			vie_courante = GameControl.control.vie;
		}
		gameover = false;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();

	}

	// Update is called once per frame
	void Update () {
		if (!enVie() && !gameover) {
			Debug.Log ("GAME OVER");
			gameover = true;
			SceneManager.LoadScene (scene.name);
			GameControl.control.Load ();
			vie_courante = vie_max;
			GameControl.control.vie = vie_courante;
			GameControl.control.Save ();
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			soigner(10);

		} else if (Input.GetKeyDown (KeyCode.W)) {
			blesser(10, this.gameObject, 0.0f);
		}

	}

	public void soigner(int valeurSoin)
	{
		vie_courante = Mathf.Min(vie_courante + valeurSoin, vie_max);
		GameControl.control.vie = vie_courante;
		Debug.Log("vie courante : " + vie_courante);
	}

	public void blesser(int valeurDegats, GameObject sourceDegats, float facteurRecule)
	{
		anim.Play ("hurt");

        Instantiate(ParticleBlood, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), Quaternion.identity);

        Vector3 directionRecule = (this.transform.position - sourceDegats.transform.position).normalized;

		rb.velocity = Vector3.zero;
		rb.AddForce ((directionRecule * (reculeHorizontal * facteurRecule)) + (this.transform.up * (reculeVertical * facteurRecule)));

		vie_courante = Mathf.Max(vie_courante - valeurDegats, 0);
		Debug.Log("vie courante : " + vie_courante);
		GameControl.control.vie = vie_courante;
	}

	public bool enVie()
	{
		return vie_courante > 0;
	}

	public int getVieCourante(){
		return vie_courante;
	}
}
