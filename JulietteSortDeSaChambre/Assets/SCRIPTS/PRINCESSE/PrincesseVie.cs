using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrincesseVie : MonoBehaviour {
	static Animator anim;

	public int vie_max;
	public float reculeVertical;
	public float reculeHorizontal;
    
	[Header("Princesse prend un dégât :")]
	public GameObject ParticleBlood;
	[Tooltip("Hauteur min : 0.5, max : 1.5")]
	public float HauteurParticule;
	[Tooltip("Son quand la princesse prend des dégâts")]	
	public AudioClip PrincesseHurt;
	private bool CanPlaySonHurt;

	private int vie_courante;
	private Rigidbody rb;
	private bool gameover;
	private Scene scene;
	private SoundManager sm;

	/*void Awake(){
		vie_courante = vie_max;
		Debug.Log (vie_courante);

	}*/


	// Use this for initialization
	void Start () {
		scene = SceneManager.GetActiveScene ();
		if (scene.name == "TEST") {
			GameControl.control.Save ();
			vie_courante = vie_max;
			GameControl.control.vie = vie_courante;
		} else {
			vie_courante = GameControl.control.vie;
		}
		gameover = false;
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		sm = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager>();
		CanPlaySonHurt = true;
	}

	// Update is called once per frame
	void Update () {
		if (!enVie() && !gameover) {
			Debug.Log ("GAME OVER");
			gameover = true;
			SceneManager.LoadScene (scene.name);
			GameControl.control.Load ();
			Debug.Log(GameControl.control.listArmeTenu);
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

	public void mourir()
	{
		vie_courante = 0;
		GameControl.control.vie = vie_courante;
		Debug.Log("vie courante : " + vie_courante);
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

		if (CanPlaySonHurt)
		{
			CanPlaySonHurt = false;
			sm.playOneShot(PrincesseHurt,Random.Range(0.5f,0.7f),Random.Range(0.85f,1.0f));
			StartCoroutine(WaitForSonHurtToPlay());
		}
		
        Instantiate(ParticleBlood, new Vector3(this.transform.position.x, this.transform.position.y + HauteurParticule, this.transform.position.z), Quaternion.identity);

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

	IEnumerator WaitForSonHurtToPlay()
	{
		yield return new WaitForSecondsRealtime(1);
		CanPlaySonHurt = true;
	}
}
