using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableHUD : MonoBehaviour
{
    public Text collectedText;

    GameObject Diamond1;

    void Start()
    {
        Diamond1 = GameObject.Find("Diamond1");
        Image d1 = Diamond1.GetComponent<Image>();
        Collectable.onCollected += OnCollected;
    }

    void UpdateDiamondUI()
    {
        Diamond1.GetComponent<Image>().color = Color.white;
        Diamond1.GetComponent<Animator>().enabled = true;
    }

    void OnCollected()
    {
        collectedText.text = "x" + Collectable.num;
        UpdateDiamondUI();
        
    }
}
