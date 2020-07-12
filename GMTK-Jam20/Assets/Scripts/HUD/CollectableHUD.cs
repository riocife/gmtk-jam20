using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollectableHUD : MonoBehaviour
{
    public Text collectedText;

    public GameObject Diamond1;
    public GameObject Diamond2;

    void Start()
    {
        Collectable.onCollected += OnCollected;
    }

    void UpdateDiamond1UI()
    {
        Diamond1.GetComponent<Image>().color = Color.white;
        Diamond1.GetComponent<Animator>().enabled = true;
    }
    void UpdateDiamond2UI()
    {
        Diamond2.GetComponent<Image>().color = Color.white;
        Diamond2.GetComponent<Animator>().enabled = true;
    }

    void OnCollected(int num)
    {
        if (num == 1)
        {
            UpdateDiamond1UI();
        }
        else if (num == 2)
        {
            UpdateDiamond2UI();
        }

        if (Collectable.num == 2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
