using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour {

    [Header("Object to Unlock :")]
    public GameObject objectLock;
    private Animator objectLockAnim;
    public string boolOpen;
    public string boolToUnlock;

    [Header("Locker :")]
    public GameObject locker;
    private Animator lockerAnim;
    public string lokerUnlock;
    public string nomAnimLockerUnlock;

    // Use this for initialization
    void Awake () {
        objectLockAnim = objectLock.GetComponent<Animator>();
        lockerAnim = locker.GetComponent<Animator>();
        objectLockAnim.SetBool(boolToUnlock, false);
    }
	
	// Update is called once per frame
	void Update () {
        if (objectLockAnim.GetBool(boolOpen)) {
            lockerAnim.SetBool(lokerUnlock, true);
        }

        if (objectLockAnim.GetBool(boolOpen) && lockerAnim.GetBool(lokerUnlock) && lockerAnim.GetCurrentAnimatorStateInfo(0).IsName(nomAnimLockerUnlock)) {
            objectLockAnim.SetBool(boolToUnlock, true);
        }
    }
}
