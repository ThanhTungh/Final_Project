using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator animator; 
    
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public void ShowCloseAnimation()
    {
        animator.SetTrigger("CloseDoor");
    }

    public void ShowOpenAnimation()
    {
        animator.SetTrigger("OpenDoor");
    }
}
