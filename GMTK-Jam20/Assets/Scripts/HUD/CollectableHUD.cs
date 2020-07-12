using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableHUD : MonoBehaviour
{
    public Text collectedText;

    void Start()
    {
        Collectable.onCollected += OnCollected;
    }

    void OnCollected()
    {
        collectedText.text = "x" + Collectable.num;
    }
}
