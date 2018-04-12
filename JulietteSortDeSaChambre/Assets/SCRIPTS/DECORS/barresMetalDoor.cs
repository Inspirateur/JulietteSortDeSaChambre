using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barresMetalDoor : MonoBehaviour {

    public bool OpenToRight = true;

    public float DistanceToMove = 3.318f;

    public bool MoveOnX = false;
    public bool MoveOnY = true;
    public bool MoveOnZ = false;

    private float positionToGo;

    [HideInInspector]
    public bool isMoving = false;

    public float speed = 1;

    // [HideInInspector]
    public bool canMove = false;

    private AudioSource audioSource;
    public AudioClip BarreMecanisme;

    private Transform transformBarre;
    private Vector3 transformBarreOriginal;


    void Start() {
        audioSource = GetComponent<AudioSource>();
        transformBarre = GetComponent<Transform>();
        transformBarreOriginal = transformBarre.localPosition;
    }

    private void Update() {
        if (isMoving && canMove) {
            if (OpenToRight) {
                OpenRight();
            } else {
                OpenLeft();
            }
        } else if (isMoving && !canMove) {
            if (OpenToRight) {
                OpenLeft();
            } else {
                OpenRight();
            }
        }
    }

    public void OpenBarre() {
        canMove = true;
        DeterminePositionToGo();
        audioSource.PlayOneShot(BarreMecanisme);
        isMoving = true;
    }

    public void CloseBarre() {
        canMove = false;
        DeterminePositionToGo();
        audioSource.PlayOneShot(BarreMecanisme);
        isMoving = true;
    }

    private void DeterminePositionToGo() {
        if (OpenToRight && canMove || !OpenToRight && !canMove) {
            if (MoveOnX) {
                positionToGo = transformBarreOriginal.x + DistanceToMove;
            }
            else if (MoveOnY) {
                positionToGo = transformBarreOriginal.y + DistanceToMove;
            }
            else if (MoveOnZ) {
                positionToGo = transformBarreOriginal.z - DistanceToMove;
            }
        } else {
            if (MoveOnX) {
                positionToGo = transformBarreOriginal.x - DistanceToMove;
            }
            else if (MoveOnY) {
                positionToGo = transformBarreOriginal.y - DistanceToMove;
            }
            else if (MoveOnZ) {
                positionToGo = transformBarreOriginal.z + DistanceToMove;
            }
        }
    }

    public void OpenRight() {
        if (MoveOnX) {
            if (transformBarre.localPosition.x < positionToGo) {
                transformBarre.Translate(Vector3.right * speed * Time.deltaTime);
            } else {
                transformBarre.localPosition = new Vector3 (positionToGo, transformBarre.localPosition.y, transformBarre.localPosition.z);
                canMove = false;
                isMoving = false;
            }
        } else if (MoveOnY) {
            if (transformBarre.localPosition.y < positionToGo) {
                transformBarre.Translate(Vector3.up * speed * Time.deltaTime);
            } else {
                transformBarre.localPosition = new Vector3(transformBarre.localPosition.x, positionToGo, transformBarre.localPosition.z);
                canMove = false;
                isMoving = false;
            }
        } else if (MoveOnZ) {
            if (transformBarre.localPosition.z > positionToGo) {
                transformBarre.Translate(Vector3.back * speed * Time.deltaTime);
            } else {
                transformBarre.localPosition = new Vector3(transformBarre.localPosition.x, transformBarre.localPosition.y, positionToGo);
                canMove = false;
                isMoving = false;
            }
        }
    }

    public void OpenLeft() {
        if (MoveOnX) {
            if (transformBarre.localPosition.x > positionToGo) {
                transformBarre.Translate(Vector3.left * speed * Time.deltaTime);
            } else {
                transformBarre.localPosition = new Vector3(positionToGo, transformBarre.localPosition.y, transformBarre.localPosition.z);
                canMove = false;
                isMoving = false;
            }
        } else if (MoveOnY) {
            if (transformBarre.localPosition.y > positionToGo) {
                transformBarre.Translate(Vector3.down * speed * Time.deltaTime);
            } else {
                transformBarre.localPosition = new Vector3(transformBarre.localPosition.x, positionToGo, transformBarre.localPosition.z);
                canMove = false;
                isMoving = false;
            }
        }
        else if (MoveOnZ) {
            if (transformBarre.localPosition.z < positionToGo) {
                transformBarre.Translate(Vector3.forward * speed * Time.deltaTime);
            } else {
                transformBarre.localPosition = new Vector3(transformBarre.localPosition.x, transformBarre.localPosition.y, positionToGo);
                canMove = false;
                isMoving = false;
            }
        }
    }

}
