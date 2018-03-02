using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour {
	public bool CanRotate;
	public bool CanLevitate;
	public float Speed;
	public float amplitude = 0.5f;
    public float frequency = 1f;

	Vector3 posOffset = new Vector3 ();
    Vector3 tempPos = new Vector3 ();

	[Range(1, 6)]
	[Tooltip("Direction de la rotation : 1 = forward, 2 = back, 3 = left, 4 = right, 5 = up, 6 = down")]
	public int direction;
	// Use this for initialization
	void Start () {
		posOffset = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (CanRotate) {
			switch (direction)
			{
			case 1:
		
				gameObject.transform.Rotate (Vector3.forward * Speed * Time.deltaTime, Space.Self);
				break;
			case 2:
				gameObject.transform.Rotate (Vector3.back * Speed * Time.deltaTime, Space.Self);
				break;
			case 3:
				gameObject.transform.Rotate (Vector3.left * Speed * Time.deltaTime, Space.Self);
				break;
			case 4:
				gameObject.transform.Rotate (Vector3.right * Speed * Time.deltaTime, Space.Self);
				break;
			case 5:

				gameObject.transform.Rotate (Vector3.up * Speed * Time.deltaTime, Space.Self);
				break;
			case 6:
		
				gameObject.transform.Rotate (Vector3.down * Speed * Time.deltaTime, Space.Self);
				break;
			}
		}

		if (CanLevitate){
			tempPos = posOffset;
			tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
			transform.position = tempPos;
		}


	}
}
