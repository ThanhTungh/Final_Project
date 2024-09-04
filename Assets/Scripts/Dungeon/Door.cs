using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator; // Animator: https://docs.unity3d.com/ScriptReference/Animator.html
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // Set animation "close or open door" when player enter a room
    public void ShowCloseAnimation()
    {
        animator.SetTrigger("CloseDoor");
    }

    public void ShowOpenAnimation()
    {
        animator.SetTrigger("OpenDoor");
    }
}
