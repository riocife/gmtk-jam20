using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RandomSound
{
    [Range(0f, 1f)]
    public float volumeMin = 0.4f;

    [Range(0f, 1f)]
    public float volumeMax = 1f;

    public float basePitch = 1f;
    public float pitchMinVariance = -0.3f;
    public float pitchMaxVariance = 0.3f;

    public float Volume
    {
        get
        {
            return Random.Range(volumeMin, volumeMax);
        }
    }

    public float Pitch
    {
        get
        {
            return basePitch + Random.Range(pitchMinVariance, pitchMaxVariance);
        }
    }
}
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
