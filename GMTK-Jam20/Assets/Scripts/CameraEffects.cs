using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CameraShakeParams
{
    public float magnitude;
    public float roughness;
    public float fadeInTime;
    public float fadeOutTime;

    public CameraShakeParams(float mag, float rough, float inTime, float outTime)
    {
        magnitude = mag;
        roughness = rough;
        fadeInTime = inTime;
        fadeOutTime = outTime;
    }
}

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
