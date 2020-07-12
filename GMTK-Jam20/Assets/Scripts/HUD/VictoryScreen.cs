using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScreen : MonoBehaviour
{
    public float timeToLoad = 4f;

    void Start()
    {
        StartCoroutine(MainMenu());
    }

    IEnumerator MainMenu()
    {
        yield return new WaitForSeconds(timeToLoad);

        SceneManager.LoadScene(0);
    }
}
