using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private PlayerMovement player;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    
    void PlayDashAnim()
    {
        animator.SetBool("dashing", true);
    }
}
