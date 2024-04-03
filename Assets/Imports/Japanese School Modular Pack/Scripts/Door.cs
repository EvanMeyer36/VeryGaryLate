using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public bool IsOpenOnStart; // Do not use this to toggle the door while in play. Use ToggleDoor function instead
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsOpen", IsOpenOnStart);
    }

    public void ToggleDoor() // Call this function to toggle the door!
    {
        IsOpenOnStart = !IsOpenOnStart;
        anim.SetBool("IsOpen", IsOpenOnStart);
    }
}
